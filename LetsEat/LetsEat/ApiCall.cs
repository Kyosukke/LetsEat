using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
    }
}
