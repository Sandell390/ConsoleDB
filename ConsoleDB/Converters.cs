using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDB
{
    public class Converters
    {
        public static int UserStringToInt()
        {
            int res;
            string userString = Console.ReadLine();
            while (!int.TryParse(userString, out res))
            {
                Console.WriteLine("Du skrev ikke et gyldigt tal, prøv igen");
                userString = Console.ReadLine();

            }


            return res;
        }
    }
}
