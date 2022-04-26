using Anime_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Syndication;
using Anime_Website.Models;


namespace Anime_Website.Controllers {
    public class HomeController : Controller {

        private static HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Discord(string search_anime) {
            Console.WriteLine("Name: " + search_anime);
            string message = "";
            HttpResponseMessage response = await client.GetAsync(String.Format("https://api.jikan.moe/v4/anime?q={0}&sfw", search_anime));
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
                    return View("Discord", animeObjects);
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

        public IActionResult Schedule() {
            string url = "https://www.livechart.me/feeds/episodes";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            List<AnimeObject> animeObjects = new();
            int counter = 0;
            foreach (var item in feed.Items) {
                foreach (SyndicationElementExtension extension in item.ElementExtensions) {
                    animeObjects.Add(new AnimeObject() {
                        ID = counter,
                        Name = item.Title.Text,
                        Status = "running",
                        Image_URL = item.ElementExtensions.FirstOrDefault(ext => ext.OuterName == "thumbnail").GetObject<XmlElement>().Attributes.GetNamedItem("url").Value,
                        URL = item.Id
                    });
                    counter++;
                }
            }
            return View("Schedule", animeObjects);
        }
    }
}