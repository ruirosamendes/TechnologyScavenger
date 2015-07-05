using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace TechnologyScavenger.Service.Tests
{

    [TestFixture()]
    public class SitesLoaderUnitTest
    {
        [Test()]
        public void LoadSitesFromFile()
        {
            FileStream sitesFile = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + @"\\alexa1M.txt", FileMode.Open);
            SitesLoader sitesLoader = new SitesLoader(sitesFile);
             
            Assert.AreEqual(1, 1);
        }

    }
}
