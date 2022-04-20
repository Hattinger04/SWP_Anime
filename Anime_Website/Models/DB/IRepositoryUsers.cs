using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace Anime_Website.Models {
    
    public interface IRepositoryUsers {
        Task ConnectAsync();
        Task DisconnectAsync();
        Task<bool> Insert(User user);
        Task<bool> Delete(int user_id);
        Task<bool> ChangeUserData(int userID, User newUserData);
        Task<List<User>> GetAllUsers();
        Task<User> Login(String username, String password);
=======
namespace FirstWebApp.Models.DB {
    
    // TODO: asynchrone Programmierung 
    // wichtig: es sollte immer gegen eine Schnittstelle (Interface, Basisklasse) programmiert werden
    //      => programm leichter wartbar, änderbar, testbar
    public interface IRepositoryUsers {
        void Connect();
        void Disconnect();
        bool Insert(User user);
        bool Delete(int user_id);
        bool ChangeUserData(int userID, User newUserData);
        List<User> GetAllUsers();
        User Login(String username, String password);
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
        
        // weitere Methoden
    }
}
