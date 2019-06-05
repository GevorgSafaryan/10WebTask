using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10WebTask
{
    public class BaseTest : PageObjectsFactory
    {
        [SetUp]
        public void Setup()
        {
            Init();
        }

        [TearDown]
        public void TearDown()
        {
            CleanUp();
        }
    }
}
