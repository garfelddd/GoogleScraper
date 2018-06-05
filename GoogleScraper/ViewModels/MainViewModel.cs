using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using GoogleScraper.Models;
using GoogleScraper.Helpers;
using GoogleScraper.Executes;

namespace GoogleScraper.ViewModels
{
    class MainViewModel  : BaseViewModel
    {
        public ObservableCollection<TimeGoogleModel> TimeGoogleList { get; set; }
        public DelegateCommand ShowCommand { get; set; }
        public IWebDriver webDriver;
        private string _output;
        public string output
        {
            get
            {
                return _output;
            }
            set
            {
                if (_output != value)
                {
                    _output = value;
                    this.RaisePropertyChanged<string>(() => this.output);
                }
            }
        }
        private string _input;
        public string input
        {
            get
            {
                return _input;
            }
            set
            {
                if (_input != value)
                {
                    _input = value;
                    this.RaisePropertyChanged<string>(() => this.input);
                }
            }
        }
        private TimeGoogleModel _selectedTime;
        public TimeGoogleModel selectedTime
        {
            get
            {
                if (_selectedTime == null)
                    return TimeGoogleList[0];
                    return _selectedTime;
            }
            set
            {
                _selectedTime = value;
                this.RaisePropertyChanged<TimeGoogleModel>(() => this.selectedTime);
            }
        }
        private int _positions;
        public int positions
        {
            get
            {
                return _positions;
            }
            set
            {
                _positions = value;
                RaisePropertyChanged(() => this.positions);
            }
        }
        public MainViewModel()
        {
            SetGoogleTime();

            this.ShowCommand = new DelegateCommand(async () => await ShowExecute());
        }

        public async Task ShowExecute()
        {
            if (webDriver == null)
                webDriver = Browser.Initialize();
            
            List<string> keywords = new List<string>(
               input.Split(new string[] { "\r\n" },
               StringSplitOptions.RemoveEmptyEntries));

                GetLinks getLinks = new GetLinks(webDriver, this.positions, this.selectedTime.urlPart);

                foreach (string keyword in keywords)
                {
                    this.output += await getLinks.Execute(keyword);
                }

            

        }
        public void SetGoogleTime()
        {
            this.TimeGoogleList = new ObservableCollection<TimeGoogleModel>();
            TimeGoogleList.Add(new TimeGoogleModel() { description = "whenever", urlPart = "" });
            TimeGoogleList.Add(new TimeGoogleModel() { description = "last hour", urlPart = "tbs=qdr:h" });
            TimeGoogleList.Add(new TimeGoogleModel() { description = "last 24 hours", urlPart = "tbs=qdr:d" });
            TimeGoogleList.Add(new TimeGoogleModel() { description = "last week", urlPart = "tbs=qdr:w" });
            TimeGoogleList.Add(new TimeGoogleModel() { description = "last month", urlPart = "tbs=qdr:m" });
            TimeGoogleList.Add(new TimeGoogleModel() { description = "last year", urlPart = "tbs=qdr:y" });
        }
    }

}
