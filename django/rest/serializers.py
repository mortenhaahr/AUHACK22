from django.contrib.auth import get_user_model
from django.core.exceptions import ValidationError
from rest_framework import serializers, fields

from poke_profile.models import PokeProfile, Pokemon
from user.choices import GENDER

from rest_framework import serializers    

class Base64ImageField(serializers.ImageField):
    """
    A Django REST framework field for handling image-uploads through raw post data.
    It uses base64 for encoding and decoding the contents of the file.

    Heavily based on
    https://github.com/tomchristie/django-rest-framework/pull/1268

    Updated for Django REST framework 3.
    """

    def to_internal_value(self, data):
        from django.core.files.base import ContentFile
        import base64
        import six
        import uuid

        # Check if this is a base64 string
        if isinstance(data, six.string_types):
            # Check if the base64 string is in the "data:" format
            if 'data:' in data and ';base64,' in data:
                # Break out the header from the base64 content
                header, data = data.split(';base64,')

            # Try to decode the file. Return validation error if it fails.
            try:
                decoded_file = base64.b64decode(data)
            except TypeError:
                self.fail('invalid_image')

            # Generate file name:
            file_name = str(uuid.uuid4())[:12] # 12 characters are more than enough.
            # Get the file name extension:
            file_extension = self.get_file_extension(file_name, decoded_file)

            complete_file_name = "%s.%s" % (file_name, file_extension, )

            data = ContentFile(decoded_file, name=complete_file_name)

        return super(Base64ImageField, self).to_internal_value(data)

    def get_file_extension(self, file_name, decoded_file):
        import imghdr

        extension = imghdr.what(file_name, decoded_file)
        extension = "jpg" if extension == "jpeg" else extension

        return extension

class UserSerializer(serializers.HyperlinkedModelSerializer):
    looking_for = fields.MultipleChoiceField(choices=GENDER.GENDER_CHOICES)
    photo0 = Base64ImageField(
        max_length=None, use_url=True
    )

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