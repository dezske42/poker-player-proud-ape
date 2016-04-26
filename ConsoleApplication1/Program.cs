using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nancy.Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            string JSonString = System.IO.File.ReadAllText("testjson.txt");
            Console.Read();
        }
    }
}
