﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class EditArticles
    {
       public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Account { get; set; }

        public Guid UserID { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int ID { get; set; }
    }
}
