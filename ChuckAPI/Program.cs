using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using Nancy.Json;
using Newtonsoft.Json;

namespace ChuckAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowCategories();
            //RandomJoke();
            CategoryByUser();
        }

        private static void CategoryByUser()
        {
            Console.WriteLine("\nChoose a category?");
            string userCat = Console.ReadLine();

            string categoryChoiceURL = ($"https://api.chucknorris.io/jokes/random?category={userCat}");
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(categoryChoiceURL);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);
            }
        }

        private static void ShowCategories()
        {
            string categoryURL = "https://api.chucknorris.io/jokes/categories";
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(categoryURL);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                //Console.WriteLine(response);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var categories = ser.Deserialize<List<string>>(response);
                foreach (var category in categories)
                {
                    Console.WriteLine(category);
                }
            }
        }
        private static void RandomJoke()
        {
            string randomJokeUrl = "https://api.chucknorris.io/jokes/random";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(randomJokeUrl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);
            }
        }
    }
}
