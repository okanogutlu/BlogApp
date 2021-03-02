using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.ViewComponents
{
    //Burada birkaç farklı sayfada ortak olarak kullanacağımız
    //componentleri ekliyoruz. Örneğin burada sayfa solunda bulunan kategori menusu
    //için bir component oluşturuldu. View içine ategorileri gönderdik.
    //ayrıca constr. ile veritabanına bağlandık.
    //Class'ın sonu ViewComponen ile bitmek zorundadır!
    public class CategoryMenuViewComponent:ViewComponent
    {

        private ICategoryRepository _repository;
        public CategoryMenuViewComponent(ICategoryRepository repo)
        {
            _repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["id"]; //seçilen category'nin arkasını mavi yapmak için(active)
            //Default.cshtml içerisinde kullanıldı.
            return View(_repository.GetAll());
        }
    }
}
