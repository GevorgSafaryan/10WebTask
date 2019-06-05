using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10WebTask
{
    public class PageObjectsFactory : CorePage
    {
        public HomePage HomePage
        {
            get
            {
                return new HomePage(WebDriver);
            }
        }
    }
}
