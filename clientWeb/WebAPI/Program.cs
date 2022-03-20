﻿// See https://aka.ms/new-console-template for more information

using WebAPI;
using System.Text.Json;
using System.Text.Json.Serialization;


var users = await WebClient.Get<User>("users/");
var user = await WebClient.Get<User>("users/", 5);
var profile = await WebClient.Get<PokeProfile>("poke_profiles/");
var candidates = await WebClient.GetCandidates(user.id);
var smases = await WebClient.GetSmash(user.id, 5);

await WebClient.Post<User>("users/", new User { id = user.id, email = "HelloWorld2@gmail.com",
    first_name = "Andreas",
            last_name = "Hansen",
    gender = 1,
    age = 20,
    description = "Ditto is cool.",
    last_seen_lat = 56.171089,
    last_seen_long = 10.189372,
    looking_for = new int[] { 2 },
    age_from = 18,
    age_to = 24,
    search_radius = 40 });
await WebClient.Post<PostPokeProfile>("poke_profiles/user/", new()
                                                        {
                                                            pokemons = new string[] { "Darumaka",
                                                            "Dragonair",
                                                            "Xerneas",
                                                            "Charizard",
                                                            "Sceptile",
                                                            "Sylveon" }}, user.id);
await WebClient.PostSmash(user.id, 5, true);

int talksdjf = 5;
Console.WriteLine("candidates?candidates=" + talksdjf.ToString());

