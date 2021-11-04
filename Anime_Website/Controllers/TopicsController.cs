using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anime_Website.Controllers
{
    public class TopicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
