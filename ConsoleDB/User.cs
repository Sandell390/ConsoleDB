using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ConsoleDB
{
    class User
    {
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string postnr { get; set; }
        public string address { get; set; }
        public string mobilenr { get; set; }

        public User() 
        { 
            
        }

        public void Login() 
        {
            Console.Clear();

            Console.WriteLine("Hej, Du er nu ved at logge ind på verdens bedste service");

            string loginEmail = "";
            string password = "";
            bool currentLogin = false;

            while (!currentLogin)
            {
                Console.WriteLine("");

                Console.Write("Email: ");
                loginEmail = Console.ReadLine();

                Console.WriteLine("");

                Console.Write("Password: ");
                password = Console.ReadLine();

                password = encode(password);

                Dictionary<string, string> conditions = new Dictionary<string, string>();
                conditions.Add("email",loginEmail);
                conditions.Add("password", password);

                string sql = Database.SELECT(conditions: conditions, tableName: "user");
                MySqlDataReader reader = Database.MySqlReader(sql);

                if (reader.HasRows) 
                {
                    while (reader.Read()) 
                    {
                        name = reader["name"].ToString();
                        email = reader["email"].ToString();
                        address = reader["address"].ToString();
                        mobilenr = reader["mobilenr"].ToString();
                        postnr = reader["postnr"].ToString();
                    }
                    currentLogin = true;
                    
                }
            }

            Menu();
            
        }

        public void Register() 
        {
            string pwd1 = "";
            string pwd2 = "";

            Console.Clear();

            Console.WriteLine("Hej, Du er nu ved at oprette dig til verdens bedste service");
            Console.Write("Navn: ");
            name = Console.ReadLine();
            Console.WriteLine("");

            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                Console.WriteLine("");

                if (email.Contains("@")) 
                {
                    break;
                }
                else 
                {
                    Console.WriteLine("Skriv venlist en gyldig email");
                }
            } while (true);

           

            do
            {
                Console.WriteLine("");
                Console.WriteLine("Password: ");
                pwd1 = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("Confirm password: ");
                pwd2 = Console.ReadLine();

                if (pwd1 == pwd2) 
                {
                    password = encode(pwd1);
                    break;
                }
                else 
                {
                    Console.WriteLine("");
                    Console.WriteLine("Indtastning forkert ");
                }

            } while (true);

            Console.Write("Address: ");
            address = Console.ReadLine();
            Console.WriteLine("");

            Console.Write("Mobilnr: ");
            mobilenr = Console.ReadLine();
            Console.WriteLine("");

            Console.Write("Postnummer: ");
            postnr = Console.ReadLine();
            Console.WriteLine("");

            List<string> columns = new List<string>() 
            { 
                "name",
                "password",
                "email",
                "address",
                "mobilenr",
                "postnr"
            };
            List<string> values = new List<string>()
            {
                name,
                password,
                email,
                address,
                mobilenr,
                postnr
            };

            Database.INSERT(columns,"user",values);

            Console.Clear();
            Console.WriteLine("Du er nu registred");
            Thread.Sleep(1500);

            Menu();
        }

        void Menu() 
        {
            Console.Clear();

            Console.WriteLine($"Velkommen, {name}");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("1. View post");
            Console.WriteLine("2. Make post");
            Console.WriteLine("3. Edit profile");
            Console.WriteLine("4. Logout");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Dit valg: ");

            int input = Converters.UserStringToInt();

            Console.WriteLine("");

            while (input > 4 && input < 0) 
            {
                Console.WriteLine("Du skrev noget forkert, prøv igen");

                Console.Write("Dit valg: ");
                input = Converters.UserStringToInt();
                Console.WriteLine("");
            }

            switch (input)
            {
                case 1:
                    ViewPost();
                    break;
                case 2:
                    MakePost();
                    break;
                case 3:
                    EditProfile();
                    break;
                case 4:
                    Logout();
                    break;
                default:
                    break;
            }


        }

        void EditProfile() 
        { 
        
        }
        void Logout() 
        { 
        
        }

        void ViewPost() 
        { 
        
        }

        void MakePost() 
        { 
        
        }

        string decode(string encodedText) 
        {
            var encodedBytes = Convert.FromBase64String(encodedText);
            return Encoding.UTF8.GetString(encodedBytes);
        }
        string encode(string plainText) 
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
