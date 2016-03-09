using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using LetsEat.Class;
using Windows.UI.Popups;

namespace LetsEat
{
    class ApiCall
    {

        static public async Task<UserRP> MakeCall(string method, UserVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");

                if (method.Equals("editProfil"))
                    client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
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
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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

        static public async Task<GetRestaurantRP> MakeCall(string method, GetRestaurantVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    GetRestaurantRP f = JsonConvert.DeserializeObject<GetRestaurantRP>(res);

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
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

        static public async Task<AddMemberRP> MakeCall(string method, AddMemberVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    AddMemberRP f = JsonConvert.DeserializeObject<AddMemberRP>(res);

                    return f;
                }
            }

            return null;
        }

        static public async Task<DeleteMemberRP> MakeCall(string method, DeleteMemberVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    DeleteMemberRP f = JsonConvert.DeserializeObject<DeleteMemberRP>(res);

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
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

        static public async Task<RestaurantChoiceRP> MakeCall(string method, RestaurantChoiceVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    RestaurantChoiceRP f = JsonConvert.DeserializeObject<RestaurantChoiceRP>(res);

                    return f;
                }
            }

            return null;
        }

        static public async Task<CanRandomRP> MakeCall(string method, CanRandomVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    CanRandomRP f = JsonConvert.DeserializeObject<CanRandomRP>(res);

                    return f;
                }
            }

            return null;
        }

        static public async Task<AddRestaurantRP> MakeCall(string method, AddRestaurantVM obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                client.DefaultRequestHeaders.Add("Authorization", GlobalData.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(method, obj);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    AddRestaurantRP f = JsonConvert.DeserializeObject<AddRestaurantRP>(res);

                    return f;
                }
            }

            return null;
        }
    }
}
