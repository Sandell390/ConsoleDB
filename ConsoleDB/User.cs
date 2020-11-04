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
        public int id_user { get; set; }

        public bool wantLogout = false;

        public User() 
        { 
            
        }

        public void Login() 
        {
            Console.Clear();

            Console.WriteLine("Hej, Du er nu ved at logge ind på verdens bedste service");

            wantLogout = false;

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
                        id_user = int.Parse(reader["id_user"].ToString());
                    }
                    currentLogin = true;
                    
                }
                reader.Close();
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
            Console.Clear();

            MySqlDataReader reader = Database.MySqlReader($"SELECT postnummer.by, user.name from postnummer INNER JOIN user ON user.postnr='{postnr}' and postnummer.postnr=user.postnr");
            string by = "";
            while (reader.Read()) 
            {
                by = reader[0].ToString();
            }

            reader.Close();

            Console.WriteLine("Her kan du se alle dine oplysninger: ");
            Console.WriteLine("");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Address: {address}");
            Console.WriteLine($"Postnr: {postnr} ({by})");
            Console.WriteLine($"Mobilnr: {mobilenr}");

            Console.WriteLine("");
            Console.Write("Skriv en af tingene som du vil ændre: ");
            string input = Console.ReadLine();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"Du er nu igang med at ændre {input}");
            Console.WriteLine("");
            Console.Write("Ændring: ");
            string newData = Console.ReadLine();

            switch (input.ToLower()) 
            {
                case nameof(name):
                    name = newData;
                    break;
                case nameof(email):
                    email = newData;
                    break;
                case nameof(mobilenr):
                    mobilenr = newData;
                    break;
                case nameof(address):
                    address = newData;
                    break;
                case nameof(postnr):
                    postnr = newData;
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }

            MySqlCommand cmd = Database.mySql.CreateCommand();

            cmd.CommandText = $"Update user SET {input}='{newData}' WHERE id_user='{id_user}'";

            cmd.Parameters.AddWithValue(input,newData);

            cmd.ExecuteNonQuery();

            Console.ReadKey();

            Menu();

        }
        void Logout() 
        {
            Console.Write("Du logger nu ud");
            for (int i = 0; i < 6; i++)
            {
                Thread.Sleep(80);
                Console.Write(".");
            }
            Console.WriteLine("");
            wantLogout = true;

        }

        void ViewPost() 
        {
            Console.Clear();

            List<Post> posts = new List<Post>();

            string sql = Database.SELECT(tableName: "blog");
            MySqlDataReader reader = Database.MySqlReader(sql);

            int count = 0;
            while (reader.Read()) 
            {
                posts.Add(new Post());
                posts[count].id_user = int.Parse(reader[1].ToString());
                posts[count].x = int.Parse(reader[2].ToString());
                posts[count].y = int.Parse(reader[3].ToString());
                posts[count].z = int.Parse(reader[4].ToString());
                posts[count].desp = reader[5].ToString();
                count++;
            }
            reader.Close();
            if (posts.Count < 1) 
            {
                Console.WriteLine("Der er ikke nogen posts så du må heller lave nogle ");
                Thread.Sleep(1000);
                return;
            }

            for (int i = 0; i < posts.Count; i++)
            {
                posts[i].ViewDetails();
            }

            Console.ReadKey();

            Menu();
        }

        void MakePost() 
        {
            Console.Clear();

            Console.WriteLine("Du er nu ved at oprette en post");
            Console.WriteLine("");
            Console.Write("X: ");
            int x = Converters.UserStringToInt();
            Console.WriteLine("");

            Console.Write("Y: ");
            int Y = Converters.UserStringToInt();
            Console.WriteLine("");

            Console.Write("Z: ");
            int Z = Converters.UserStringToInt();
            Console.WriteLine("");

            Console.Write("Description: ");
            string desp = Console.ReadLine();
            Console.WriteLine("");

            List<string> columns = new List<string>()
            {
                "id_user",
                "x",
                "y",
                "z",
                "description"
            };
            List<string> values = new List<string>()
            {
                id_user.ToString(),
                x.ToString(),
                Y.ToString(),
                Z.ToString(),
                desp
            };

            Database.INSERT(columns, "blog", values);

            Console.ReadKey();

            Menu();
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
