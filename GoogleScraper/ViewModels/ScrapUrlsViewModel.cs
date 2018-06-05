using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleScraper.ViewModels
{
    class ScrapUrlsViewModel : BaseViewModel
    {
        public string keywords
        {
            get
            {
                return keywords;
            }
            set
            {
                keywords = value;
            }
        }

    }
}
