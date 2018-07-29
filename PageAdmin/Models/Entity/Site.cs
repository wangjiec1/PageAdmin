using System;
using System.Collections;
using System.Collections.Generic;

namespace PageAdmin.Models
{
    public class Site
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<AdminUser> Users { get; set; }
    }
}