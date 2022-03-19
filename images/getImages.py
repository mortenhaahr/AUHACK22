
import requests # to get image from the web
import shutil # to save it locally
import csv
def downloadAndSave(image_url, filename):

    # Open the url image, set stream to True, this will return the stream content.
    r = requests.get(image_url, stream = True)

    # Check if the image was retrieved successfully
    if r.status_code == 200:
        # Set decode_content value to True, otherwise the downloaded image file's size will be zero.
        r.raw.decode_content = True
        
        # Open a local file with wb ( write binary ) permission.
        with open(filename,'wb') as f:
            shutil.copyfileobj(r.raw, f)
            
        print('Image sucessfully Downloaded: ',filename)
    else:
        print('Image Couldn\'t be retreived')
        with open("error.log", "a") as errf:
            errf.writelines(image_url)
"https://img.pokemondb.net/sprites/sword-shield/icon/magnemite.png"
"https://img.pokemondb.net/artwork/meowth.jpg"
def getPokemonImages(pokemon):
    pokemon = pokemon.lower().replace("'", "").replace(".", "").replace(" ", "-")
    downloadAndSave(f"https://img.pokemondb.net/sprites/sword-shield/icon/{pokemon}.png",f"./sprites/{pokemon}.png")
    downloadAndSave(f"https://img.pokemondb.net/artwork/large/{pokemon}.jpg",f"./artwork/{pokemon}.jpg")

with open("../dataFetcher/Stats.csv", "r") as f:
    reader = csv.reader(f)
    for num, row in enumerate(reader):
        if num == 0:
            continue
        getPokemonImages(row[1])

