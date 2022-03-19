import random
import json

# Static modifier for alterating values depending upon actual swiping
modifier = 1

# Dict^2 for holding effectiveness, attack then defence
AttackEffect = { # Steel and Fire are swapped on one axis, wont fix
"Normal"   :  {   "Normal": 2,    "Fight": 2,     "Flying": 2,    "Poison": 2,    "Ground": 2,    "Rock": 1,      "Bug": 2,   "Ghost": 0,     "Steel": 1,     "Fire": 2,      "Water": 2,     "Grass": 2,     "Electric": 2,  "Psychic": 2,   "Ice": 2,   "Dragon": 2,    "Dark": 2,  "Fairy": 2},
"Fight"    :  {   "Normal": 4,    "Fight": 2,     "Flying": 1,    "Poison": 1,    "Ground": 2,    "Rock": 4,      "Bug": 1,   "Ghost": 0,     "Steel": 4,     "Fire": 2,      "Water": 2,     "Grass": 2,     "Electric": 2,  "Psychic": 1,   "Ice": 4,   "Dragon": 2,    "Dark": 4,  "Fairy": 1},
"Flying"   :  {   "Normal": 2,    "Fight": 4,     "Flying": 2,    "Poison": 2,    "Ground": 2,    "Rock": 1,      "Bug": 4,   "Ghost": 2,     "Steel": 1,     "Fire": 2,      "Water": 2,     "Grass": 4,     "Electric": 1,  "Psychic": 2,   "Ice": 2,   "Dragon": 2,    "Dark": 2,  "Fairy": 2},
"Poison"   :  {   "Normal": 2,    "Fight": 2,     "Flying": 2,    "Poison": 1,    "Ground": 1,    "Rock": 1,      "Bug": 2,   "Ghost": 1,     "Steel": 0,     "Fire": 2,      "Water": 2,     "Grass": 4,     "Electric": 2,  "Psychic": 2,   "Ice": 2,   "Dragon": 2,    "Dark": 2,  "Fairy": 4},
"Ground"   :  {   "Normal": 2,    "Fight": 2,     "Flying": 0,    "Poison": 4,    "Ground": 2,    "Rock": 4,      "Bug": 1,   "Ghost": 2,     "Steel": 4,     "Fire": 4,      "Water": 2,     "Grass": 1,     "Electric": 4,  "Psychic": 2,   "Ice": 2,   "Dragon": 2,    "Dark": 2,  "Fairy": 2},
"Rock"     :  {   "Normal": 2,    "Fight": 1,     "Flying": 4,    "Poison": 2,    "Ground": 1,    "Rock": 2,      "Bug": 4,   "Ghost": 2,     "Steel": 1,     "Fire": 4,      "Water": 2,     "Grass": 2,     "Electric": 2,  "Psychic": 2,   "Ice": 4,   "Dragon": 2,    "Dark": 2,  "Fairy": 2},
"Bug"      :  {   "Normal": 2,    "Fight": 1,     "Flying": 1,    "Poison": 1,    "Ground": 2,    "Rock": 2,      "Bug": 2,   "Ghost": 1,     "Steel": 1,     "Fire": 1,      "Water": 2,     "Grass": 4,     "Electric": 2,  "Psychic": 4,   "Ice": 2,   "Dragon": 2,    "Dark": 1,  "Fairy": 2},
"Ghost"    :  {   "Normal": 0,    "Fight": 2,     "Flying": 2,    "Poison": 2,    "Ground": 2,    "Rock": 2,      "Bug": 2,   "Ghost": 4,     "Steel": 2,     "Fire": 2,      "Water": 2,     "Grass": 2,     "Electric": 2,  "Psychic": 4,   "Ice": 2,   "Dragon": 2,    "Dark": 1,  "Fairy": 2},
"Steel"    :  {   "Normal": 2,    "Fight": 2,     "Flying": 2,    "Poison": 2,    "Ground": 2,    "Rock": 4,      "Bug": 2,   "Ghost": 2,     "Steel": 2,     "Fire": 1,      "Water": 1,     "Grass": 2,     "Electric": 1,  "Psychic": 2,   "Ice": 4,   "Dragon": 2,    "Dark": 2,  "Fairy": 4},
"Fire"     :  {   "Normal": 2,    "Fight": 2,     "Flying": 2,    "Poison": 2,    "Ground": 2,    "Rock": 1,      "Bug": 4,   "Ghost": 2,     "Steel": 1,     "Fire": 1,      "Water": 1,     "Grass": 4,     "Electric": 2,  "Psychic": 2,   "Ice": 4,   "Dragon": 1,    "Dark": 2,  "Fairy": 2},
"Water"    :  {   "Normal": 2,    "Fight": 2,     "Flying": 2,    "Poison": 1,    "Ground": 4,    "Rock": 4,      "Bug": 1,   "Ghost": 2,     "Steel": 4,     "Fire": 4,      "Water": 1,     "Grass": 1,     "Electric": 2,  "Psychic": 2,   "Ice": 2,   "Dragon": 1,    "Dark": 2,  "Fairy": 2},
"Grass"    :  {   "Normal": 2,    "Fight": 2,     "Flying": 1,    "Poison": 2,    "Ground": 4,    "Rock": 4,      "Bug": 4,   "Ghost": 2,     "Steel": 1,     "Fire": 1,      "Water": 4,     "Grass": 1,     "Electric": 2,  "Psychic": 2,   "Ice": 2,   "Dragon": 1,    "Dark": 2,  "Fairy": 2},
"Electric" :  {   "Normal": 2,    "Fight": 2,     "Flying": 4,    "Poison": 4,    "Ground": 0,    "Rock": 2,      "Bug": 4,   "Ghost": 2,     "Steel": 2,     "Fire": 2,      "Water": 4,     "Grass": 1,     "Electric": 1,  "Psychic": 2,   "Ice": 2,   "Dragon": 1,    "Dark": 2,  "Fairy": 2},
"Psychic"  :  {   "Normal": 2,    "Fight": 4,     "Flying": 2,    "Poison": 2,    "Ground": 2,    "Rock": 2,      "Bug": 2,   "Ghost": 2,     "Steel": 1,     "Fire": 2,      "Water": 2,     "Grass": 2,     "Electric": 2,  "Psychic": 1,   "Ice": 2,   "Dragon": 2,    "Dark": 0,  "Fairy": 2},
"Ice"      :  {   "Normal": 2,    "Fight": 2,     "Flying": 4,    "Poison": 2,    "Ground": 4,    "Rock": 2,      "Bug": 2,   "Ghost": 2,     "Steel": 1,     "Fire": 1,      "Water": 1,     "Grass": 4,     "Electric": 2,  "Psychic": 2,   "Ice": 1,   "Dragon": 4,    "Dark": 2,  "Fairy": 2},
"Dragon"   :  {   "Normal": 2,    "Fight": 2,     "Flying": 2,    "Poison": 2,    "Ground": 2,    "Rock": 2,      "Bug": 2,   "Ghost": 2,     "Steel": 1,     "Fire": 2,      "Water": 2,     "Grass": 2,     "Electric": 2,  "Psychic": 2,   "Ice": 2,   "Dragon": 4,    "Dark": 2,  "Fairy": 0},
"Dark"     :  {   "Normal": 2,    "Fight": 1,     "Flying": 2,    "Poison": 2,    "Ground": 2,    "Rock": 2,      "Bug": 2,   "Ghost": 4,     "Steel": 2,     "Fire": 2,      "Water": 2,     "Grass": 2,     "Electric": 2,  "Psychic": 4,   "Ice": 2,   "Dragon": 2,    "Dark": 1,  "Fairy": 1},
"Fairy"    :  {   "Normal": 2,    "Fight": 4,     "Flying": 2,    "Poison": 1,    "Ground": 2,    "Rock": 2,      "Bug": 2,   "Ghost": 2,     "Steel": 1,     "Fire": 1,      "Water": 2,     "Grass": 2,     "Electric": 2,  "Psychic": 2,   "Ice": 2,   "Dragon": 4,    "Dark": 4,  "Fairy": 2}
}

