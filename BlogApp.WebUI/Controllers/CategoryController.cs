using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository repository;
        public CategoryController(ICategoryRepository _repo)
        {
            repository = _repo;
        }
        public IActionResult List()
        {
            return View(repository.GetAll());
        }
        public IActionResult Index()
        {
            return View();
        }
        

        public IActionResult AddorUpdate(int? id)
        {
            if (id == null)
            {
                return View(new Category()); //view içerisindeki hidden input id olarak sorun çıkarıyor. bunun için bu eklenti yapıldı.
            }
            else
            {
                return View(repository.GetById((int)id));
            }
        }

        [HttpPost]
        public IActionResult AddorUpdate(Category entity)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(entity);
                TempData["message"] = $"{entity.Name} kayıt eklendi.";
                return RedirectToAction("List");
            }
            return View(entity);
        }
    }
}

