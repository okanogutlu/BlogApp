using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private BlogContext context;
        public CategoryRepository(BlogContext _context)
        {
            context = _context;
        }
        public void AddCategory(Category entity)
        {
            context.Categorys.Add(entity);
            context.SaveChanges();
        }

        public void DeleteCategory(int BlogId)
        {
            var SilinecekEleman = context.Categorys.FirstOrDefault(b => b.ID == BlogId);
            context.Categorys.Remove(SilinecekEleman);
            context.SaveChanges();
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categorys;
        }

        public Category GetById(int blogId)
        {
            return context.Categorys.FirstOrDefault(b => b.ID == blogId);
        }

        public void SaveCategory(Category entity)
        {
            if (entity.ID == 0)
            {
                context.Categorys.Add(entity);

            }
            else
            {
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            }
            context.SaveChanges();
        }

        public void UpdateCategory(Category entity)
        {
            var DegistirilecekEleman = context.Categorys.FirstOrDefault(b => b.ID == entity.ID);
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
