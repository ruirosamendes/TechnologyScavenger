using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyScavenger.Service.Tests
{
    [TestFixture()]
    public class TechnologyFinderUnitTests
    {
        [OneTimeSetUp]
        public void TestSetup()
        {

        }

        [Test()]
        public void TechnologyFinderMustSetupTechnologyOnConstructor()
        {
            FileStream patternsFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\MagentoStringPatterns.txt", FileMode.Open);
            ITechnology technology = new Technology("Magento", patternsFile);
            FileStream sitesFile = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + @"\\MagentoSitesURLs.txt", FileMode.Open);
            SitesLoader sitesLoader = new SitesLoader(sitesFile);
            TechnologyFinder finder = new TechnologyFinder(technology, sitesLoader.SitesURLs);
            Assert.IsNotNull(finder);
        }

        [Test()]
        public void TechnologyFinderRunCrawlerOnMagento()
        {
            FileStream patternsFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\MagentoStringPatterns.txt", FileMode.Open);
            ITechnology technology = new Technology("Magento", patternsFile);
            FileStream sitesFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\MagentoSitesURLs.txt", FileMode.Open);
            SitesLoader sitesLoader = new SitesLoader(sitesFile);

            TechnologyFinder finder = new TechnologyFinder(technology, sitesLoader.SitesURLs);
            finder.RunCrawler();
            
            Assert.AreEqual(finder.SiteURLsWithTheTechnology.Count, 7);
        }

        [Test()]
        public void TechnologyFinderRunCrawlerCantCrashWithABadURL()
        {
            FileStream patternsFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\MagentoStringPatterns.txt", FileMode.Open);
            ITechnology technology = new Technology("Magento", patternsFile);
            List<string> badURL = new List<string> { "w1jafpi3rvcdf" };
            TechnologyFinder finder = new TechnologyFinder(technology, badURL);
            finder.RunCrawler();


        }


    }



}
