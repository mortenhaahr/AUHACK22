
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
	return ret


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