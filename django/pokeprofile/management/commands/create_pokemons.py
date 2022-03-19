from django.core.management.base import BaseCommand
from django.utils import timezone

from pokeprofile.models import Pokemon


class Command(BaseCommand):
    help = "Creates a Teamhjulet project for the default Partner and answers it for a few of the Respondents."

    def handle(self, *args, **options):
        """
        Init for the Consultant detail project. Creates a Partner, Questions, Project, Respondents and Answers.
        All linked together.
        """
        from pokeprofile.data import pokemons

        for name, pokemon in pokemons.items():
            Pokemon.objects.create(
                name=name,
                pokedex=pokemon['Pokedex'],
                leveling_rate=pokemon['Leveling rate'],
                species=pokemon['Species'],
                catch_rate=pokemon['Catch rate'],
                gender_ratio=pokemon['Gender ratio'],
                hp=pokemon['Stats']['HP'],
                attack=pokemon['Stats']['Attack'],
                defence=pokemon['Stats']['Defence'],
                sp_atk=pokemon['Stats']['Sp. Atk'],
                sp_def=pokemon['Stats']['Sp. Def'],
                speed=pokemon['Stats']['Speed'],
                type0=pokemon['Type'][0],
                type1=pokemon['Type'][1],
                height=pokemon['Height'],
                weight=pokemon['Weight'],
                bmi=pokemon['BMI'],
                prevolution=pokemon['Evolution']['Pre'],
                evolution=pokemon['Evolution']['Post'],
                img=pokemon['Img'],
            )

        self.stdout.write(
            self.style.SUCCESS("Caught them all (created Pokemons)")
        )
        return
