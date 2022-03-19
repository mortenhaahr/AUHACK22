from django.contrib.auth import get_user_model
from django.core.exceptions import ValidationError
from rest_framework import serializers, fields

from poke_profile.models import PokeProfile, Pokemon
from user.choices import GENDER

class UserSerializer(serializers.HyperlinkedModelSerializer):
    looking_for = fields.MultipleChoiceField(choices=GENDER.GENDER_CHOICES)

    class Meta:
        model = get_user_model()
        fields = [
            "url",
            "id",
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
            "id",
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

class PokeProfileSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = PokeProfile
        fields = [
            'url',
            'user_id',
            'height',
            'weight',
            'generation',
            'pokedex',
            'catch_rate',
            'bmi',
            'gender_ratio',
            'hp',
            'attack',
            'defence',
            'sp_atk',
            'sp_def',
            'speed',
            'type_normal',
            'type_flying',
            'type_fire',
            'type_psychic',
            'type_water',
            'type_bug',
            'type_grass',
            'type_rock',
            'type_electric',
            'type_ghost',
            'type_ice',
            'type_dark',
            'type_fighting',
            'type_dragon',
            'type_poison',
            'type_steel',
            'type_ground',
            'type_fairy',
        ]