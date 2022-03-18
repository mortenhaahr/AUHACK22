# Generated by Django 4.0.3 on 2022-03-18 22:24

import django.core.validators
from django.db import migrations, models
import django.utils.timezone
import multiselectfield.db.fields
import user.models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        ('auth', '0012_alter_user_first_name_max_length'),
    ]

    operations = [
        migrations.CreateModel(
            name='User',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('password', models.CharField(max_length=128, verbose_name='password')),
                ('last_login', models.DateTimeField(blank=True, null=True, verbose_name='last login')),
                ('is_superuser', models.BooleanField(default=False, help_text='Designates that this user has all permissions without explicitly assigning them.', verbose_name='superuser status')),
                ('first_name', models.CharField(blank=True, max_length=150, verbose_name='first name')),
                ('last_name', models.CharField(blank=True, max_length=150, verbose_name='last name')),
                ('is_staff', models.BooleanField(default=False, help_text='Designates whether the user can log into this admin site.', verbose_name='staff status')),
                ('is_active', models.BooleanField(default=True, help_text='Designates whether this user should be treated as active. Unselect this instead of deleting accounts.', verbose_name='active')),
                ('date_joined', models.DateTimeField(default=django.utils.timezone.now, verbose_name='date joined')),
                ('email', models.EmailField(max_length=254, unique=True, verbose_name='email address')),
                ('gender', models.PositiveSmallIntegerField(choices=[(0, 'Other'), (1, 'Male'), (2, 'Female')], default=0)),
                ('age', models.PositiveSmallIntegerField(default=18, validators=[django.core.validators.MinValueValidator(18), django.core.validators.MaxValueValidator(150)])),
                ('description', models.CharField(max_length=1024)),
                ('last_seen_lat', models.FloatField(default=0, validators=[django.core.validators.MinValueValidator(-90), django.core.validators.MaxValueValidator(90)])),
                ('last_seen_long', models.FloatField(default=0, validators=[django.core.validators.MinValueValidator(-180), django.core.validators.MaxValueValidator(180)])),
                ('looking_for', multiselectfield.db.fields.MultiSelectField(choices=[(0, 'Other'), (1, 'Male'), (2, 'Female')], max_length=5)),
                ('age_from', models.PositiveSmallIntegerField(default=18, validators=[django.core.validators.MinValueValidator(18), django.core.validators.MaxValueValidator(150)])),
                ('age_to', models.PositiveSmallIntegerField(default=30, validators=[django.core.validators.MinValueValidator(18), django.core.validators.MaxValueValidator(150)])),
                ('search_radius', models.PositiveIntegerField(default=805)),
                ('photo0', models.ImageField(upload_to=user.models.user_directory_path)),
                ('photo1', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('photo2', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('photo3', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('photo4', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('photo5', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('photo6', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('photo7', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('photo8', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('photo9', models.ImageField(blank=True, upload_to=user.models.user_directory_path)),
                ('groups', models.ManyToManyField(blank=True, help_text='The groups this user belongs to. A user will get all permissions granted to each of their groups.', related_name='user_set', related_query_name='user', to='auth.group', verbose_name='groups')),
                ('user_permissions', models.ManyToManyField(blank=True, help_text='Specific permissions for this user.', related_name='user_set', related_query_name='user', to='auth.permission', verbose_name='user permissions')),
            ],
            options={
                'verbose_name': 'user',
                'verbose_name_plural': 'users',
            },
        ),
    ]