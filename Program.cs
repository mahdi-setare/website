using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;  //Add html agility pack for project's refrence  from htmlagilitypack.codeplex.com website

namespace Poroje_dadekavi
{
    class Program
    {

        static void Main(string[] args)
        {
            string url = Console.ReadLine();
            string url2 = Console.ReadLine();
            //-----------------------------
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);   //request for website1 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader src = new StreamReader(response.GetResponseStream());    //get html 
            string srce1 = src.ReadToEnd();                                        // save all html in sting
            MatchCollection m1 = Regex.Matches(srce1, @"<title>\s*(.+?)\s*</title>", RegexOptions.Singleline); //Specify web title
            string title1 = " ";
            foreach (Match m in m1)  //get inner text from web title
            {
                if(m.Success)
                {
                    title1 = m.Groups[1].Value;
                }
            }
            //--------------save sorce1----------------------
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"F:\sourc1.txt", true))
            {
                file.WriteLine(srce1);
            }

            //-----------------git inner text-----------------------
            HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
            HtmlWeb hw1 = new HtmlWeb();
            doc1 = hw1.Load(@"F:\sourc1.txt");
            HtmlNodeCollection nodes1 = doc1.DocumentNode.SelectNodes("//body//text()[(normalize-space(.) != '') and not(parent::script) and not(*)]");
            string result1 = " ";
            foreach (var item in nodes1)
            {
                result1 += item.InnerText;
            }
            //--------------------delete preposition and split----------------------------------
            result1 = Regex.Replace(result1, @"\ba\b|\bam\b|\bis\b|\bare\b|\bwas\b|\bwere\b|\bwill\b|\bto\b|\bof\b|\bin\b|\bthe\b|\bas\b|\bat\b|\bbut\b|\bby\b|\bfor\b|\bfrom\b|\bvia\b|\bwith\b|\band\b|\bwithin\b|\btill\b", " ");
            result1 = Regex.Replace(result1, @"\bwithout\b|\bupon\b|\bthan\b|\bper\b|\bhave\b|\bhas\b|\bdo\b|\bdoes\b|\bcould\b|\bshall\b|\bmay\b|\bmust\b|\bhad\b|\bdid\b|\bcan\b|\bmight\b|\bbeen\b|\bany\b|\ban\b"," ");
            result1 = Regex.Replace(result1, @"\b   \b|\b  \b|\b    \b|\b     \b|\b      \b|\b       \b", " ");
            string pattern = "[^\\w]"; //get all spaces and other signs, like: '.' '?' '!'
            string[] words = null;
            int cont = 0, b = 0;
            words = Regex.Split(result1, pattern, RegexOptions.IgnoreCase);
            foreach (string s in words)
            {
                if (s.ToString() != String.Empty)
                {
                    cont++;
                }
            }
            string[] ans1 = new string[cont];

            foreach (string s in words)
            {
                if (s.ToString() != string.Empty)
                {
                    ans1[b] = s;
                    b++;
                }

            }
           //---------------cont words number-----------------------
            Array.Sort(ans1);
            string temp = "";
            int size = 0;
            for (int it = 0; it < ans1.Length; ++it)
            {
                if (temp != ans1[it]) ++size;
                temp = ans1[it];
            }

            temp = ans1.Length > 0 ? ans1[0] : "";
            List<KeyValuePair<string, int>> array = new List<KeyValuePair<string, int>>();
            size = 1;
            for (int it = 1; it < ans1.Length; ++it)
            {
                if (ans1[it] == temp) ++size;
                else
                {
                    array.Add(new KeyValuePair<string, int>(temp, size));
                    size = 1;
                }
                temp = ans1[it];
            }
            array.Add(new KeyValuePair<string, int>(ans1[ans1.Length - 1], size));

         
            //------------ do same things for second website------------------
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
            //-------------save source2----------------------
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"F:\sourc2.txt", true))
            {
                file.WriteLine(srce2);
            }
            //------------------get inner text ------------------------------
            HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
            HtmlWeb hw2 = new HtmlWeb();
            doc2 = hw2.Load(@"F:\sourc2.txt");
            HtmlNodeCollection nodes2 = doc2.DocumentNode.SelectNodes("//body//text()[(normalize-space(.) != '') and not(parent::script) and not(*)]");
            string result2 = " ";
            foreach (var item in nodes2)
            {
                result2 += item.InnerText;
            }
            //------------------delete preposition and splite--------------------------
            result2 = Regex.Replace(result2, @"\ba\b|\bam\b|\bis\b|\bare\b|\bwas\b|\bwere\b|\bwill\b|\bto\b|\bof\b|\bin\b|\bthe\b|\bas\b|\bat\b|\bbut\b|\bby\b|\bfor\b|\bfrom\b|\bvia\b|\bwith\b|\band\b|\bwithin\b|\btill\b", " ");
            result2 = Regex.Replace(result2, @"\bwithout\b|\bupon\b|\bthan\b|\bper\b|\bhave\b|\bhas\b|\bdo\b|\bdoes\b|\bcould\b|\bshall\b|\bmay\b|\bmust\b|\bhad\b|\bdid\b|\bcan\b|\bmight\b|\bbeen\b|\bany\b|\ban\b", " ");
            result2 = Regex.Replace(result1, @"\b   \b|\b  \b|\b    \b|\b     \b|\b      \b|\b       \b", " ");
            string pattern2 = "[^\\w]"; //get all spaces and other signs, like: '.' '?' '!'
            string[] words2 = null;
            int cont2 = 0, b2 = 0;
            words2 = Regex.Split(result2, pattern2, RegexOptions.IgnoreCase);
            foreach (string s in words2)
            {
                if (s.ToString() != String.Empty)
                {
                    cont2++;
                }
            }
            string[] ans2 = new string[cont2];

            foreach (string s in words2)
            {
                if (s.ToString() != string.Empty)
                {
                    ans2[b2] = s;
                    b2++;
                }

            }
            //------------counting words number---------------
            //put words and words count in list
            Array.Sort(ans2);
            string temp2 = "";
            int size2 = 0;
            for (int it = 0; it < ans2.Length; ++it)
            {
                if (temp2 != ans2[it]) ++size2;
                temp2 = ans2[it];
            }

            temp2 = ans2.Length > 0 ? ans2[0] : "";
            List<KeyValuePair<string, int>> array2 = new List<KeyValuePair<string, int>>();

            size2 = 1;
            for (int it = 1; it < ans2.Length; ++it)
            {
                if (ans2[it] == temp2) ++size2;
                else
                {
                    array2.Add(new KeyValuePair<string, int>(temp2, size2));
                    size2 = 1;
                }
                temp2 = ans2[it];
            }
            array2.Add(new KeyValuePair<string, int>(ans2[ans2.Length - 1], size2));

            //----------------------------
            foreach (var g in array)
            {
                System.Console.WriteLine(g);
            }
            foreach (var g in array2)
            {
                System.Console.WriteLine(g);
            }
          
            Console.Read();
        }

    }
}
