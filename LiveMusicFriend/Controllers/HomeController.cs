using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LiveMusicFriend.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(string band)
        {
            Models.TestBST tbst = new Models.TestBST();
            tbst.Test();


            //Models.Derived derived = new Models.Derived();
            //derived.virtualMethod();
            //derived.Arrays();


            Models.Search search = new Models.Search();
            ViewData.Add("search", search);

            return View("Search");
        }

        [HttpPost]
        public ActionResult Index(LiveMusicFriend.Models.Search search)
        {
            Models.Jambase jb = new Models.Jambase(search);
            search = jb.getRest();
            ViewData.Add("search", search);

            return View("Search");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Here it is.  Developer Derek's first killer web site.  I intend this website to answer the question, 'What the heck am I going to do tonight'?  Initially, there will be concert info, but there is more to come.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Want features added?  Find a problem?";

            return View();
        }

        public ActionResult Get()
        {
            ViewBag.Message = "http rest request";

            return View();
        }

        public ActionResult GetHttp()
        {
            return View("Get");
        }

        [HttpGet]
        public ActionResult Search(string type, string target)
        {
            Models.Search search = new Models.Search();
            if (type != null && target != null && type.ToLower() == "artist")
            {
                search.artist = target;
                Models.Jambase jb = new Models.Jambase(search);
                search = jb.getRest();
            }
            ViewData.Add("search", search);

            return View();
        }

        [HttpPost]
        public ActionResult Search(LiveMusicFriend.Models.Search search)
        {
            if (!string.IsNullOrEmpty(search.artist))
            {
                Models.Jambase artistSearch = new Models.Jambase(search);
                search.artistid = artistSearch.getArtistId();
            }
            Models.Jambase jb = new Models.Jambase(search);
            search = jb.getRest();
            ViewData.Add("search", search);

            return View();
        }
    }
}
