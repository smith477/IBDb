using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Domain;
using Application.Domain.User;
using Application.Persistence;
using Cassandra;

namespace Application.DataProvider
{
    public class UserDataProvider
    {
        public static List<User> GetUsers()
        {
            ISession session = SessionManager.GetSession();
            List<User> users = new List<User>();

            if (session == null)
                return null;

            RowSet usersDb = session.Execute("SELECT * FROM ibdb.user;");
            foreach (var usr in usersDb)
            {
                User user = new User();
                user.Id = usr["id"] != null ? usr["id"].ToString() : " ";
                user.FirstName = usr["firstname"] != null ? usr["firstname"].ToString() : " ";
                user.LastName = usr["lastname"] != null ? usr["lastname"].ToString() : " ";
                user.DisplayName = usr["displayname"] != null ? usr["displayname"].ToString() : " ";
                user.Birthday = usr["birthday"] != null ? usr["birthday"].ToString() : " ";
                users.Add(user);
            }

            return users;
        }

        public static User GetUser(string id)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();
            if (session == null)
                return null;


            Row usr = session.Execute("SELECT * FROM ibdb.user where id=" + new Guid(id) + ";").FirstOrDefault();

            if (usr == null)
                return null;
            else
            {
                user.Id = usr["id"] != null ? usr["id"].ToString() : " ";
                user.FirstName = usr["firstname"] != null ? usr["firstname"].ToString() : " ";
                user.LastName = usr["lastname"] != null ? usr["lastname"].ToString() : " ";
                user.DisplayName = usr["displayname"] != null ? usr["displayname"].ToString() : " ";
                user.Birthday = usr["birthday"] != null ? usr["birthday"].ToString() : " ";
                user.Email = usr["email"] != null ? usr["email"].ToString() : " ";

                return user;
            }
        }

        public static User LoginUser(User user)
        {
            ISession session = SessionManager.GetSession();
            if (session == null)
                return null;

            Row userDb = session.Execute("SELECT * FROM ibdb.user WHERE displayname ='" + user.DisplayName + "' AND password='" + user.Password + "' allow filtering;").FirstOrDefault();

            if(userDb == null) 
            {
                userDb = session.Execute("SELECT * FROM ibdb.user WHERE email ='" + user.Email + "' AND password='" + user.Password + "' allow filtering;").FirstOrDefault();
            }
            
            if (userDb == null)
                return null;
            else
            {
                user.Id = userDb["id"] != null ? userDb["id"].ToString() : " ";
                user.FirstName = userDb["firstname"] != null ? userDb["firstname"].ToString() : " ";
                user.LastName = userDb["lastname"] != null ? userDb["lastname"].ToString() : " ";
                user.DisplayName = userDb["displayname"] != null ? userDb["displayname"].ToString() : " ";
                user.Birthday = userDb["birthday"] != null ? userDb["birthday"].ToString() : " ";
                user.Email = userDb["email"] != null ? userDb["email"].ToString() : " ";

                return user;
            }
        }

        public static User RegisterUser(User user)
        {
            ISession session = SessionManager.GetSession();
            if (session == null)
                return null;

            RowSet userDb = session.Execute("INSERT INTO ibdb.user (id, firstname, lastname, displayname, birthday, email, password) " +
                            "VALUES (uuid(),'" + user.FirstName +"','" + user.LastName + "','" + user.DisplayName + "', '" + user.Birthday + "','" + user.Email + "', '" +
                            user.Password + "');");

            Row usr = session.Execute("SELECT * FROM ibdb.user WHERE email = " + user.Email + "allow filtering;").FirstOrDefault();

            user.Id = usr["id"] != null ? usr["id"].ToString() : " ";
            user.FirstName = usr["firstname"] != null ? usr["firstname"].ToString() : " ";
            user.LastName = usr["lastname"] != null ? usr["lastname"].ToString() : " ";
            user.DisplayName = usr["displayname"] != null ? usr["displayname"].ToString() : " ";
            user.Birthday = usr["birthday"] != null ? usr["birthday"].ToString() : " ";
            user.Email = usr["email"] != null ? usr["email"].ToString() : " ";

            return user;
        }

        public static void UpdateUser(User usr)
        {
            ISession session = SessionManager.GetSession();
            if (session == null)
                return;

            RowSet userDb = session.Execute("UPDATE ibdb.user SET " +
                                                "displayname='" + usr.DisplayName + "'," +
                                                "firstname='" + usr.FirstName + "'," +
                                                "lastname='" + usr.LastName + "'," +
                                                "password='" + usr.Password + "'," +
                                                "email='" + usr.Email + "'," +
                                                "birthday='" + usr.Birthday + "' " +
                                                "WHERE id=" + new Guid(usr.Id) + ";");
        }

        public static void DeleteUser(string id)
        {
            ISession session = SessionManager.GetSession();
            if (session == null)
                return;

            RowSet userdb = session.Execute("DELETE FROM ibdb.user WHERE id=" + new Guid(id) + ";");
        }
    }
}
