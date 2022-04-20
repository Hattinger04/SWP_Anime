using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace Anime_Website.Models.DB {
=======
namespace FirstWebApp.Models.DB {
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8

    public class RepositoryUsersDB : IRepositoryUsers {

        private string connectionsString = "Server=localhost;database=testwebsite;user=root";
        private DbConnection connection;
        
<<<<<<< HEAD
        public async Task ConnectAsync() {
=======
        public void Connect() {
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
            if (this.connection == null) {
                this.connection = new MySqlConnection(this.connectionsString);
            }
            if (this.connection.State != System.Data.ConnectionState.Open) {
<<<<<<< HEAD
               await this.connection.OpenAsync();
            }
        }
        public async Task DisconnectAsync() {
            if (this.connection != null && this.connection.State == System.Data.ConnectionState.Open) {
                await this.connection.CloseAsync();
            }
        }

        public async Task<bool> ChangeUserData(int userID, User user) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "update users set username = @username, password = sha2(@password, 512), " +
                    "email = @email, birthdate = @birthdate, gender = @gender) where user_id = @user_id";
=======
                this.connection.Open();
            }
        }
        public void Disconnect() {
            if (this.connection != null && this.connection.State == System.Data.ConnectionState.Open) {
                this.connection.Close();
            }
        }

        public bool ChangeUserData(int userID, User user) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "update users set username = @username, password = sha2(@password, 512), " +
                    "email = @email where user_id = @user_id";
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
                DbParameter paramUN = cmd.CreateParameter();
                paramUN.ParameterName = "username";
                paramUN.DbType = System.Data.DbType.String;
                paramUN.Value = user.Username;

                DbParameter paramPW = cmd.CreateParameter();
                paramPW.ParameterName = "password";
                paramPW.DbType = System.Data.DbType.String;
                paramPW.Value = user.Password;

                DbParameter paramEmail = cmd.CreateParameter();
                paramEmail.ParameterName = "email";
                paramEmail.DbType = System.Data.DbType.String;
<<<<<<< HEAD
                paramEmail.Value = user.Email;

                DbParameter paramBD = cmd.CreateParameter();
                paramBD.ParameterName = "birthdate";
                paramBD.DbType = System.Data.DbType.Date;
                paramBD.Value = user.Birthdate;

                DbParameter paramGender = cmd.CreateParameter();
                paramGender.ParameterName = "gender";
                paramGender.DbType = System.Data.DbType.Int32;
                paramGender.Value = user.Gender;

                DbParameter paramID = cmd.CreateParameter();
                paramGender.ParameterName = "user_id";
                paramGender.DbType = System.Data.DbType.Int32;
                paramGender.Value = user.UserID;
=======
                paramEmail.Value = user.Email; 

                DbParameter paramID = cmd.CreateParameter();
                paramID.ParameterName = "user_id";
                paramID.DbType = System.Data.DbType.Int32;
                paramID.Value = user.UserID;
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8


                cmd.Parameters.Add(paramUN);
                cmd.Parameters.Add(paramPW);
                cmd.Parameters.Add(paramEmail);
<<<<<<< HEAD
                cmd.Parameters.Add(paramBD);
                cmd.Parameters.Add(paramGender);
                cmd.Parameters.Add(paramID);

                return await cmd.ExecuteNonQueryAsync() == 1;
=======
                cmd.Parameters.Add(paramID);

                return cmd.ExecuteNonQuery() == 1;
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
            }
            return false; 
        }

