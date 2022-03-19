from rest_framework import viewsets, permissions
from rest_framework.response import Response
from rest.serializers import UserSerializer, PokemonSerializer
from django.contrib.auth import get_user_model

from pokeprofile.models import Pokemon

class UserViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows users to be viewed or edited.
    """
    queryset = get_user_model().objects.all().order_by('-date_joined')
    serializer_class = UserSerializer
    permission_classes = [permissions.IsAuthenticated]

    def create(self, request, *args, **kwargs):
        obj = super().create(request, *args, **kwargs)
        return obj

class PokemonViewSet(viewsets.ReadOnlyModelViewSet):
    queryset = Pokemon.objects.all().order_by('pokedex')
    serializer_class = PokemonSerializer
    permission_classes = [permissions.IsAuthenticated]