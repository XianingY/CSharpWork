using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace h1.Crawler
{
    public partial class Form1 : Form
    {
        private Hashtable urls = new Hashtable();
        private List<string> errorUrls = new List<string>();
        private int count = 0;
        private string startUrl;
        private Uri baseUri;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            startUrl = textBoxUrl.Text;
            baseUri = new Uri(startUrl);
            urls.Clear();
            errorUrls.Clear();
            listBoxCrawledUrls.Items.Clear();
            listBoxErrorUrls.Items.Clear();
            urls.Add(startUrl, false);

            new Thread(Crawl).Start();
        }

        private void Crawl()
        {
            Console.WriteLine("开始爬行了....");
            while (true)
            {
                string current = null;
                lock (urls)
                {
                    foreach (string url in urls.Keys)
                    {
                        if ((bool)urls[url]) continue;
                        current = url;
                        break;
                    }
                }

                if (current == null || count > 100) break;

                Console.WriteLine("爬行" + current + "页面！");

                string html = DownLoad(current);

                lock (urls)
                {
                    urls[current] = true;
                    count++;
                }

                Parse(html, current);
            }
            Console.WriteLine("爬行结束");
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);

                string fileName = count.ToString() + ".html";
                File.WriteAllText(fileName, html, Encoding.UTF8);

                Invoke(new Action(() =>
                {
                    listBoxCrawledUrls.Items.Add(url);
                }));

                return html;
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    listBoxErrorUrls.Items.Add($"{url}: {ex.Message}");
                }));
                return "";
            }
        }

        public void Parse(string html, string currentUrl)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\'', '#', ' ', '>');

                if (strRef.Length == 0) continue;

                Uri absoluteUri;
                if (!Uri.TryCreate(baseUri, strRef, out absoluteUri) || !absoluteUri.Host.Equals(baseUri.Host))
                    continue;

                strRef = absoluteUri.ToString();

                lock (urls)
                {
                    if (urls[strRef] == null)
                    {
                        urls[strRef] = false;
                    }
                }
            }
        }
    }
}
