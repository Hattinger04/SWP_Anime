using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anime_Website.Models {
    public class AnimeObject {

        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Image_URL { get; set; }
        public string Status { get; set; }
    }
}
