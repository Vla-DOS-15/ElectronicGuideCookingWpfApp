using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGuideCookingWpfApp
{
    public class GuideCooking
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlImage { get; set; }
        public string Description { get; set; }
        public uint CookingTime { get; set; }
        public string Products { get; set; }
        public string Type { get; set; }
    }
}
