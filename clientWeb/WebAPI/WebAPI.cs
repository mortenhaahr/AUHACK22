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
using System.Reflection;
using System.IO;

namespace WebAPI
{
    public enum Gender
    {
        OTHER = 0,
        MALE = 1,
        FEMALE = 2,
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
        [PostUser]
        public string? url { get; set; }

        [PostUser]
        public int id { get; set; }

        [PostUser]
        public string? email { get; set; }

        [PostUser]
        public string? first_name { get; set; }

        [PostUser]
        public string? last_name { get; set; }

        [PostUser]
        public int gender { get; set; }

        [PostUser]
        public int age { get; set; }

        [PostUser]
        public string? description { get; set; }

        [PostUser]
        public double last_seen_lat { get; set; }

        [PostUser]
        public double last_seen_long { get; set; }

        [PostUser]
        public int[]? looking_for { get; set; }

        [PostUser]
        public int age_from { get; set; }
        
        [PostUser]
        public int age_to { get; set; }

        [PostUser]
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

    public class PostUserAttribute : Attribute
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
        public double type_fairy { get; set; }
    }


    public class PostSmash
    {
        public bool smash { get; set; }
    };

    public class Smash : PostSmash
    {
        public bool? counter_part_smashed { get; set; }
    }

    public class WebClient
    {

        

        private static HttpClient client = new HttpClient();

        //Ask for candidates        (data=amount of candidates)
        //Send matched candidate       (Send back id of candidates matched)
        //Send team                 (
        //Get team
        //Login

        public static async Task<T?> Get<T>(string? path, int? userID)
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

        public static async Task<Response<T>?> Get<T>(string? path)
        {
            return await Get<Response<T>>(path, null);
        }

        public static async Task<string> Post<T>(string? path, T obj, int? userId = null)
        {


            string myContent = JsonSerializer.Serialize(obj, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            

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

        public static async Task<User[]?> GetCandidates(int userID, int candidates=10) {
            return await Get<User[]>("candidates/" + userID.ToString() + "/" + candidates.ToString() + "/", null);
        }

        public static async Task<string> PostSmash(int userID, int smashID, bool smash)
        {
            return await Post(("smash_pass/" + userID.ToString() + "/" + smashID.ToString() + "/"), new PostSmash() { smash = smash });
        }

        public static async Task<Smash?> GetSmash(int userID, int smashID)
        {
            return await Get<Smash>("smash_pass/" + userID.ToString() + "/" + smashID.ToString() + "/", null);
        }

        public async static Task<byte[]> LoadImage(Uri uri)
        {

            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var image = await response.Content.ReadAsByteArrayAsync();

            return image;
        }

        public async static Task<string> PostUser(string? path, PostUser userData)
        {
            return await Post(path, new User()
            {
                url = userData.url,
                id = userData.id,
                email = userData.email,
                first_name = userData.first_name,
                last_name = userData.last_name,
                gender = userData.gender,
                age = userData.age,
                description = userData.description,
                last_seen_lat = userData.last_seen_lat,
                last_seen_long = userData.last_seen_long,
                looking_for = userData.looking_for,
                age_from = userData.age_from,
                age_to = userData.age_to,
                search_radius = userData.search_radius,
                photo0 = (userData.photo0 == null? null:Convert.ToBase64String(userData.photo0, 0, userData.photo0.Length)),
                photo1 = (userData.photo1 == null? null:Convert.ToBase64String(userData.photo1, 0, userData.photo1.Length)),
                photo2 = (userData.photo2 == null? null:Convert.ToBase64String(userData.photo2, 0, userData.photo2.Length)),
                photo3 = (userData.photo3 == null? null:Convert.ToBase64String(userData.photo3, 0, userData.photo3.Length)),
                photo4 = (userData.photo4 == null? null:Convert.ToBase64String(userData.photo4, 0, userData.photo4.Length)),
                photo5 = (userData.photo5 == null? null:Convert.ToBase64String(userData.photo5, 0, userData.photo5.Length)),
                photo6 = (userData.photo6 == null? null:Convert.ToBase64String(userData.photo6, 0, userData.photo6.Length)),
                photo7 = (userData.photo7 == null? null:Convert.ToBase64String(userData.photo7, 0, userData.photo7.Length)),
                photo8 = (userData.photo8 == null? null:Convert.ToBase64String(userData.photo8, 0, userData.photo8.Length)),
                photo9 = (userData.photo9 == null ? null : Convert.ToBase64String(userData.photo9, 0, userData.photo9.Length)),
            });

        }

        static WebClient()
        {
            client.BaseAddress = new Uri("https://auhack22.herokuapp.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Basic YXVoYWNrMjJAYXdlc29tZS5jb206MTIzNA==");
        }

        

    }
}
