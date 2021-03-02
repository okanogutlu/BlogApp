using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Entity
{
   public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Blog> blogs { get; set; }

    }
}
