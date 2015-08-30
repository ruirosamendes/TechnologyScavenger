using System;
using System.Collections.Generic;
using System.IO;
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

        private string GetSafeFilename(string filename)
        {

            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));

        }

        private static void CreateFolder(string technologyFolderPath)
        {
            if (!Directory.Exists(technologyFolderPath))
            {
                Directory.CreateDirectory(technologyFolderPath);
            }
            else
            {
                Directory.Delete(technologyFolderPath, true);
                Directory.CreateDirectory(technologyFolderPath);
            }
        }

        private Dictionary<string, string> CreateTechnologyFolders()
        {
            Dictionary<string, string> technologyFolderPaths = new Dictionary<string, string>() ;
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + mTechnology.Name;

            technologyFolderPaths.Add("OK", folderPath + "\\OK");
            technologyFolderPaths.Add("NOTOK", folderPath + "\\NOTOK");
            technologyFolderPaths.Add("NA", folderPath + "\\NA");

            foreach(string subFolderPath in technologyFolderPaths.Values)
            {
                CreateFolder(subFolderPath);
            }    

            return technologyFolderPaths;
        }

      
        public TechnologyFinder(ITechnology technology, List<string> siteURLs)
        {
            this.mTechnology = technology;
            this.mSiteURLs = siteURLs;
            this.mSiteURLsWithTheTechnology = new List<string>();
        }

        public void RunCrawler()
        {
            Dictionary<string, string> technologyFolderPaths = CreateTechnologyFolders();

            foreach (string siteURL in this.mSiteURLs)
            {
                string siteURLAbsolutePath;
                if (siteURL.StartsWith("http://") || siteURL.StartsWith("https://"))
                    siteURLAbsolutePath = siteURL;
                else
                    siteURLAbsolutePath = "http://" + siteURL;

                string safeSiteURLFileName = GetSafeFilename(siteURL) + ".html";
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
                                client.DownloadFile(siteUri, technologyFolderPaths["OK"] + "\\" + safeSiteURLFileName);
                            }
                            else
                            {
                                client.DownloadFile(siteUri, technologyFolderPaths["NOTOK"] + "\\" + safeSiteURLFileName);
                            }
                        }
                        catch (WebException)
                        {
                            FileStream file = File.Create(technologyFolderPaths["NA"] + "\\" + GetSafeFilename(siteURL));
                            file.Close();
                        }
                    }
                }
                else
                {
                    File.Create(technologyFolderPaths["NA"] + "\\" + GetSafeFilename(siteURL));
                }
            }
        }

        public void WriteFoundSites()
        {
            foreach(string url in mSiteURLsWithTheTechnology)
            {
                Console.WriteLine(url);
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
