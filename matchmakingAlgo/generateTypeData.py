import random
import json

typeList = ["Normal", "Fight", "Flying", "Poison", "Ground", "Rock", "Bug", "Ghost", "Steel", "Fire", "Water", "Grass", "Electric", "Psychic", "Ice", "Dragon" ,"Dark",  "Fairy"]
userList = ["A1", "A2", "A3", "A4", "A5"]

def generateType():
    types = {typeName: random.random() for typeName in typeList}

    tot = sum(types.values())
    for key in types:
        types[key] /= tot
    return types


with open("userdata.json", "w+") as typeFile:
    users = [{"Profile": {"Name": user}, "Type": generateType()} for user in userList] 
    typeFile.write(json.dumps(users))