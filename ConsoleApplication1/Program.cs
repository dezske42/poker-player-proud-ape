using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
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
            Poker poker = new Poker(jobj);
            Console.WriteLine("http://rainman.leanpoker.org/rank?cards=" + poker.AllCardsJSon);


            System.Net.WebRequest request;
            request = WebRequest.Create("http://rainman.leanpoker.org/rank?cards="+poker.AllCardsJSon);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader resStreamReader = new StreamReader(resStream);
            String streamString = resStreamReader.ReadToEnd();
            Console.WriteLine(streamString);
            JObject rankingObj = JObject.Parse(streamString);
            Console.WriteLine(rankingObj.GetValue("kickers"));
            Console.Read();
        }
    }
}