class Profile():
    """ Class for holding user profile data
    """
    def __init__(self, profile: dict):
        self.profile = profile
        self.type = self.retrieveType()
        self.matches = None
        self.smashed = None

    def retrieveType(self):
        typeDict = {}
        for atkType in AttackEffect.keys():
            typeDict[atkType] = self.profile[f"type_{atkType.lower()}"]
        return typeDict

    def retrieveMatches(self, iteratable):
        self.matches = [Match(user, self) for user in iteratable]
        self.matches.sort(reverse=True)

    def getTopMatch(self, recalculate: bool=True):
        if not self.matches:
            return None
        if recalculate:
            for match in self.matches:
                match.findEffectiveness(self)
            self.matches.sort(reverse=True)
        return self.matches[0]

    def getMatches(self, recalculate: bool=True):
        if not self.matches:
            return None
        if recalculate:
            for match in self.matches:
                match.findEffectiveness(self)
            self.matches.sort(reverse=True)
        return [match.profile["user"] for match in self.matches]

    def rateTopMatch(self, smash: bool):
        """ Function for calculating the change in stats with a given decision
        Args: 
            smash (bool): self-explainatory 
        """
        # Create dict for holding changes
        modifierDict = {atkType: 0 for atkType in AttackEffect}
        for atkType, defValue in AttackEffect.items():
            for defType, effect in defValue.items():
                # Since this is finding out if the "match" has effect upon the user, the match is the attacker
                modifierDict[atkType] += self.type[defType] * self.matches[0].type[atkType] * effect * modifier
        modifierDict = {atkType: x*(1-2*smash) for atkType, x in modifierDict.items()}
        self.type = {atkType: max(value - modifierDict[atkType], 0) for atkType, value in self.type.items()}
        tot = sum(self.type.values())
        self.type = {atkType: value/tot for atkType, value in self.type.items()}

class Match(Profile):
    """ Class for holding values of people other than user
    """
    def __init__(self, profile: dict, main: Profile):
        super().__init__(profile)
        self.findEffectiveness(main)

    def findEffectiveness(self, main: Profile):
        self.attractValue = 0
        for atkType, defValue in AttackEffect.items():
            for defType, effect in defValue.items():
                # Find the effectiveness the match has on the user - so use defence of the main user
                self.attractValue += self.type[atkType] * main.type[defType] * effect

    def __eq__(self, other: "Match"):
        return self.attractValue == other.attractValue

    def __ne__(self, other: "Match"):
        return self.attractValue != other.attractValue

    def __lt__(self, other: "Match"):
        return self.attractValue < other.attractValue

    def __le__(self, other: "Match"):
        return self.attractValue <= other.attractValue

    def __gt__(self, other: "Match"):
        return self.attactValue > other.attractValue

    def __ge__(self, other: "Match"):
        return self.attactValue >= other.attractValue


    

