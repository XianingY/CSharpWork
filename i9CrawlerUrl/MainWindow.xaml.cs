using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCrawlerApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<PhoneNumberInfo> phoneNumbers = new ObservableCollection<PhoneNumberInfo>();

        public MainWindow()
        {
            InitializeComponent();
            lvPhoneNumbers.ItemsSource = phoneNumbers;
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtKeyword.Text;
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("请输入关键字！");
                return;
            }

            btnSearch.IsEnabled = false;
            phoneNumbers.Clear();

            await CrawlAsync(keyword);

            btnSearch.IsEnabled = true;
        }

        private async Task CrawlAsync(string keyword)
        {
            using (HttpClient client = new HttpClient())
            {
                string searchUrl = $"https://www.bing.com/search?q={Uri.EscapeDataString(keyword)}";
                string searchResult = await client.GetStringAsync(searchUrl);

                var urls = ExtractUrls(searchResult);
                foreach (var url in urls)
                {
                    if (phoneNumbers.Count >= 100)
                        break;

                    try
                    {
                        string pageContent = await client.GetStringAsync(url);
                        ExtractPhoneNumbers(url, pageContent);
                    }
                    catch (Exception ex)
                    {
                        // 处理请求失败的情况
                        Console.WriteLine($"请求失败：{ex.Message}");
                    }
                }
            }
        }

        private List<string> ExtractUrls(string htmlContent)
        {
            var urls = new List<string>();
            string pattern = @"href=\""(?<url>http[^\\""]+)\""";

            foreach (Match match in Regex.Matches(htmlContent, pattern))
            {
                string url = match.Groups["url"].Value;
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute) && !urls.Contains(url))
                {
                    urls.Add(url);
                }
            }

            return urls;
        }

        private void ExtractPhoneNumbers(string url, string htmlContent)
        {
            string phonePattern = @"\(?\d{3}\)?-?\s*-?\d{3}-?\s*-?\d{4}";

            foreach (Match match in Regex.Matches(htmlContent, phonePattern))
            {
                string phoneNumber = match.Value;
                if (!phoneNumbers.Any(p => p.PhoneNumber == phoneNumber))
                {
                    phoneNumbers.Add(new PhoneNumberInfo { PhoneNumber = phoneNumber, Url = url });
                }
            }
        }
    }

    public class PhoneNumberInfo
    {
        public string PhoneNumber { get; set; }
        public string Url { get; set; }
    }
}
