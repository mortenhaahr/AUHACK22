from geopy import distance
from django.core.exceptions import ObjectDoesNotExist
from django_filters import rest_framework as django_filters
from django_filters.rest_framework import DjangoFilterBackend
from django.forms.models import model_to_dict 
from django.contrib.auth import get_user_model
from rest_framework import viewsets, views, permissions
from rest_framework.response import Response
from rest.serializers import PokeProfileSerializer, UserSerializer, PokemonSerializer

from poke_profile.models import Pokemon, PokeProfile
from poke_profile.util import get_poke_profile
from poke_profile.match_algorithm import Profile
from user.models import Match


class UserViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows users to be viewed or edited.
    """

    queryset = get_user_model().objects.all().order_by("-date_joined")
    serializer_class = UserSerializer
    permission_classes = [permissions.IsAuthenticated]
    filter_backends = [DjangoFilterBackend]
    filterset_fields = [
        "email"
    ]

    def create(self, request, *args, **kwargs):
        obj = super().create(request, *args, **kwargs)
        return obj

class PokemonNameFilter(django_filters.FilterSet):
    names = django_filters.CharFilter(method="names_comma_seperated")

    def names_comma_seperated(self, queryset, name, value):
        names = value.split(',')
        return Pokemon.objects.filter(name__in=names)
    
    class Meta:
        model = Pokemon
        fields = ['names']


class PokemonViewSet(viewsets.ReadOnlyModelViewSet):
    queryset = Pokemon.objects.all().order_by("pokedex")
    serializer_class = PokemonSerializer
    permission_classes = [permissions.IsAuthenticated]
    filter_backends = [DjangoFilterBackend]
    filterset_class = PokemonNameFilter

class PokeProfileViewSet(viewsets.ReadOnlyModelViewSet):
    queryset = PokeProfile.objects.all().order_by("pokedex")
    serializer_class = PokeProfileSerializer
    permission_classes = [permissions.IsAuthenticated]

class PokeProfileView(views.APIView):
    permission_classes = [permissions.IsAuthenticated]

    def post(self, request, pk=None):
        user = get_user_model().objects.get(pk=pk)
        pokemons = request.data['pokemons']
        
        profile, created = get_poke_profile(user, pokemons)

        return Response(model_to_dict(profile))

class CandidatesView(views.APIView):
    permission_classes = [permissions.IsAuthenticated]

    def get(self, request, pk=None, amount=None):
        user = get_user_model().objects.get(pk=pk)
        poke_profile = user.pokeprofile
        profile = Profile(model_to_dict(poke_profile))

        user_coords = (user.last_seen_lat, user.last_seen_long)

        # This is stupid but ok
        matches = Match.objects.filter(user__pk=user.pk).only('user')
        matches_ids = [match.candidate.pk for match in matches]
        candidates = get_user_model().objects.exclude(pk=pk).exclude(pokeprofile__isnull=True).exclude(pk__in=matches_ids)
        l = lambda inp: distance.distance(user_coords, (inp.last_seen_lat, inp.last_seen_long)).km < user.search_radius
        iter = filter(l, candidates)
        iter = map(lambda inp: model_to_dict(inp.pokeprofile), iter)
        profile.retrieveMatches(iter)
        matches = profile.getMatches(False)
        user_matches = list(map(lambda inp: get_user_model().objects.get(pk=inp), matches))

        result = [UserSerializer(user, context={'request': request}).data for user in user_matches[0:amount]]

        for match in user_matches:
            Match.objects.create(user=user, candidate=match).save()

        return Response(result)

class SmashPassView(views.APIView):
    permission_classes = [permissions.IsAuthenticated]

    def post(self, request, pk=None, candidate_pk=None):
        user = get_user_model().objects.get(pk=pk)
        candidate = get_user_model().objects.get(pk=candidate_pk)
        match = Match.objects.get(user=user, candidate=candidate)
        smash = request.data['smash']
        match.smash = smash
        match.save()

        # Return if counterpart said smash/pass or haven't answered
        try:
            counter_match = Match.objects.get(user=candidate, candidate=user)
        except ObjectDoesNotExist:
            return Response({'counter_part_smashed': None})

        return Response({'counter_part_smashed': counter_match.smash})

    def get(self, request, pk=None, candidate_pk=None):
        user = get_user_model().objects.get(pk=pk)
        candidate = get_user_model().objects.get(pk=candidate_pk)

        # Return if counterpart said smash/pass or haven't answered
        try:
            counter_match = Match.objects.get(user=candidate, candidate=user)
        except ObjectDoesNotExist:
            return Response({'counter_part_smashed': None})

        return Response({'counter_part_smashed': counter_match.smash})