using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApp.Entity
{
   public class Blog
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool isApproved { get; set; }
        public bool isHome { get; set; }

        public int CategoryID { get; set; }
        public Category category { get; set; }


    }
}
