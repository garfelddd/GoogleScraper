using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using GoogleScraper.Helpers;

namespace GoogleScraper.Executes
{
    class GetLinks
    {
        IWebDriver webDriver;
        int positions;
        List<string> keywords;
        string time;
        public GetLinks(IWebDriver webDriver, int positions, string time)
        {
            this.webDriver = webDriver;
            this.positions = positions;
            this.time = time;
        }
        public Task<string> Execute(string keyword)
        {
            return Task.Run(() =>
            {
                var encodedKeyword = HttpUtility.UrlEncode(keyword);
                string output = null;
                webDriver.Url = "https://www.google.com/search?q=" + encodedKeyword + "&num=100&ip=0.0.0.0&source_ip=0.0.0.0&ie=UTF-8&oe=UTF-8&hl=en&adtest=on&noj=1&igu=1&uule=w+CAIQIFISCQs2MuSEtepUEUK33kOSuTsc&nomo=1&nota=1&biw=1075&bih=5000&adsdiag=-6218766314297752031&" + time;
                var elements = BrowserExtensions.FindElements(webDriver, By.XPath("//a[contains(@ping, '/url') and @class='q']"));
                int i = 0;
                foreach (var element in elements)
                {
                    if (i <= this.positions)
                    {
                        i++;
                        var urlPart = element.GetAttribute("ping");
                        var match = Regex.Match(urlPart, "&url=(.*)&ved");
                        string result = match.Groups[1].Value;
                        output += result + "\r\n";
                    }
                }
                Debug.WriteLine(output);
                return output;
            });
                

        }
    }
}
