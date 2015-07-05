using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TechnologyScavenger.Service
{
    public class SitesLoader
    {
        private List<string> mSitesURLs;

        public SitesLoader(Stream sitesFile)
        {
            mSitesURLs = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(sitesFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        mSitesURLs.Add(line);
                        Console.WriteLine(line);
                    }            
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public List<string> SitesURLs
        {
            get
            {
                return mSitesURLs;
            } 
        }
    }
}
