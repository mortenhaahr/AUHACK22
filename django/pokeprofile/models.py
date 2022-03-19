from django.db import models

class Pokemon(models.Model):
    name = models.CharField(max_length=255)
    pokedex = models.PositiveIntegerField()
    leveling_rate = models.CharField(max_length=255)
    species = models.CharField(max_length=255)
    catch_rate = models.SmallIntegerField()
    gender_ratio = models.FloatField() # Ratio of male pokemon. -1 if genderless.
    hp = models.SmallIntegerField()
    attack = models.SmallIntegerField()
    defence = models.SmallIntegerField()
    sp_atk = models.SmallIntegerField()
    sp_def = models.SmallIntegerField()
    speed = models.SmallIntegerField()
    type0 = models.CharField(max_length=255)
    type1 = models.CharField(max_length=255)
    height = models.FloatField()
    weight = models.FloatField()
    bmi = models.FloatField()
    prevolution = models.CharField(max_length=255, null=True)
    evolution = models.CharField(max_length=255, null=True)
    img = models.URLField(max_length = 512)

    def __str__(self):
        return f"{self.name}"

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