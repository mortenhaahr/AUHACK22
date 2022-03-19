
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

def getMean(weights, SmashedPokemons, key):
	mean = 0
	for i, smashed in enumerate(SmashedPokemons.values()):
		mean += smashed[key]*weights[i]

	return mean / len(SmashedPokemons)

def getMeanHeight(SmashedPokemons):
	return getMean(SmashedPokemons, "Height")

def getMeanWeight(SmashedPokemons):
	return getMean(SmashedPokemons, "Weight")

def getMeanStats(weights, SmashedPokemons):

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
			ret[key] += smashed["Stats"][key] *weights[i]

	for keys in ret:
		ret[keys] /= len(SmashedPokemons)

	return ret

def getMeanGeneration(SmashedPokemons):
	return getMean(SmashedPokemons, "Generation")

def getMeanCatchRate(SmashedPokemons):
	return getMean(SmashedPokemons, "Catch rate")

def countOccurredString(weights, SmashedPokemons, key):
	ret = {}
	for i, smashed in enumerate(SmashedPokemons.values()):

		if not isinstance(smashed[key], str):
			raise KeyError(f"Key {key} does not contain a string!")

		occuranceKey = smashed[key]
		if occuranceKey in ret:
			ret[occuranceKey] += 1*weights[i]
		else:
			ret[occuranceKey] = 1*weights[i]
	
	return ret

def countOccurancesInList(weights, SmashedPokemons, key):
	ret = {}
	for i, smashed in enumerate(SmashedPokemons.values()):

		if not isinstance(smashed[key], list):
			raise KeyError(f"Key {key} is not a list!")

		for item in smashed[key]:
			if item in ret:
				ret[item] += 1*weights[i]
			else:
				ret[item] = 1*weights[i]
	return ret


def getProfile(SmashedPokemons, weights):

	if sum(weights) != 1:
		raise ValueError("Total weight sum is not 1!!!")

	return {
		"Type": countOccurancesInList(weights, SmashedPokemons, "Type"),
		"Height": getMean(weights, SmashedPokemons, "Height"),
		"Weight": getMean(weights, SmashedPokemons, "Weight"),
		"Leveling rate": countOccurredString(weights, SmashedPokemons, "Leveling rate"),
		"Stats": getMeanStats(weights, SmashedPokemons),
		"Generation": getMean(weights, SmashedPokemons, "Generation"),
		"Pokedex": ",".join(str(x["Pokedex"]) for x in SmashedPokemons.values()),
		"Catch rate": getMean(weights, SmashedPokemons, "Catch rate"),
		"Species": countOccurredString(weights, SmashedPokemons, "Species"),
		"BMI": getMean(weights, SmashedPokemons, "BMI"),
		"Gender ratio": getMean(weights, SmashedPokemons, "Gender ratio")
	}
	
print(f"CountOccured: {getProfile(Pokemon_test, [0.80,0.20])}")