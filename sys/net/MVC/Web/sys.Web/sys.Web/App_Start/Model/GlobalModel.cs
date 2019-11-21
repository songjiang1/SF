using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sys.Web.Model
{
    public class GlobalModel
    { 
        public List<LinkItem> GunDongTus { get; set; } 
    }
    public class LinkItem
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
        public string Href { get; set; }
        public DateTime Create { get; set; }
        public List<LinkItem> GunDongTus { get; set; }
    }
    public class FoIndex
    {
        public List<LinkItem> ZhuanTis { get; set; }

        public List<LinkItem> ShiPins { get; set; }

        public List<LinkItem> DongTais { get; set; }

        public List<LinkItem> LunTans { get; set; }

        public List<LinkItem> LunTanTus { get; set; }

        public List<LinkItem> GongGaos { get; set; }
    }
}