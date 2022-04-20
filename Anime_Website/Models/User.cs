using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace Anime_Website.Models
{
    public class User
    {
        private int userID; 
        public int UserID
        {
            get { return this.userID; }
            set { 
                if(value >= 0) {   
                    this.userID = value; 
                } 
            }
        }
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set;  }
        public DateTime Birthdate { get; set;  }
        public Gender Gender { get; set; }
=======
namespace FirstWebApp.Models
{
    public class User
    {
        private int userID;
        public int UserID
        {
            get { return this.userID; }
            set
            {
                if (value >= 0)
                {
                    this.userID = value;
                }
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }    
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
    }
}
