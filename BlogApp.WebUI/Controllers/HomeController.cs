using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogApp.WebUI.Models;
using BlogApp.Data.Abstract;

namespace BlogApp.WebUI.Controllers
{
    
    //dotnet add BlogApp.WebUI package Microsoft.EntityFrameworkCore.Tools
    //komutu, eğer ki database'de bir tabloyu eklediyseniz ypdate için bir başlangıçtır
    //sonra add-migration,enable-migration,update-database komutları kullanılır.
    public class HomeController : Controller
    {
        private IBlogRepository blogRepository;
        //Bize aslında blogrepositoy sınıfını getirecek.
        public HomeController(IBlogRepository repository)
        {
            blogRepository = repository;
        }
        private readonly ILogger<HomeController> _logger;

        

        public IActionResult Index()
        {
            return View(blogRepository.GetAll().Where(i => i.isApproved && i.isHome));
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
