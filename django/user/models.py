from django.contrib.auth.models import AbstractUser
from django.core.validators import MaxValueValidator, MinValueValidator
from django.db import models

from user.choices import GENDER
from user.managers import UserManager

def user_directory_path(instance, filename):
    # file will be uploaded to MEDIA_ROOT/beat/author/<filename>
    return f'user/{instance.author}/{filename}'

class User(AbstractUser):
    username = None
    email = models.EmailField('email address', unique=True)
    USERNAME_FIELD = 'email'
    REQUIRED_FIELDS = []

    objects = UserManager()

    gender = models.PositiveSmallIntegerField(
        choices=GENDER.GENDER_CHOICES, default=GENDER.OTHER
    )
    age = models.PositiveSmallIntegerField(default=18, validators=[MinValueValidator(18), MaxValueValidator(150)])
    
    description = models.CharField(max_length=1024)
    last_seen_lat = models.FloatField(validators=[MinValueValidator(-90), MaxValueValidator(90)], default=0)
    last_seen_long = models.FloatField(validators=[MinValueValidator(-180), MaxValueValidator(180)], default=0)

    looking_for = 0
    age_from = models.PositiveSmallIntegerField(default=18, validators=[MinValueValidator(18), MaxValueValidator(150)])
    age_to = models.PositiveSmallIntegerField(default=30, validators=[MinValueValidator(18), MaxValueValidator(150)])
    search_radius = models.PositiveIntegerField(default=805)

    photo0 = models.ImageField(upload_to=user_directory_path) 
    photo1 = models.ImageField(upload_to=user_directory_path, blank=True) 
    photo2 = models.ImageField(upload_to=user_directory_path, blank=True) 
    photo3 = models.ImageField(upload_to=user_directory_path, blank=True) 
    photo4 = models.ImageField(upload_to=user_directory_path, blank=True) 
    photo5 = models.ImageField(upload_to=user_directory_path, blank=True) 
    photo6 = models.ImageField(upload_to=user_directory_path, blank=True) 
    photo7 = models.ImageField(upload_to=user_directory_path, blank=True) 
    photo8 = models.ImageField(upload_to=user_directory_path, blank=True) 
    photo9 = models.ImageField(upload_to=user_directory_path, blank=True) 

    class Meta:
        verbose_name = "user"
        verbose_name_plural = "users"
