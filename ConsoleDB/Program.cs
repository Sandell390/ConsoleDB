using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;

namespace ConsoleDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.serverip = "localhost";
            Database.user = "root";
            Database.database = "minecraftcoords";
            Database.port = "3306";
            Database.password = "hack2020";


            User user = new User();

            Database.connect();

            Console.WriteLine("1. Login ");
            Console.WriteLine("2. Register ");

            int input = int.Parse(Console.ReadLine());


            switch (input)
            {
                case 1:
                    user.Login();
                    break;
                case 2:
                    user.Register();
                    
                    break;
                default:
                    break;
            }

            List<string> test = new List<string>() { "name", "email" };
            string sql = Database.SELECT(test,tableName: "user");
            MySqlCommand cmd = new MySqlCommand(sql, Database.mySql);
            MySqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    Console.Write(rdr[i] + " -- ");
                }
                Console.WriteLine();
            }
            rdr.Close();

            Database.disconnect();
            Console.WriteLine("Done.");
            Console.ReadKey();

        }
    }
}
