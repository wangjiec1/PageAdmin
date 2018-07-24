using System;
using System.Collections;
using System.Collections.Generic;

namespace PageAdmin.Models
{
    public class Site
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AdminUser> Users { get; set; }
    }
}