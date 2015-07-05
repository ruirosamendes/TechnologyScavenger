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
    public class TechnologyUnitTests
    {
        [Test()]
        public void TechnologyConstructorMustSucessfullReadStringsPatternsFileAndSetName()
        {
            FileStream patternsFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\\MagentoStringPatterns.txt", FileMode.Open);
            Technology technology = new Technology("Magento",  patternsFile);
            List<string> patterns = technology.PatternsStrings;

            
            Assert.AreEqual(technology.Name, "Magento");
            Assert.AreEqual(patterns.Count, 5);
            Assert.AreEqual(patterns[0], "Varien");
            Assert.AreEqual(patterns[1], "/skin/frontend/");
            Assert.AreEqual(patterns[2], "/app/design/frontend");
            Assert.AreEqual(patterns[3], "/giftcard/customer");
        }
    }
}
