from django.contrib.auth import get_user_model
from rest_framework import serializers, fields

from user.choices import GENDER

class UserSerializer(serializers.HyperlinkedModelSerializer):
    looking_for = fields.MultipleChoiceField(choices=GENDER.GENDER_CHOICES)

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
