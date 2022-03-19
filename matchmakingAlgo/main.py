from asyncio.base_subprocess import ReadSubprocessPipeProto
from pydoc import doc
from readline import read_init_file
import matching
import json

def main():
    steffen = readProfile("Steffen.json")
    stina = readProfile("Stina.json")

    steffen.retrieveMatches("userdata.json")

    print(steffen.printAll())

    steffen.rateTopMatch(False)

    print(steffen.printAll())



def readProfile(path):
    with open(path, "r") as typeFile:
        data = json.load(typeFile)
        return matching.Profile({"Name": path}, data)

if __name__ == "__main__":
    main()