using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anime_Website.Models;
using Anime_Website.Models.DB;
using System.Data.Common;
using System.IO;

namespace Anime_Website.Controllers {
    public class UserController : Controller {

        private IRepositoryUsers repo = new RepositoryUsersDB();
        
        [HttpGet]
        public IActionResult Index() {
            if (HttpContext.Session.GetString("user") != null) {
                return View();
            }
            return RedirectToAction("Login"); 
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User userDataFromForm) {
            if (userDataFromForm == null) {
                return RedirectToAction("Login");
            }
            ValidateRegistrationData(userDataFromForm);
            if (ModelState.IsValid) {
                try {
                    await repo.ConnectAsync();
                    if (repo.Insert(userDataFromForm).Result) {
                        return View("_Message", new Message("Registrierung", "Ihre Daten wurden erfolgreich abgespeichert"));
                    } else {
                        return View("_Message", new Message("Registrierung", "Ihre Daten NICHT wurden erfolgreich abgespeichert", "Bitte versuchen sie es später erneut!"));
                    }
                } catch (DbException ex) {
                    return View("_Message", new Message("Registrierung", "Datenbankfehler!" + ex.Message, "Bitte versuchen sie es später erneut!"));
                } finally {
                    await repo.DisconnectAsync();
                }

            }
            return View("Login", userDataFromForm);
        }
        private void ValidateRegistrationData(User user) {
            if (user == null) {
                return;
            }
            if (user.Username == null || (user.Username.Trim().Length < 4)) {
                ModelState.AddModelError("Username", "Der Benutzername muss mind. 4 Zeichen lang sein!");
            }
            if (user.Password == null || (user.Password.Length < 8)) {
                ModelState.AddModelError("Password", "Das Passwort muss mind. 8 Zeichen lang sein!");
            }
        }
        [HttpGet]
        public IActionResult Login() {
            if (HttpContext.Session.GetString("user") != null) {
                return RedirectToAction("Index");
            }
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Login(User userDataFromForm) {
            if (userDataFromForm == null) {
                return RedirectToAction("Login");
            }
            try {
                await repo.ConnectAsync();
                User user = await repo.Login(userDataFromForm.Username, userDataFromForm.Password); 
                if (user != null) {
                    HttpContext.Session.SetString("user", user.getJsonFromUser());
                    // TODO: Set picture on website! 
                    return RedirectToAction("Index");
                } else {
                    return View("_Message", new Message("Login", "Ihr Username oder Password war falsch", "Bitte überprüfen sie ihre Daten!"));
                }
            } catch (DbException ex) {
                return View("_Message", new Message("Registrierung", "Datenbankfehler! " + ex.Message, "Bitte versuchen sie es später erneut!"));
            } finally {
                await repo.DisconnectAsync();
            }
        }
        [HttpGet]
        public IActionResult Logout() {
            HttpContext.Session.Remove("user"); 
            return RedirectToAction("Index","Home"); 
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id) {
            try {
                await repo.ConnectAsync();
                await repo.Delete(id);
                return RedirectToAction("Index");
            } catch (DbException) {
                return View("_Message", new Message("Datenbankfehler!", "Der Benutzer konnte nicht gelöscht werden! Versuchen sie es später erneut."));
            } finally {
                await repo.DisconnectAsync();
            }
        }
        [HttpGet]
        public IActionResult Update() {
            return View();
        }

        // Wird so natürlich nicht funktionieren 
        [HttpPost]
        public async Task<IActionResult> Update(User user, int id) {
            try {
                await repo.ConnectAsync();
                await repo.ChangeUserData(id, user);
                return RedirectToAction("Index");
            } catch (DbException) {
                return View("_Message", new Message("Datenbankfehler!", "Der Benutzer konnte nicht geändert werden! Versuchen sie es später erneut."));
            } finally {
                await repo.DisconnectAsync();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file) {
            try {
                await repo.ConnectAsync();
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath)) {
                    await file.CopyToAsync(stream);
                }
                
                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                User user = Models.User.getUserFromJson(HttpContext.Session.GetString("user")); 
                user.Profilpicture = base64ImageRepresentation;
                Console.WriteLine(user.getJsonFromUser()); 
                await repo.ChangeUserPicture(user.UserID, user);
                HttpContext.Session.SetString("user", user.getJsonFromUser()); 
                return RedirectToAction("Index");
            } catch (DbException) {
                return View("_Message", new Message("Datenbankfehler!", "Der Benutzer konnte nicht geändert werden! Versuchen sie es später erneut."));
            } finally {
                await repo.DisconnectAsync();
            }
        }
    }
}
