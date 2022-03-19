import json
data = {}
with open("pokemons.compact.fixed.json", "r") as j:
    data = json.load(j)
newData = []
for key, value in data.items():
    value["Name"] = key
    newData.append(value)
with open("pokemons.compact.csharp.json", "w") as jw:
    json.dump(newData,jw)
