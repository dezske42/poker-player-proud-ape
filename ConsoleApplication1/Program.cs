using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            string JSonString = System.IO.File.ReadAllText("testjson.txt");
            JObject jobj = JObject.Parse(JSonString);
            Console.WriteLine(jobj.GetValue("minimum_raise"));
            Console.Read();
        }
    }
}
