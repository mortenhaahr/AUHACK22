from django.conf import settings
from django.contrib.auth.models import User
from django.core.management import call_command
from django.core.management.base import BaseCommand, CommandError
from django.db.utils import IntegrityError
import os
import shutil

class Command(BaseCommand):
    help = 'Prepares the DB after a flush'

    def handle(self, *args, **options):
        self.stdout.write("Preparing to flush all. That means flushing the database, removing the media folder and deleting previous migrations.")
        self.stdout.write(self.style.NOTICE('Are you sure you wish to remove all data[y/N]'))
        answer=""
        while(not (answer == "y" or answer == "n")):
            answer = str(input('')).lower().strip()
        if(answer == "n"):
            self.stdout.write(self.style.ERROR("Aborting"))
            return

        # call_command("flush", "--no-input") # Doesn't actually delete the tables...        
        if os.path.isfile(str(settings.BASE_DIR) + "/db.sqlite3"):
            self.stdout.write(f"Removing DB folder {str(settings.BASE_DIR) + '/db.sqlite3'}")
            os.remove(str(settings.BASE_DIR) + "/db.sqlite3")
        
        if os.path.isdir(settings.MEDIA_ROOT):
            self.stdout.write(f"Removing media folder {settings.MEDIA_ROOT}")
            shutil.rmtree(settings.MEDIA_ROOT)

        migration_dirs = [
            str(settings.BASE_DIR) + "/user/migrations",
            str(settings.BASE_DIR) + "/rest/migrations",
            str(settings.BASE_DIR) + "/poke_profile/migrations",
        ]
        for dir in migration_dirs:
            if os.path.isdir(dir):
                self.stdout.write(f"Removing migrations folder {dir}")
                shutil.rmtree(dir)

        self.stdout.write(self.style.NOTICE('Do you want to run prepare_db now? [y/N]'))
        answer=""
        while(not (answer == "y" or answer == "n")):
            answer = str(input('')).lower().strip()
        if(answer == "n"):
            self.stdout.write(self.style.SUCCESS("Done"))
            return
        
        call_command("prepare_db")
        self.stdout.write(self.style.SUCCESS("Done"))
    
        return
