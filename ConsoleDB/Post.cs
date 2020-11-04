using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ConsoleDB
{
    class Post
    {
        public int id_user { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public string desp { get; set; }

        public void ViewDetails() 
        {
            string username = GetUsernameFromId();

            Console.WriteLine($"------------------");
            Console.WriteLine($"Username: {username}");
            Console.WriteLine($"Description: {desp}");
            Console.WriteLine($"X: {x} Y: {y} Z: {z}");
            Console.WriteLine($"------------------");
        }

        string GetUsernameFromId() 
        {
            string name = "";
            MySqlDataReader reader = Database.MySqlReader($"SELECT name from user where id_user='{id_user}'");

            while (reader.Read()) 
            {
                name = reader[0].ToString();
            }
            reader.Close();

            return name;
        }

    }
}
