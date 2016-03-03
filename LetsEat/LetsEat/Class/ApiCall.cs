using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace LetsEat
{
    class ApiCall
    {

        static public async Task<UserRP> MakeCall(string method, UserVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    UserRP f = JsonConvert.DeserializeObject<UserRP>(res);

                    return f;
                }
            }

            return null;
        }

        static public async Task<GroupRP> MakeCall(string method, GroupVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    GroupRP f = JsonConvert.DeserializeObject<GroupRP>(res);

                    return f;
                }
            }

            return null;
        }

        static public async Task<ProfilRP> MakeCall(string method, ProfilVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    ProfilRP f = JsonConvert.DeserializeObject<ProfilRP>(res);

                    return f;
                }
            }

            return null;
        }

        static public async Task<EditProfilRP> MakeCall(string method, EditProfilVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    EditProfilRP f = JsonConvert.DeserializeObject<EditProfilRP>(res);

                    return f;
                }
            }

            return null;
        }

        static public async Task<CreateGroupRP> MakeCall(string method, CreateGroupVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    CreateGroupRP f = JsonConvert.DeserializeObject<CreateGroupRP>(res);

                    return f;
                }
            }

            return null;
        }
    }
}
