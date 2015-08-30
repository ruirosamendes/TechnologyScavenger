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

        private static void SetupShopifyTechnology(out ITechnology technology, out SitesLoader sitesLoader)
        {
            FileStream patternsFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\ShopifyStringPatterns.txt", FileMode.Open);
            technology = new Technology("Shopify", patternsFile);
            FileStream sitesFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\ShopifySitesURLs.txt", FileMode.Open);
            sitesLoader = new SitesLoader(sitesFile);
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
            Assert.AreEqual(7, finder.SiteURLsWithTheTechnology.Count);
            finder.WriteFoundSites();
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

        [Test()]
        public void TechnologyFinderRunCrawlerOnShopify()
        {
            ITechnology technology;
            SitesLoader sitesLoader;
            SetupShopifyTechnology(out technology, out sitesLoader);

            TechnologyFinder finder = new TechnologyFinder(technology, sitesLoader.SitesURLs);
            finder.RunCrawler();

            Assert.AreEqual(5, finder.SiteURLsWithTheTechnology.Count);
        }


        [Test()]
        public void TechnologyFinderRunCrawlerCreatesFoldersOKNOTandNA()
        {
            ITechnology technology;
            SitesLoader sitesLoader;
            SetupShopifyTechnology(out technology, out sitesLoader);

            TechnologyFinder finder = new TechnologyFinder(technology, sitesLoader.SitesURLs);
            finder.RunCrawler();

            Assert.IsTrue(Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + technology.Name + "\\OK"));
            Assert.IsTrue(Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + technology.Name + "\\NOTOK"));
            Assert.IsTrue(Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + technology.Name + "\\NA"));
        }


        [Test()]
        public void TechnologyFinderSaveMainPageSourceToTechonologySubFolderOK()
        {
            ITechnology technology;
            SitesLoader sitesLoader;
            SetupShopifyTechnology(out technology, out sitesLoader);

            TechnologyFinder finder = new TechnologyFinder(technology, sitesLoader.SitesURLs);
            finder.RunCrawler();

            int sourcePagesCount = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + technology.Name + "\\OK").Length;
            Assert.AreEqual(5, sourcePagesCount);
        }

      
        [Test()]
        public void TechnologyFinderSaveMainPageSourceToTechonologySubFolderNOTOK()
        {
            ITechnology technology;
            SitesLoader sitesLoader;
            SetupShopifyTechnology(out technology, out sitesLoader);

            TechnologyFinder finder = new TechnologyFinder(technology, sitesLoader.SitesURLs);
            finder.RunCrawler();

            int sourcePagesCount = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + technology.Name + "\\NOTOK").Length;
            Assert.AreEqual(3, sourcePagesCount);
        }

        [Test()]
        public void TechnologyFinderSaveMainPageSourceToTechonologySubFolderNA()
        {
            ITechnology technology;
            SitesLoader sitesLoader;
            SetupShopifyTechnology(out technology, out sitesLoader);

            TechnologyFinder finder = new TechnologyFinder(technology, sitesLoader.SitesURLs);
            finder.RunCrawler();

            int sourcePagesCount = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + technology.Name + "\\NA").Length;
            Assert.AreEqual(2, sourcePagesCount);
        }

    }



}
