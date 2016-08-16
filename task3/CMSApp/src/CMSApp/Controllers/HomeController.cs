using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMSApp.Models;
using CMSApp.Data;

namespace CMSApp.Controllers
{
    public class HomeController : Controller
    {
        private IPageRepository pageRepository { get; set; }
      
        public HomeController(ApplicationDbContext context)
        {
            this.pageRepository = new PageRepository(context);
        }


        public IActionResult Index()
        {
            Random rnd = new Random();
            var list = pageRepository.GetAllPages().OrderBy(x => rnd.Next()).Take(3).ToList();
            ViewData["Link1"]= list.ElementAt(0);
            ViewData["Link2"]= list.ElementAt(1);
            ViewData["Link3"]= list.ElementAt(2);
           
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
