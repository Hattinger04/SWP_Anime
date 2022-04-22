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
            string message = "";
            HttpResponseMessage response = await client.GetAsync(String.Format("https://api.jikan.moe/v4/anime?q={0}&sfw", name));
            if (response.IsSuccessStatusCode) {
                // bearbeiten des json vll
                message = await response.Content.ReadAsStringAsync();
                return View(message);
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