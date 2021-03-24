using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace BooksTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load our page by url using HtmlWeb
            HtmlWeb web = new HtmlWeb();
            string url = "https://allitbooks.net/";
            int pages = 3;

            HtmlDocument document = web.Load(url);
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//div[@class='post-thumbnail']/a").ToArray();

            //Get href from all link tags
            List<string> link = new List<string>();
            if (pages == 1)
            {
                foreach (HtmlNode item in nodes)
                {
                    link.Add(item.Attributes["href"].Value);
                    Console.WriteLine(item.Attributes["href"].Value);
                }
            }else
            {

                for(int i = 1; i <= pages; i++)
                {
                    if (i != 1)
                        url = "https://allitbooks.net/" + "page-" + i + ".html";

                    document = web.Load(url);
                    nodes = document.DocumentNode.SelectNodes("//div[@class='post-thumbnail']/a").ToArray();

                    foreach (HtmlNode item in nodes)
                    {
                        link.Add(item.Attributes["href"].Value);
                        Console.WriteLine(item.Attributes["href"].Value);
                    }
                }
            }
            

            //Get Books Number from Url for dowloading page

            List<string> sortedLink = new List<string>();
            List<string> booksName = new List<string>();
            foreach (var item in link)
            {
                sortedLink.Add(item.Split('/')[2].Split('-')[0]);
                booksName.Add(string.Join(' ', item.Split('/')[2].Split('-').Skip(1)));
                Console.WriteLine(item.Split('/')[2].Split('-')[0]);
                //Console.WriteLine(string.Join(' ', item.Split('/')[2].Split('-').Skip(1)));
            }

            string dUrl = "download-file-";
            string eUrl = ".html";

            // Download books by their url number from sordet list

            url = "https://allitbooks.net/";
            var index = 0;

            using (WebClient wc = new WebClient())
            {
                foreach (var item in sortedLink)
                {
                    var main = url + dUrl + item + eUrl;
                    Console.WriteLine(main);

                    var save = "C:/Users/olesm/source/repos/BooksTest/BooksTest/books/" + booksName.ElementAt(index) + ".pdf";
                    Console.WriteLine(save);
                    wc.DownloadFileCompleted += (sender, e) => Console.WriteLine("Finished");
                    wc.DownloadFileAsync(new Uri(main), save);
                    index++;
                    while (wc.IsBusy)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }

            Console.WriteLine("");
            Console.ReadLine();
        }

        static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Download completed!");
        }

    }
}
