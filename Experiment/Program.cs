using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Nancy.Simple;

namespace Experiment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HERE!!!");
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            string jsonstring = System.IO.File.ReadAllText("testjson.txt");
            JObject jo = JObject.Parse(jsonstring);
            
            Console.Write("small_blind: ");
            Console.WriteLine(jo.GetValue("small_blind"));
            Console.Read();
        }
    }
}
