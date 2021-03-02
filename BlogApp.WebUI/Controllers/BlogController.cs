using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.WebUI.Controllers
{
    public class BlogController : Controller 
    {
        IBlogRepository blogrepository;
        ICategoryRepository categoryrepository;
        public BlogController(IBlogRepository _repoBlog,ICategoryRepository _repoCategory)
        {
            blogrepository = _repoBlog;
            categoryrepository = _repoCategory;
        }
        public IActionResult List()
        {
            return View(blogrepository.GetAll());
        }

        public IActionResult AddorUpdate(int? id)
        {
            ViewBag.Categories = new SelectList(categoryrepository.GetAll(), "ID", "Name");

            if (id == null)
            {
                return View(new Blog()); //view içerisindeki hidden input id olarak sorun çıkarıyor. bunun için bu eklenti yapıldı.
            }
            else
            {
                return View(blogrepository.GetById((int)id));
            }
        }
        public IActionResult Details(int id)
        {
            return View(blogrepository.GetById(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddorUpdate(Blog entity,IFormFile file)//buradan fotoğraf da yüklüyoruz.
            //Istendiği takdirde gerekli ayarlamalarla birden fazla dosya da alınabilir.
        {
            if (ModelState.IsValid)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);//Kaydedeceğimiz dosyanın path'i.

                if (file!=null)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);//Dosya kaydediliyor.
                    }
                    entity.Image = file.FileName;//Aynı zamanda veritabanındaki ismi de değiştiriliyor.
                }
                
                //Yani dosya veritabanına kaydedilmiyor, dosyanın ismi veritabanına kaydediliyor.Dosya wwwroot'a kaydediliyor.
                blogrepository.SaveBlog(entity);
                TempData["message"] = $"{entity.Title} kayıt eklendi.";
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(categoryrepository.GetAll(), "ID", "Name");
            return View(entity);
        }
        public IActionResult Index(int? id, string q)
        {
            var query = blogrepository.GetAll().Where(i => i.isApproved);
            if (id != null)
            {
                return View(query.Where(i => i.CategoryID == id));

            }
            if (!string.IsNullOrEmpty(q))
            {
                return View(query.Where(i => EF.Functions.Like(i.Title, "%" +q+"%")|| EF.Functions.Like(i.Description, "%" + q + "%")|| EF.Functions.Like(i.Body, "%" + q + "%")));
            }
                return View(query);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            blogrepository.DeleteBlog(id);
            return RedirectToAction("List");
        }
    }
}
