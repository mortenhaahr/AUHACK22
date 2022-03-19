from email.policy import default
from django_filters import rest_framework as django_filters
from django_filters.rest_framework import DjangoFilterBackend
from django.forms.models import model_to_dict 
from django.contrib.auth import get_user_model
from rest_framework import viewsets, views, permissions
from rest_framework.response import Response
from rest.serializers import PokeProfileSerializer, UserSerializer, PokemonSerializer

from poke_profile.models import Pokemon, PokeProfile
from poke_profile.algorithm import getProfile

class UserViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows users to be viewed or edited.
    """

    queryset = get_user_model().objects.all().order_by("-date_joined")
    serializer_class = UserSerializer
    permission_classes = [permissions.IsAuthenticated]

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
    """
    A simple ViewSet for listing or retrieving users.
    """
    permission_classes = [permissions.IsAuthenticated]

    def put(self, request, pk=None):
        user = get_user_model().objects.get(pk=pk)
        # Not reusing filter because I don't know how
        pokemons = request.data['pokemons']
        pokemons = Pokemon.objects.filter(name__in=pokemons)
        pokemons = list(map(model_to_dict, pokemons))
        profile_dict = getProfile(pokemons)

        profile_dict_fixed = {}
        profile_dict_fixed['height'] = profile_dict['Height']
        profile_dict_fixed['weight'] = profile_dict['Weight']
        profile_dict_fixed['generation'] = profile_dict['Generation']
        profile_dict_fixed['pokedex'] = profile_dict['Pokedex']
        profile_dict_fixed['catch_rate'] = profile_dict['Catch rate']
        profile_dict_fixed['bmi'] = profile_dict['BMI']
        profile_dict_fixed['gender_ratio'] = profile_dict['Gender ratio']
        profile_dict_fixed['hp'] = profile_dict['Stats']['hp']
        profile_dict_fixed['attack'] = profile_dict['Stats']['attack']
        profile_dict_fixed['defence'] = profile_dict['Stats']['defence']
        profile_dict_fixed['sp_atk'] = profile_dict['Stats']['sp_atk']
        profile_dict_fixed['sp_def'] = profile_dict['Stats']['sp_def']
        profile_dict_fixed['speed'] = profile_dict['Stats']['speed']

        profile_dict_fixed['type_normal'] = profile_dict['Type']['Normal'] if 'Normal' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_flying'] = profile_dict['Type']['Flying'] if 'Flying' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_fire'] = profile_dict['Type']['Fire'] if 'Fire' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_psychic'] = profile_dict['Type']['Psychic'] if 'Psychic' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_water'] = profile_dict['Type']['Water'] if 'Water' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_bug'] = profile_dict['Type']['Bug'] if 'Bug' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_grass'] = profile_dict['Type']['Grass'] if 'Grass' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_rock'] = profile_dict['Type']['Rock'] if 'Rock' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_electric'] = profile_dict['Type']['Electric'] if 'Electric' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_ghost'] = profile_dict['Type']['Ghost'] if 'Ghost' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_ice'] = profile_dict['Type']['Ice'] if 'Ice' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_dark'] = profile_dict['Type']['Dark'] if 'Dark' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_fighting'] = profile_dict['Type']['Fighting'] if 'Fighting' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_dragon'] = profile_dict['Type']['Dragon'] if 'Dragon' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_poison'] = profile_dict['Type']['Poison'] if 'Poison' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_steel'] = profile_dict['Type']['Steel'] if 'Steel' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_ground'] = profile_dict['Type']['Ground'] if 'Ground' in profile_dict['Type'].keys() else 0
        profile_dict_fixed['type_fairy'] = profile_dict['Type']['Fairy'] if 'Fairy' in profile_dict['Type'].keys() else 0
        
        profile, created = PokeProfile.objects.update_or_create(
            user_id=user.pk,
            defaults = profile_dict_fixed
        )

        return Response(model_to_dict(profile))