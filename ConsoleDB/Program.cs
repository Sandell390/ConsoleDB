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

            bool power = true;
            do
            {
                Console.Clear();
                Database.connect();
                User user = new User();

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
                        power = false;
                        break;
                }


                Database.disconnect();

            } while (power);

            
            Console.WriteLine("Done.");
            Console.ReadKey();

        }
    }
}
