using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyScavenger.Service
{
    public class TechnologyFinder
    {
        private readonly List<string> mSiteURLs;
        private ITechnology mTechnology;
        private List<string> mSiteURLsWithTheTechnology;


        private bool HasTechnologyInHTML(List<string> technologyPatternsStrings, string siteHTMLSource)
        {
            foreach(string patternString in technologyPatternsStrings)
            {
                if (siteHTMLSource.Contains(patternString))
                    return true;            
            }
            return false;
        }

        public TechnologyFinder(ITechnology technology, List<string> siteURLs)
        {
            this.mTechnology = technology;
            this.mSiteURLs = siteURLs;
            this.mSiteURLsWithTheTechnology = new List<string>();
        }

        public void RunCrawler()
        {

            foreach (string siteURL in this.mSiteURLs)
            {
                string siteURLAbsolutePath;
                if (siteURL.StartsWith("http://") || siteURL.StartsWith("https://"))
                    siteURLAbsolutePath = siteURL;
                else
                    siteURLAbsolutePath = "http://" + siteURL;

                Uri siteUri;
                if (Uri.TryCreate(siteURLAbsolutePath, UriKind.RelativeOrAbsolute, out siteUri))
                {
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            string siteHTMLSource = client.DownloadString(siteUri);
                            if (HasTechnologyInHTML(mTechnology.PatternsStrings, siteHTMLSource))
                            {
                                this.mSiteURLsWithTheTechnology.Add(siteURL);
                            }
                            else
                            {
                                client.DownloadFile(siteUri, AppDomain.CurrentDomain.BaseDirectory + siteURL + ".txt");
                            }
                        }
                        catch(WebException e)
                        {
                            
                        }
                       
                    }
                }
            }
        }

        public List<string> SiteURLsWithTheTechnology
        {
            get
            {
                return mSiteURLsWithTheTechnology;
            }
        }

    }
}
