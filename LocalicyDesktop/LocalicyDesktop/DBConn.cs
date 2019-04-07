using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace LocalicyDesktop
{
    public static class DBConn
    {
        public static string GetName(int UID)
        {
            using (IDbConnection conn = new SqlConnection(Helper.ConnString()))
            {
                List<User> curr = conn.Query<User>($"select * from User_Data where UID = '{UID}'").ToList();
                return curr[0].Name;
            }
        }

        public static User LogIn(string username, string password)
        {
            using (IDbConnection conn = new SqlConnection(Helper.ConnString()))
            {
                List<User> curr = conn.Query<User>($"select * from User_Data where Username = '{username}' and Password = '{password}'").ToList();
                try { return curr[0]; }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static User GetUser(int UID)
        {
            using (IDbConnection conn = new SqlConnection(Helper.ConnString()))
            {
                List<User> curr = conn.Query<User>($"select * from User_Data where UID = '{UID}'").ToList();
                return curr[0];
            }
        }
        
        public static List<Messages> GetMessages(string state, int channel)
        {
            using (IDbConnection conn = new SqlConnection(Helper.ConnString()))
            {
                return conn.Query<Messages>($"select * from Message_Data where State = '{state}' and Channel = '{channel}'").ToList();
            }
        }

        public static void PostMessage(User user, string message, int id)
        {
            using (IDbConnection conn = new SqlConnection(Helper.ConnString()))
            {
                conn.Query($"insert into Message_Data (ID, State, Channel, Name, Message) values ('{GetNextMessageID()}','{user.State}','{id}','{user.Name}','{message}')");
            }
        }

        private static int GetNextMessageID()
        {
            using (IDbConnection conn = new SqlConnection(Helper.ConnString()))
            {
                List<Messages> mess = conn.Query<Messages>($"select * from Message_Data").ToList();
                return mess.Count;
            }
        }

        public static void CreateUser(string username, string password, string name, string address, string state, int age, string ethnicity, string gender)
        {
            using (IDbConnection conn = new SqlConnection(Helper.ConnString()))
            {
                conn.Query($"insert into User_Data (UID, Username, Password, Name, Address, State, Age, Ethnicity, Gender) values ('{GetNextUserID()}','{username}','{password}','{name}','{address}','{state}','{age}','{ethnicity}','{gender}')");
            }
        }

        private static int GetNextUserID()
        {
            using (IDbConnection conn = new SqlConnection(Helper.ConnString()))
            {
                List<User> mess = conn.Query<User>($"select * from User_Data").ToList();
                return mess.Count;
            }
        }
    }
}
