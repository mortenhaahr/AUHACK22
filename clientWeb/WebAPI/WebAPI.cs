using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPI
{

    public enum Gender {
        OTHER=0,
        MALE=1,
        FEMALE=2,
    };


    public class Response<T>
    {
        public int count { get; set; }
        public string? next { get; set; }
        public string? prev { get; set; }
        public T[]? results { get; set; }
    }


    public class Pokemon
    {
        public string? url { get; set; } = "";

        public int id { get; set; }
        public string? name { get; set; } = "";
        public int pokedex { get; set; }

        public string? leveling_rate { get; set; }
        public string? species { get; set; }

        public int catch_rate { get; set; }
        public int generation { get; set; }
        public double gender_ratio { get; set; }
        public int hp { get; set; }
        public int attack { get; set; }
        public int defence { get; set; }
        public int sp_atk { get; set; }
        public int sp_def { get; set; }
        public int speed { get; set; }
        public string? type0 { get; set; }
        public string? type1 { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
        public float bmi { get; set; }
        public string? prevolution { get; set; }
        public string? evolution { get; set; }
        public string? img { get; set; }

    }

    public abstract class IUser<T>
    {
        public string? url { get; set; }
        public int id { get; set; }
        public string? email { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public int gender { get; set; }
        public int age { get; set; }
        public string? description { get; set; }
        public double last_seen_lat { get; set; }
        public double last_seen_long { get; set; }
        public int[]? looking_for { get; set; }
        public int age_from { get; set; }
        public int age_to { get; set; }
        public double search_radius { get; set; }

        public T? photo0 { get; set; }
        public T? photo1 { get; set; }
        public T? photo2 { get; set; }
        public T? photo3 { get; set; }
        public T? photo4 { get; set; }
        public T? photo5 { get; set; }
        public T? photo6 { get; set; }
        public T? photo7 { get; set; }
        public T? photo8 { get; set; }
        public T? photo9 { get; set; }

    }

    public class PostUser : IUser<byte[]>
    {

    }

    
    public class User : IUser<string>
    {        
    }

    public class PostPokeProfile
    {
        public string[] pokemons { get; set; } = new string[6];
    }

    public class PokeProfile
    {
        public string? url { get; set; }
        public int user_id { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public int generation { get; set; }
        public string? pokedex { get; set; }
        public int catch_rate { get; set; }
        public double bmi { get; set; }
        public double gender_ratio { get; set; }
        public int hp { get; set; }
        public int attack { get; set; }
        public int defence { get; set; }
        public int sp_atk { get; set; }
        public int sp_def { get; set; }
        public int speed { get; set; }
        public double type_normal { get; set; }
        public double type_flying { get; set; }
        public double type_fire { get; set; }
        public double type_psychic { get; set; }
        public double type_water { get; set; }
        public double type_bug { get; set; }
        public double type_grass { get; set; }
        public double type_rock { get; set; }
        public double type_electric { get; set; }
        public double type_ghost { get; set; }
        public double type_ice { get; set; }
        public double type_dark { get; set; }
        public double type_fighting { get; set; }
        public double type_dragon { get; set; }
        public double type_poison { get; set; }
        public double type_steel { get; set; }
        public double type_ground { get; set; }
        public double type_fairy { get; set;  }
    }

    internal class WebClient
    {



        static HttpClient client = new HttpClient();

        //Ask for candidates        (data=amount of candidates)
        //Send matched candidate       (Send back id of candidates matched)
        //Send team                 (
        //Get team
        //Login

        static async Task<T?> Get<T>(string? path, int? userID)
        {
            T? ret = default(T);

            HttpResponseMessage response = await client.GetAsync(path + (userID is null ? "" : userID.ToString() + "/"));

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {


                try
                {
                    ret = JsonSerializer.Deserialize<T>(responseContent);
                }
                catch
                {
                    Console.Write(responseContent);
                    throw;
                }
            }

            return ret;
        }

        static async Task<Response<T>?> Get<T>(string? path)
        {
            return await Get<Response<T>>(path, null);
        }

        static async Task<string> Post<T>(string? path, T obj, int? userId = null)
        {
            var myContent = JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull});
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            path += (userId is null ? "" : userId.ToString() + "/");


            string responseContent = "";
            var response = await client.PostAsync(path, byteContent);

            try
            {

                
                responseContent = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(responseContent);
                Console.WriteLine(ex);
                throw new ArgumentException("Post request failed");
            }
        

            return responseContent;
        }



        public static async Task RunTask()
        {
            client.BaseAddress = new Uri("https://auhack22.herokuapp.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Basic YXVoYWNrMjJAYXdlc29tZS5jb206MTIzNA==");

            try
            {
                var users = await Get<User>("users/");
                
                Console.WriteLine(users.results);

                var specific_user = await Get<User>("users/", 6);

                var rsp = await Get<PokeProfile>("poke_profiles/");

                
                PostUser my_User = new()
                {
                    id = 7,
                    age = 29,
                    age_from = 25,
                    age_to = 50,
                    description = "This is my new fancy description. I am looking for Pikagirls for some fun.",
                    email = "IamNotFake@auhack22.com",
                    first_name = "John",
                    last_name = "Doe",
                    last_seen_lat = 56.17,
                    last_seen_long = 10.19,
                    gender = (int)Gender.MALE,
                    looking_for = new int[] { (int)Gender.FEMALE, (int)Gender.OTHER},
                    search_radius = 5000,
                };

                try
                {
                    await Post("users/", my_User);
                }
                catch
                {

                }
                

                PostPokeProfile temp = new () { pokemons = new string[6] {
                    "Darumaka",
                    "Dragonair",
                    "Xerneas",
                    "Charizard",
                    "Sceptile",
                    "Sylveon"
                    }
                };

                await Post("poke_profiles/user/", temp, my_User.id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        

    }
}
