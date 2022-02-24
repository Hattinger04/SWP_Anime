using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        
        // weitere Methoden
    }
}
