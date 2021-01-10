using HtmlAgilityPack;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.IO;
using System.Net.Http;
using System.Xml;

///
/// Name: GetRate          
/// Date: 08.01.2021         
/// Description: Get currency rate from RSS
/// Create by I.Bogdanova
///

namespace GetRate
{
    public class RateInfo
    {
        public int Amount { get; set; }
        public decimal Rate { get; set; }
    }
    public class RateInfo2
    {
        public RateInfo Cur1 { get; set; }
        public RateInfo Cur2 { get; set; }
    }
    class Program
    {
        public static string GetHtml()
        {
            var tableContent = "";
            var client = new HttpClient();
            var fileContent = client.GetAsync("http://www.nbg.ge/rss.php")
                .Result.Content
                .ReadAsStringAsync().Result;
            var tmpFile = Path.GetTempFileName();
            File.WriteAllText(tmpFile, fileContent);

            using (var xmlReader = XmlReader.Create(tmpFile, new XmlReaderSettings() { Async = true }))
            {
                var parser = new RssParser();
                var feedReader = new RssFeedReader(xmlReader, parser);
                while (feedReader.Read().Result)
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        var content = feedReader.ReadContent().Result;
                        var item = parser.CreateItem(content);

                        tableContent = item.Description;
                        break;
                    }
                }
            }
            return tableContent;
        }
        public static RateInfo GetCurRate(HtmlNode r, string cur)
        {
            var columns = r.SelectNodes("td");
            var amount = 0;
            var rate = 0M;
            if (columns[0].InnerText == cur)
            {
                var t = columns[1].InnerText.Split(' ');
                int.TryParse(t[0], out amount);

                //var rate = 0M;
                decimal.TryParse(columns[2].InnerText, out rate);
                rate = rate / amount;

                return new RateInfo { Amount = amount, Rate = rate };
            }
            else if (cur == "GEL")
            {
                amount = 1;
                rate = 1;
                return new RateInfo { Amount = amount, Rate = rate };
            }
            return null;
        }
        public static RateInfo2 GetRates(string html, string cur1, string cur2)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            RateInfo rate1 = null;
            RateInfo rate2 = null;

            var table = doc.DocumentNode.SelectSingleNode("//table");
            var tableRows = table.SelectNodes("tr");

                foreach (var r in tableRows)
                {
                    var rate = GetCurRate(r, cur1);
                    if (rate != null)
                    {
                        rate1 = rate;
                        break;
                    } 
                }

                foreach (var r in tableRows)
                {
                    var rate = GetCurRate(r, cur2);
                    if (rate != null)
                    {
                        rate2 = rate;
                        break;
                    }
                }

            if (rate1 != null && rate2 != null)
            {
                return new RateInfo2
                {
                    Cur1 = rate1,
                    Cur2 = rate2
                };

            }

            return null;
        }
        public static decimal Import(string cur1, string cur2)
        {
            var tableContent = GetHtml();          
            var rateAll = GetRates(tableContent, cur1, cur2);

            if (rateAll != null)
            {               
               return rateAll.Cur1.Rate / rateAll.Cur2.Rate;
            }
            
            return 0;
        }

        static void Main(string[] args)
        {
            bool endApp = false;
            Console.WriteLine("Found currency rate from RSS\r");
            Console.WriteLine("-------------**********------------\n");

            while (!endApp)
            {

                Console.WriteLine("Enter First Currency: (format must be GEL, USD, EUR)");
                string currency1 = Console.ReadLine().ToUpper().Trim();

                Console.WriteLine("Enter Second Currency:");
                string currency2 = Console.ReadLine().ToUpper().Trim();

                var rate = Import(currency1, currency2);
                if (rate != 0)
                    Console.WriteLine("Current rate " + currency1 + " to " + currency2 + " : " + rate.ToString("N4"));
                else
                    Console.WriteLine("Currency not found... ");

                Console.WriteLine("------------**********------------\n");

                Console.Write("Press 'x' for close app, or press 'Enter'  for continue: ");
                if (Console.ReadLine() == "x") endApp = true;
            }
        }
    }
}
