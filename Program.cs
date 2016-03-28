using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Poroje_dadekavi
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string url = Console.ReadLine();
            string url2 = Console.ReadLine();
            //-----------------------------
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader src = new StreamReader(response.GetResponseStream());    
            string srce = src.ReadToEnd();
            MatchCollection m1 = Regex.Matches(srce, @"<title>\s*(.+?)\s*</title>", RegexOptions.Singleline);
            string title1 = " ";
            foreach (Match m in m1)
            {
                if(m.Success)
                {
                    title1 = m.Groups[1].Value;
                }
            }
            //------------------------------
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
            HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
            StreamReader src2 = new StreamReader(response2.GetResponseStream());
            string srce2 = src2.ReadToEnd();
            MatchCollection m2 = Regex.Matches(srce2, @"<title>\s*(.+?)\s*</title>", RegexOptions.Singleline);
            string title2 = " ";
            foreach (Match m in m2)
            {
                if (m.Success)
                {
                    title2 = m.Groups[1].Value;
                }
            }
            //-----------------------------------

            Console.WriteLine(title1);
            Console.WriteLine(title2);
            Console.Read();
        }

    }
}
