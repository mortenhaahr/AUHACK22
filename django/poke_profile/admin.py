from django.contrib import admin
from poke_profile.models import Pokemon
from user.models import User

admin.site.register(Pokemon)
admin.site.register(User)