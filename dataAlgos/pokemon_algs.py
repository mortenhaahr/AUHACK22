
Pokemon_test = [
    {
        "id": 1,
        "name": "Bulbasaur",
        "pokedex": 1,
        "leveling_rate": "Medium Slow",
        "species": "Seed Pokemon",
        "catch_rate": 45,
        "generation": 1,
        "gender_ratio": 87.5,
        "hp": 45,
        "attack": 49,
        "defence": 49,
        "sp_atk": 65,
        "sp_def": 65,
        "speed": 45,
        "type0": "Grass",
        "type1": "Poison",
        "height": 0.7,
        "weight": 6.9,
        "bmi": 14.1,
        "prevolution": None,
        "evolution": "Ivysaur",
        "img": "https://img.pokemondb.net/artwork/bulbasaur.jpg"
    },
    {
        "id": 4,
        "name": "Charmander",
        "pokedex": 4,
        "leveling_rate": "Medium Slow",
        "species": "Lizard Pokemon",
        "catch_rate": 45,
        "generation": 1,
        "gender_ratio": 87.5,
        "hp": 39,
        "attack": 52,
        "defence": 43,
        "sp_atk": 60,
        "sp_def": 50,
        "speed": 65,
        "type0": "Fire",
        "type1": "Fire",
        "height": 0.6,
        "weight": 8.5,
        "bmi": 23.6,
        "prevolution": None,
        "evolution": "Charmeleon",
        "img": "https://img.pokemondb.net/artwork/charmander.jpg"
    }
]

def getMean(SmashedPokemons, key):
	mean = 0
	for smashed in SmashedPokemons:
		mean += smashed[key]

	return mean / len(SmashedPokemons)

def getMeanHeight(SmashedPokemons):
	return getMean(SmashedPokemons, "Height")

def getMeanWeight(SmashedPokemons):
	return getMean(SmashedPokemons, "Weight")

def getMeanStats(SmashedPokemons):

	stat_keys = ["hp", "attack", "defence", "sp_atk", "sp_def", "speed"]
	ret = {}

	for key in stat_keys:
		if key not in ret:
			ret[key] = 0;

	for smashed in SmashedPokemons:
		for key in stat_keys:
			ret[key] += smashed[key] / len(SmashedPokemons)

	return ret

def getMeanGeneration(SmashedPokemons):
	return getMean(SmashedPokemons, "Generation")

def getMeanCatchRate(SmashedPokemons):
	return getMean(SmashedPokemons, "Catch rate")

def countOccurredString(SmashedPokemons, key):
	ret = {}
	for smashed in SmashedPokemons:

		if not isinstance(smashed[key], str):
			raise KeyError(f"Key {key} does not contain a string!")

		occuranceKey = smashed[key]
		if occuranceKey in ret:
			ret[occuranceKey] += 1 / len(SmashedPokemons)
		else:
			ret[occuranceKey] = 1 / len(SmashedPokemons)
	
	return ret



def getTypes(SmashedPokemons):
	ret = {}
	for smashed in SmashedPokemons:
		for type in ["type0", "type1"]:
			if smashed[type] in ret:
				ret[smashed[type]] += 1 / (2*len(SmashedPokemons))
			else:
				ret[smashed[type]] = 1 / (2*len(SmashedPokemons))
	

	return ret;


def getProfile(SmashedPokemons):

	return {
		"Type": getTypes(SmashedPokemons),
		"Height": getMean(SmashedPokemons, "height"),
		"Weight": getMean(SmashedPokemons, "weight"),
		"Leveling rate": countOccurredString(SmashedPokemons, "leveling_rate"),
		"Stats": getMeanStats(SmashedPokemons),
		"Generation": getMean(SmashedPokemons, "generation"),
		"Pokedex": ",".join(str(x["pokedex"]) for x in SmashedPokemons),
		"Catch rate": getMean(SmashedPokemons, "catch_rate"),
		"Species": countOccurredString(SmashedPokemons, "species"),
		"BMI": getMean(SmashedPokemons, "bmi"),
		"Gender ratio": getMean(SmashedPokemons, "gender_ratio")
	}

profile = getProfile(Pokemon_test)
print(f"CountOccured: {profile}")