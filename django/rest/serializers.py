from django.contrib.auth import get_user_model
from django.core.exceptions import ValidationError
from rest_framework import serializers, fields

from poke_profile.models import PokeProfile, Pokemon
from user.choices import GENDER

class UserSerializer(serializers.HyperlinkedModelSerializer):

    class Meta:
        model = get_user_model()
        fields = [
            "url",
            "email",
            "first_name",
            "last_name",
            "gender",
            "age",
            "description",
            "last_seen_lat",
            "last_seen_long",
            "looking_for",
            "age_from",
            "age_to",
            "search_radius",
            "photo0",
            "photo1",
            "photo2",
            "photo3",
            "photo4",
            "photo5",
            "photo6",
            "photo7",
            "photo8",
            "photo9",
        ]

class PokemonSerializer(serializers.HyperlinkedModelSerializer):

    class Meta:
        model = Pokemon
        fields = [
            "url",
            "name",
            "pokedex",
            "leveling_rate",
            "species",
            "catch_rate",
            "generation",
            "gender_ratio",
            "hp",
            "attack",
            "defence",
            "sp_atk",
            "sp_def",
            "speed",
            "type0",
            "type1",
            "height",
            "weight",
            "bmi",
            "prevolution",
            "evolution",
            "img",
        ]