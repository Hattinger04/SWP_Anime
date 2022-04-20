using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Anime_Website.Models.DB {

    public class RepositoryUsersDB : IRepositoryUsers {

        private string connectionsString = "Server=localhost;database=testwebsite;user=root";
        private DbConnection connection;

        public async Task ConnectAsync() {
            if (this.connection == null) {
                this.connection = new MySqlConnection(this.connectionsString);
            }
            if (this.connection.State != System.Data.ConnectionState.Open) {
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


                cmd.Parameters.Add(paramUN);
                cmd.Parameters.Add(paramPW);
                cmd.Parameters.Add(paramEmail);
                cmd.Parameters.Add(paramBD);
                cmd.Parameters.Add(paramGender);
                cmd.Parameters.Add(paramID);

                return await cmd.ExecuteNonQueryAsync() == 1;
            }
            return false;
        }

        public async Task<bool> Delete(int user_id) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "delete from users where user_id = @user_id";
                DbParameter paramID = cmd.CreateParameter();

                paramID.ParameterName = "user_id";
                paramID.DbType = System.Data.DbType.Int32;
                paramID.Value = user_id;

                cmd.Parameters.Add(paramID);
                return await cmd.ExecuteNonQueryAsync() == 1;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsers() {
            List<User> users = new List<User>();
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "select * from users";
                using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
                    while (await reader.ReadAsync()) {
                        users.Add(new User() {
                            UserID = Convert.ToInt32(reader["user_id"]),
                            Username = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
                            Birthdate = Convert.ToDateTime(reader["birthdate"]),
                            Email = Convert.ToString(reader["email"]),
                            Gender = (Gender)Convert.ToInt32(reader["gender"])
                        });
                    }
                }
            }
            return users;
        }
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
                    if (reader.Read()) {
                        return new User() {
                            UserID = user_id,
                            Username = Convert.ToString(reader["username"]),
                            Password = Convert.ToString(reader["password"]),
                            Birthdate = Convert.ToDateTime(reader["birthdate"]),
                            Email = Convert.ToString(reader["email"]),
                            Gender = (Gender)Convert.ToInt32(reader["gender"])
                        };
                    }
                }
            }
            return new User();
        }
        public async Task<bool> Insert(User user) {
            if (this.connection?.State == System.Data.ConnectionState.Open) {
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = "insert into users values(null, @username, sha2(@password, 512), @email, @birthdate, @gender)";
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
            }
            return false;
        }

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
                        };
                    }
                }
            }
            return null;
        }
    }
}
