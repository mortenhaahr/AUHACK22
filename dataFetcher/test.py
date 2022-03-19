from encodings import utf_8
import http.client
import re
import csv
import json


def findData(match, data):
    d = data.split(match)[1][0:200]
    d = d.split(">")[1].split("<")[0]
    return d.lstrip().rstrip()


def getPokemonData(pokemon):
    print(pokemon)
    conn = http.client.HTTPSConnection("pokemondb.net", 443)
    conn.putrequest(
        "GET", "/pokedex/" + pokemon.replace("'", "").replace(".", "").replace(" ", "-")
    )
    conn.endheaders()  # <---
    r = conn.getresponse()
    d = [k for k in r.read()]
    data = bytes(d).decode("utf_8").replace("é", "e")

    dataDict = {
        "Leveling rate": findData("<th>Growth Rate</th>", data),
        "Species": findData("<th>Species</th>", data),
    }
    try:
        dataDict["Catch rate"] = int(findData("<th>Catch rate</th>", data))
    except ValueError:
        dataDict["Catch rate"] = -1
    try:
        dataDict["Generation"] = int(
            findData('<li class="list-nav-title">In other generations</li><li>', data)
        )
    except IndexError:
        dataDict["Generation"] = 8
    try:
        try:
            dataDict["Gender ratio"] = float(
                findData("<th>Gender</th>\n<td>", data).split("%")[0].replace(",", "")
            )
        except IndexError:
            dataDict["Gender ratio"] = float(
                findData("<th>Gender</th>\n <td>", data).split("%")[0].replace(",", "")
            )
    except ValueError:
        dataDict["Gender ratio"] = -1.0
    return dataDict


with open("Stats.csv", "r") as f:
    reader = csv.reader(f)
    try:
        with open("data.json") as json_file:
            data = json.load(json_file)
    except FileNotFoundError:
        data = {}
    for num, row in enumerate(reader):
        if num == 0:
            continue
        if row[1] in data.keys() or row[1].find("\n") != -1 or row[1] in data.keys():
            continue
        data[row[1]] = getPokemonData(row[1].lower())
        data[row[1]]["Stats"] = {
            "HP": int(row[4]),
            "Attack": int(row[5]),
            "Defence": int(row[6]),
            "Sp. Atk": int(row[7]),
            "Sp. Def": int(row[8]),
            "Speed": int(row[9]),
        }
        data[row[1]]["Pokedex"] = row[0]
        data[row[1]]["Type"] = [x.lstrip().rstrip() for x in row[2].split("\n")]
        with open("SizeWeight.csv", "r") as sw:
            swreader = csv.reader(sw)
            for swrow in swreader:
                if swrow[1] != row[1]:
                    continue
                data[row[1]]["Height"] = float(swrow[4].replace(",", ""))
                data[row[1]]["Weight"] = float(swrow[6].replace(",", ""))
                data[row[1]]["BMI"] = float(swrow[7].replace(",", ""))
        data[row[1]]["Evolution"] = {"Pre": None, "Post": None}
        with open("Evolusiontable.csv", "r") as et:
            find = False
            etreader = csv.reader(et)
            for etrow in etreader:
                for num, etcell in enumerate(etrow):
                    if etcell != row[1]:
                        continue
                    if num > 1 and len(etrow[num - 1]) > 0:
                        data[row[1]]["Evolution"]["Pre"] = etrow[num - 1]
                    if num < 3 and len(etrow[num + 1]) > 0:
                        data[row[1]]["Evolution"]["Post"] = etrow[num + 1]
                    find = True
                    break
                if find:
                    break
        data[row[1]]["Img"] = (
            "https://img.pokemondb.net/artwork/" + row[1].lower() + ".jpg"
        )
        with open("data.json", "w") as fp:
            json.dump(data, fp)

# with open("test.html","w") as f:
#     f.write(data)
