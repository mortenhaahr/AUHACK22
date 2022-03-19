from django.db import models
from django.core.validators import MaxValueValidator, MinValueValidator

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
    type_normal = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_flying = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_fire = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_psychic = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_water = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_bug = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_grass = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_rock = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_electric = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_ghost = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_ice = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_dark = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_fighting = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_dragon = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_poison = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_steel = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_ground = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])
    type_fairy = models.FloatField(default=0, validators=[MinValueValidator(0), MaxValueValidator(1)])

    height = models.FloatField()
    weight = models.FloatField()
    hp = models.SmallIntegerField()
    attack = models.SmallIntegerField()
    defence = models.SmallIntegerField()
    sp_atk = models.SmallIntegerField()
    sp_def = models.SmallIntegerField()
    speed = models.SmallIntegerField()
    generation = models.PositiveIntegerField()
    pokedex = models.SmallIntegerField()
    catch_rate = models.PositiveIntegerField()
    species = models.CharField(max_length=255)
    gender_ratio = models.IntegerField() # Ratio of male pokemon. -1 if genderless.
    pokedex_color = models.CharField(max_length=255)
    prevolution = models.CharField(max_length=255)
    evolution = models.CharField(max_length=255)