using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageAdmin.Models.ViewModel
{
    public class SiteGridViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public List<SiteGridViewModel> Sites { get; set; }


        public SiteGridViewModel()
        {

        }

        public SiteGridViewModel(Site site)
        {
            ID = site.ID;
            Name = site.Name;
            Url = site.Url;
            Active = site.Active;
        }

        public SiteGridViewModel(List<Site> sites)
        {
            Sites = new List<SiteGridViewModel>();
            foreach (var site in sites)
            {
                var s = new SiteGridViewModel(site);
                Sites.Add(s);
            }
        }
    }

}