from django.conf import settings
from django.contrib.auth import get_user_model
from django.core.files import File
from django.core.management import call_command
from django.core.management.base import BaseCommand, CommandError
from django.db.utils import IntegrityError
import shutil
import os

class Command(BaseCommand):
    help = ''

    def handle(self, *args, **options):
        from user.test_user import users
        self.stdout.write("Creating test users.")
        try:
                os.makedirs(str(settings.MEDIA_ROOT) + '/user')
        except FileExistsError:
            pass
        shutil.copyfile('user/test_user_images/default_monkey.png', str(settings.MEDIA_ROOT) + '/user/default_monkey.png')

        for user in users:
            try:
                password = get_user_model().objects.make_random_password()
                photo_path = user.pop('photo_path')
                user_obj = get_user_model().objects.create_user(**user, password=password)
                try:
                     os.makedirs(str(settings.MEDIA_ROOT) + '/user' + photo_path)
                except FileExistsError:
                    pass
                for i in range(0, 10):
                    try:
                        path = user.pop(f'photo{i}')
                    except KeyError:
                        break
                    cp_path = str(settings.MEDIA_ROOT) + '/user' + photo_path + path
                    old_path = 'user/test_user_images/' + photo_path + path
                    shutil.copyfile(old_path, cp_path)
                    setattr(user_obj, f'photo{i}', 'user/' + photo_path + path)
                    user_obj.save()
            except IntegrityError as e:
                self.stdout.write(self.style.ERROR('Error. Most likely a user is already registered by that name.'))
                print(e)
                return

        self.stdout.write(self.style.SUCCESS('Singles near you (created test users)'))
        return