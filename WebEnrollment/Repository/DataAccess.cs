using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace WebEnrollment.Repository
{
    public interface IDataAccess
    {
        List<string> Get();
    }

    public class DataAccess : IDataAccess
    {
        //private readonly IUserProvider _userProvider;
        //public UserRepository(IUserProvider userProvider)
        //{
        //    _userProvider = userProvider;
        //}

        public List<string> Get()
        {
            //var user = _userProvider.UserName;
            return new List<string> { "User 1", "User 2", "User 3" };
        }

        public static HttpClient ApiClient { get; set; } = new HttpClient();

        public static void Initialize()
        {
            Get1();
            Post();
        }

        public static async void Get1()
        {
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string uri = "http://localhost:63908/api/values/1/";
            using (HttpResponseMessage reponse = await ApiClient.GetAsync(uri))
            {
                string a = await reponse.Content.ReadAsStringAsync();
            }
        }
        public class Profile
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        public static async void Post()
        {
            ApiClient.DefaultRequestHeaders.Accept.Clear();

            Profile prof = new Profile();
            prof.FirstName = "Jessie";
            prof.LastName = "Cruz";

            var uri = string.Format("http://localhost:63908/api/values/");

            var content = new StringContent(JsonConvert.SerializeObject(prof), Encoding.UTF8, "application/json");

            using (HttpResponseMessage reponse = await ApiClient.PostAsync(uri, content))
            {
                var a = await reponse.Content.ReadAsStringAsync();
            }
        }

    }
}