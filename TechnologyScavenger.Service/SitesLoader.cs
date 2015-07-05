using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TechnologyScavenger.Service
{
    public class SitesLoader
    {
        private FileStream sitesFile;

        public SitesLoader(FileStream sitesFile)
        {
            this.sitesFile = sitesFile;
        }
    }
}
