from django.db import models

class PokeProfile(models.Model):
    type = 0
    height = models.FloatField()
    weight = models.FloatField()
    stats = 0
    generation = models.PositiveIntegerField()
    pokedex = models.SmallIntegerField()
    catch_rate = models.PositiveIntegerField()
    species = models.CharField(max_length=255)
    gender_ratio = models.IntegerField() # Ratio of male pokemon. -1 if genderless.
    pokedex_color = models.CharField(max_length=255)
    prevolution = models.CharField(max_length=255)
    evolution = models.CharField(max_length=255)