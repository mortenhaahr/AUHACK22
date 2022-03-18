from django.conf import settings
from django.contrib.auth import get_user_model
from django.core.management import call_command
from django.core.management.base import BaseCommand, CommandError
from django.db.utils import IntegrityError

class Command(BaseCommand):
    help = 'Prepares the DB after a flush'

    def handle(self, *args, **options):
        self.stdout.write("Preparing database for testing.")
        self.stdout.write("Running makemigrations.")
        call_command("makemigrations")
        call_command("makemigrations", "user") # Hardcoded because otherwise it won't create initial migration.
        self.stdout.write("Running migrate.")
        call_command("migrate")
        
        try:
            user = get_user_model().objects.create_superuser(email="mortenhaahrkristensen@gmail.com", password="1234")
            self.stdout.write(self.style.SUCCESS('Successfully created Superuser. '))
        except IntegrityError as e:
            self.stdout.write(self.style.ERROR('Error. Most likely a superuser is already registered by that name.'))
            print(e)
            return

        self.stdout.write(self.style.SUCCESS('Done preparing db.'))
        return