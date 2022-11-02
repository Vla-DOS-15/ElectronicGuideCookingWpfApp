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

        //public GuideCooking(string id, string title, string urlImage, string description, uint cookingTime, string products, string type)
        //{
        //    Id = id;
        //    Title = title;
        //    UrlImage = urlImage;
        //    Description = description;
        //    CookingTime = cookingTime;
        //    Products = products;
        //    Type = type;

        //}
        //public GuideCooking(string id, string title, string urlImage, string description, uint cookingTime, string type)
        //{
        //    Id = id;
        //    Title = title;
        //    UrlImage = urlImage;
        //    Description = description;
        //    CookingTime = cookingTime;
        //    Type = type;

        //}
        //public GuideCooking() { }

        //public static string WriteTitle(GuideCooking d)
        //{
        //    return d.Title;
        //}
        //public static uint WriteCookingTime(GuideCooking d)
        //{
        //    return d.CookingTime;
        //}
        //public static string WriteDescription(GuideCooking t)
        //{
        //    return t.Description;
        //}
        //public override string ToString()
        //{
        //    return $"{Id} {Title} {UrlImage} {Description} {CookingTime} {Products} {Type}";
        //}
    }
}
