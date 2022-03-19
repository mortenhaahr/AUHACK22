import json
data = {}
with open("pokemons.compact.json", "r") as j:
    data = json.load(j)
for key, value in data.items():
    if(len(value["Type"]) == 1):
        value["Type"].append(value["Type"][0])
        
with open("pokemons.compact.fixed.json", "w") as fp:
    json.dump(data, fp)