<<<<<<< HEAD
        public async Task<bool> Delete(int user_id) {
=======
        public bool Delete(int user_id) {
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
            if(this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "delete from users where user_id = @user_id";
                DbParameter paramID = cmd.CreateParameter();
               
                paramID.ParameterName = "user_id";
                paramID.DbType = System.Data.DbType.Int32;
                paramID.Value = user_id;
                
                cmd.Parameters.Add(paramID);
<<<<<<< HEAD
                return await cmd.ExecuteNonQueryAsync() == 1; 
=======
                return cmd.ExecuteNonQuery() == 1; 
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
            }
            return false; 
        }

<<<<<<< HEAD
        public async Task<List<User>> GetAllUsers() {
=======
        public List<User> GetAllUsers() {
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
            List<User> users = new List<User>(); 
            if(this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "select * from users";
<<<<<<< HEAD
                using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
                    while(await reader.ReadAsync()) {
=======
                using (DbDataReader reader = cmd.ExecuteReader()) {
                    while(reader.Read()) {
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
                        users.Add(new User() {
                            UserID = Convert.ToInt32(reader["user_id"]),
                            Username = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
<<<<<<< HEAD
                            Birthdate = Convert.ToDateTime(reader["birthdate"]),
                            Email = Convert.ToString(reader["email"]),
                            Gender = (Gender)Convert.ToInt32(reader["gender"])
=======
                            Email = Convert.ToString(reader["email"])
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
                        });   
                    }
                }
            }
            return users; 
        }
<<<<<<< HEAD
        public async Task<User> GetUser(int user_id) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "select * from users where user_id = @user_id";
                
                DbParameter paramID = cmd.CreateParameter();
                paramID.ParameterName = "user_id";
                paramID.DbType = System.Data.DbType.String;
                paramID.Value = user_id;

                cmd.Parameters.Add(paramID);


                using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
=======
        public User GetUser(int user_id) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "select * from users where user_id = @user_id";
                using (DbDataReader reader = cmd.ExecuteReader()) {
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
                    if(reader.Read()) {
                        return new User() {
                            UserID = user_id,
                            Username = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
<<<<<<< HEAD
                            Birthdate = Convert.ToDateTime(reader["birthdate"]),
                            Email = Convert.ToString(reader["email"]),
                            Gender = (Gender)Convert.ToInt32(reader["gender"])
=======
                            Email = Convert.ToString(reader["email"])
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
                        };
                    }
                }               
            }
            return new User(); 
        }
<<<<<<< HEAD
        public async Task<bool> Insert(User user) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "insert into users values(null, @username, sha2(@password, 512), @email, @birthdate, @gender)";
=======
        public bool Insert(User user) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "insert into users values(null, @username, sha2(@password, 512), @email)";
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
                DbParameter paramUN = cmd.CreateParameter();
                paramUN.ParameterName = "username";
                paramUN.DbType = System.Data.DbType.String;
                paramUN.Value = user.Username;

                DbParameter paramPW = cmd.CreateParameter();
                paramPW.ParameterName = "password";
                paramPW.DbType = System.Data.DbType.String;
                paramPW.Value = user.Password;

                DbParameter paramEmail = cmd.CreateParameter();
                paramEmail.ParameterName = "email";
                paramEmail.DbType = System.Data.DbType.String;
                paramEmail.Value = user.Email;

<<<<<<< HEAD
                DbParameter paramBD = cmd.CreateParameter();
                paramBD.ParameterName = "birthdate";
                paramBD.DbType = System.Data.DbType.Date;
                paramBD.Value = user.Birthdate;

                DbParameter paramGender = cmd.CreateParameter();
                paramGender.ParameterName = "gender";
                paramGender.DbType = System.Data.DbType.Int32;
                paramGender.Value = user.Gender;

                cmd.Parameters.Add(paramUN);
                cmd.Parameters.Add(paramPW);
                cmd.Parameters.Add(paramEmail);
                cmd.Parameters.Add(paramBD);
                cmd.Parameters.Add(paramGender);

                return await cmd.ExecuteNonQueryAsync() == 1; 
=======
                cmd.Parameters.Add(paramUN);
                cmd.Parameters.Add(paramPW);
                cmd.Parameters.Add(paramEmail);

                return cmd.ExecuteNonQuery() == 1; 
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
            }
            return false;
        }

<<<<<<< HEAD
        public async Task<User> Login(string username, string password) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "select * from users where username = @username and password = sha2(@password, 512)";
                
                DbParameter paramUN = cmd.CreateParameter();
                paramUN.ParameterName = "username";
                paramUN.DbType = System.Data.DbType.String;
                paramUN.Value = username;

                DbParameter paramPW = cmd.CreateParameter();
                paramPW.ParameterName = "password";
                paramPW.DbType = System.Data.DbType.String;
                paramPW.Value = password;

                cmd.Parameters.Add(paramUN);
                cmd.Parameters.Add(paramPW);

                using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
                    if (reader.Read()) {
                        return new User() {
                            UserID = Convert.ToInt32(reader["user_id"]),
                            Username = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
                            Birthdate = Convert.ToDateTime(reader["birthdate"]),
                            Email = Convert.ToString(reader["email"]),
                            Gender = (Gender)Convert.ToInt32(reader["gender"])
=======
        public User Login(string username, string password) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "select * from users where username = @username and password =  @password";
                using (DbDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        return new User() {
                            UserID = Convert.ToInt32(reader["user_id"]),
                            Username = username, 
                            Password = password, 
                            Email = Convert.ToString(reader["email"])
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
                        };
                    }
                }
            }
<<<<<<< HEAD
            return null;
=======
            return null; 
>>>>>>> e50850021beb0cfe5c1fa8e58895e424740ce6d8
        }
    }
}
