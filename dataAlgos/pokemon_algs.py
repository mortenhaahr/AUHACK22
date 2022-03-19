
PokemonKeys = [
	"Type", 
	"Height", 
	"Weight", 
	"Leveling rate", 
	"Stats",
	"Generation",
	"Pokedex",
	"Catch rate",
	"Species",
	"Gender ratio",
	"Evolution"]

Pokemon_test = {
	"Bulbasaur" : {
		"Leveling rate": "Medium Slow",
        "Species": "Seed Pokemon",
        "Catch rate": 45,
        "Generation": 1,
        "Gender ratio": 87.5,
        "Stats": {
            "HP": 45,
            "Attack": 49,
            "Defence": 49,
            "Sp. Atk": 65,
            "Sp. Def": 65,
            "Speed": 45
        },
        "Pokedex": "1",
        "Type": [
            "Grass",
            "Poison"
        ],
        "Height": 0.7,
        "Weight": 6.9,
        "BMI": 14.1,
        "Evolution": {
            "Pre": None,
            "Post": "Ivysaur"
        },
	},
	"Eevee" : {
		"Type": ["Grass"],
		"Height": 40,
		"Weight": 10,
		"Leveling rate": "Slow",
		"Stats": {
			"HP": 100,
			"Attack": 1,
			"Defence": 0,
			"Sp. Atk": 1,
			"Sp. Def": 2,
			"Speed": 1000,
		},
		"Generation": 0,
		"Pokedex": 1000,
		"Catch rate": 60,
		"Species": "Some group",
		"Gender ratio": 50,
		"BMI": 90,
		"Evolution" : {
			"Pre": None,
			"Post": None,
		}
	}
}

def getMean(SmashedPokemons, key):
	mean = 0
	for i, smashed in enumerate(SmashedPokemons.values()):
		mean += smashed[key]

	return mean / len(SmashedPokemons)

def getMeanHeight(SmashedPokemons):
	return getMean(SmashedPokemons, "Height")

def getMeanWeight(SmashedPokemons):
	return getMean(SmashedPokemons, "Weight")

def getMeanStats(SmashedPokemons):

	ret = {
		"HP": 0,
		"Attack": 0, 
		"Defence": 0,
		"Sp. Atk": 0,
		"Sp. Def": 0,
		"Speed": 0
	}

	for i, smashed in enumerate(SmashedPokemons.values()):
		for key in ret.keys():
			ret[key] += smashed["Stats"][key]

	for keys in ret:
		ret[keys] /= len(SmashedPokemons)

	return ret

def getMeanGeneration(SmashedPokemons):
	return getMean(SmashedPokemons, "Generation")

def getMeanCatchRate(SmashedPokemons):
	return getMean(SmashedPokemons, "Catch rate")

def countOccurredString(SmashedPokemons, key):
	ret = {}
	for i, smashed in enumerate(SmashedPokemons.values()):

		if not isinstance(smashed[key], str):
			raise KeyError(f"Key {key} does not contain a string!")

		occuranceKey = smashed[key]
		if occuranceKey in ret:
			ret[occuranceKey] += 1
		else:
			ret[occuranceKey] = 1
	
	return ret

def countOccurancesInList(SmashedPokemons, key):
	ret = {}
	for i, smashed in enumerate(SmashedPokemons.values()):

		if not isinstance(smashed[key], list):
			raise KeyError(f"Key {key} is not a list!")

		for item in smashed[key]:
			if item in ret:
				ret[item] += 1
			else:
				ret[item] = 1
	return ret


def getProfile(SmashedPokemons):

	return {
		"Type": countOccurancesInList(SmashedPokemons, "Type"),
		"Height": getMean(SmashedPokemons, "Height"),
		"Weight": getMean(SmashedPokemons, "Weight"),
		"Leveling rate": countOccurredString(SmashedPokemons, "Leveling rate"),
		"Stats": getMeanStats(SmashedPokemons),
		"Generation": getMean(SmashedPokemons, "Generation"),
		"Pokedex": ",".join(str(x["Pokedex"]) for x in SmashedPokemons.values()),
		"Catch rate": getMean(SmashedPokemons, "Catch rate"),
		"Species": countOccurredString(SmashedPokemons, "Species"),
		"BMI": getMean(SmashedPokemons, "BMI"),
		"Gender ratio": getMean(SmashedPokemons, "Gender ratio")
	}
	
print(f"CountOccured: {getProfile(Pokemon_test)}")