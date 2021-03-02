using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete
{
    public class BlogRepository : IBlogRepository
    {
        private BlogContext context;
        public BlogRepository(BlogContext _context)
        {
            context = _context;
        }
        public void AddBlog(Blog entity)
        {
            context.Blogs.Add(entity);
            context.SaveChanges();
        }

        public void SaveBlog(Blog entity)
        {
            if (entity.ID == 0)
            {
                context.Blogs.Add(entity);

            }
            else
            {
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                
            }
            context.SaveChanges();
        }

        public void DeleteBlog(int BlogId)
        {
            var SilinecekEleman = context.Blogs.FirstOrDefault(b => b.ID == BlogId);
            context.Blogs.Remove(SilinecekEleman);
            context.SaveChanges();
        }

        public IQueryable<Blog> GetAll()
        {
            return context.Blogs;
        }

        public Blog GetById(int blogId)
        {
            return context.Blogs.FirstOrDefault(b => b.ID == blogId);
        }

        public void UpdateBlog(Blog entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
