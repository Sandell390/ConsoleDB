using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ConsoleDB
{
    public class Database
    {
        public static string serverip;
        public static string user;
        public static string database;
        public static string port;
        public static string password;

        public static MySqlConnection mySql = null;

        public static void connect() 
        {
            mySql = new MySqlConnection($"server={serverip};user={user};database={database};port={port};password={password};");

            try
            {
                Console.WriteLine("Connecting to database...");
                mySql.Open();

                Console.WriteLine("Now connected to database");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not connect to database");
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }

        public static void disconnect() 
        {
            try
            {
                Console.WriteLine("Disconnecting to database...");
                mySql.Close();

                Console.WriteLine("Now disconnected from database");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not disconnect from database");
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }

        public static string SELECT(List<string> columns = null, string tableName = "", Dictionary<string,string> conditions = null) 
        {
            string sqlQuery = string.Empty;

            if (columns == null)
            {
                columns = new List<string>();
                columns.Add("*");
            }

            sqlQuery += "Select ";

            foreach (string column in columns)
            {
                sqlQuery += $"{column},";
            }

            sqlQuery = sqlQuery.Remove(sqlQuery.Length - 1);
            sqlQuery += $" from {tableName}";

            if (conditions == null)
            {
                return sqlQuery;
            }

            sqlQuery += " where ";
            foreach (var condition in conditions)
            {
                sqlQuery += $"{condition.Key} = '{condition.Value}' and ";
            }

            sqlQuery = sqlQuery.Remove(sqlQuery.LastIndexOf("and"));

            return sqlQuery;

        }

        public static void INSERT(List<string> columns = null, string tableName = "", List<string> values = null) 
        {
            string sqlQuery = string.Empty;

            if (columns == null || values == null) 
            {
                return;   
            }

            sqlQuery += $"Insert into {tableName} (";

            foreach (string column in columns)
            {
                sqlQuery += $"{column},";
            }

            sqlQuery = sqlQuery.Remove(sqlQuery.Length - 1);
            sqlQuery += ") VALUES (";

            foreach (string value in values)
            {
                sqlQuery += $"'{value}',";
            }

            sqlQuery = sqlQuery.Remove(sqlQuery.Length - 1);

            sqlQuery += ")";

            MySqlCommand cmd = new MySqlCommand(sqlQuery, mySql);

            cmd.ExecuteNonQuery();
        }

        public static MySqlDataReader MySqlReader(string sql) 
        {
            MySqlCommand cmd = new MySqlCommand(sql, Database.mySql);
            return cmd.ExecuteReader();
        }

    }
}
