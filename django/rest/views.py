from django.db.models import Q
from django_filters import rest_framework as django_filters
from django_filters.rest_framework import DjangoFilterBackend
from django.contrib.auth import get_user_model
from rest_framework import viewsets, permissions
from rest_framework.decorators import action
from rest_framework.response import Response
from rest.serializers import UserSerializer, PokemonSerializer

from pokeprofile.models import Pokemon


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
