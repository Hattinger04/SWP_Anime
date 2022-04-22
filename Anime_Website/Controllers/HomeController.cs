using Anime_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;


namespace Anime_Website.Controllers {
    public class HomeController : Controller {

        static HttpClient client = new HttpClient();

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string name) {
            name = name.Replace(" ", "%20"); 
            string message = "";
            HttpResponseMessage response = await client.GetAsync(String.Format("https://api.jikan.moe/v4/anime?q={0}&sfw", name));
            if (response.IsSuccessStatusCode) {
                message = await response.Content.ReadAsStringAsync();
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(message);
                List<AnimeObject> animeObjects = new List<AnimeObject>();
                string countString = jsonObj["pagination"]["items"]["count"];
                int count = Int32.Parse(countString); 
                if (count != 0) { 
                    for (int i = 0; i < count; i++) {
                        animeObjects.Add(new AnimeObject() {
                            ID = i,
                            Image_URL = jsonObj["data"][i]["images"]["jpg"]["image_url"],
                            Name = jsonObj["data"][i]["title"],
                            Status = jsonObj["data"][i]["status"],
                            URL = jsonObj["data"][i]["url"],
                        });
                    }
                    foreach (AnimeObject obj in animeObjects) {
                        Console.WriteLine(obj.Name);
                    }
                    return View("Index", animeObjects);
                }
                return View("_Message", new Message("Kein Ergebnis!", "Die Abfrage hat kein Ergebnis geliefert!"));
            }
            return View("_Message", new Message("HTTPClient Error!", "Die Abfrage hat nicht funktioniert, versuche es bitte später erneut!")); 
        }

        public IActionResult Help() {
            return View();
        }
        public IActionResult AboutUs() {
            return View();
        }
        public IActionResult Discord() {
            return View();
        }
    }
}