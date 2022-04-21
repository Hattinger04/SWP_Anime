using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json; 

namespace Anime_Website.Models
{
    public class User {
        private int userID;
        public int UserID {
            get { return this.userID; }
            set {
                if (value >= 0) {
                    this.userID = value;
                }
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Profilpicture { get; set; }

        public string getJsonFromUser() {
            return JsonConvert.SerializeObject(this); 
        }

        public static User getUserFromJson(string json) {
            return JsonConvert.DeserializeObject<User>(json); 
        }
    }
}
