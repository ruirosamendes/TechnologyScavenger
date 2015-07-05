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
    public class SitesLoaderUnitTests
    {
        [Test()]
        public void LoadSitesFromFileMustSucessfullReadSitesFile()
        {
            FileStream sitesFile = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + @"\\alexa1M.txt", FileMode.Open);
            SitesLoader sitesLoader = new SitesLoader(sitesFile);
            Assert.IsNotNull(sitesLoader);
        }

        [Test()]
        public void LoadSitesMustSaveAListOfFilesInMemory()
        {
            FileStream sitesFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\alexa1M.txt", FileMode.Open);
            SitesLoader sitesLoader = new SitesLoader(sitesFile);

            List<string> sites = sitesLoader.SitesURLs;
            
            Assert.AreEqual(sites.Count, 1000000);
        }

        [Test()]
        public void LoadMagentoSitesMustSaveAListOfFilesInMemory()
        {
            FileStream sitesFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\MagentoSitesURLs.txt", FileMode.Open);
            SitesLoader sitesLoader = new SitesLoader(sitesFile);

            List<string> sites = sitesLoader.SitesURLs;

            Assert.AreEqual(sites.Count, 10);
        }

    }
}
