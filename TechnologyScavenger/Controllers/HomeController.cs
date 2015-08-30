using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnologyScavenger.Models;
using TechnologyScavenger.Service;

namespace TechnologyScavenger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Technology(string id)
        {
            string technologyName = id;
            string patternsFilePath = Server.MapPath("~/App_Data/") + technologyName + "StringPatterns.txt";
            FileStream patternsFile = new FileStream(patternsFilePath, FileMode.Open);
            ITechnology technology = new Technology("technologyName", patternsFile);
            string sitesFilePath = Server.MapPath("~/App_Data/") + technologyName + "SitesURLs.txt";
            FileStream sitesFile = new FileStream(sitesFilePath, FileMode.Open);
            SitesLoader sitesLoader = new SitesLoader(sitesFile);

            TechnologyFinder finder = new TechnologyFinder(technology, sitesLoader.SitesURLs);
            finder.RunCrawler();

            TechnologyViewModel model = new TechnologyViewModel();
            model.Name = technology.Name;
            model.URLs = finder.SiteURLsWithTheTechnology;

            ViewData["Title"] = technologyName;
            ViewData["Message"] = "It's the " + technologyName + " message to view page";

            return View(model);
        }

    }
}