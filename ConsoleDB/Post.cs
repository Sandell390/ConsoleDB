using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine($"------------------");
            Console.WriteLine($"User ID: {id_user}");
            Console.WriteLine($"Description: {desp}");
            Console.WriteLine($"X: {x} Y: {y} Z: {z}");
            Console.WriteLine($"------------------");
        }

    }
}
