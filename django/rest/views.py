from django_filters import rest_framework as django_filters
from django_filters.rest_framework import DjangoFilterBackend
from django.forms.models import model_to_dict 
from django.contrib.auth import get_user_model
from rest_framework import viewsets, views, permissions
from rest_framework.response import Response
from rest.serializers import UserSerializer, PokemonSerializer

from poke_profile.models import Pokemon


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
        pokemons = map(model_to_dict, pokemons)
        return Response(pokemons)