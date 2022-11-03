using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ElectronicGuideCookingWpfApp
{
    public class GuideCookingDataGateway
    {


        static string LargestCommonSubstring(string left, string right)
        {
            if (left == right)
                return left;

            for (int length = left.Length - 1; length > 1; length--)
            {
                HashSet<string> set = new HashSet<string>();
                for (int offset = 0; offset <= left.Length - length; offset++)
                {
                    set.Add(left.Substring(offset, length));
                }
                for (int offset = 0; offset <= right.Length - length; offset++)
                {
                    string substring = right.Substring(offset, length);
                    if (set.Contains(substring))
                        return substring;
                }
            }

            char[] chars = left.Distinct().OrderBy(x => x).ToArray();
            foreach (char c in right)
            {
                if (Array.BinarySearch(chars, c) >= 0)
                    return c.ToString();
            }
            return null;
        }

        public static int GetResId(string title)
        {
            string res = "";
            int id = -1, resLenghtMax = -1, resultId = -1;

            using (ApplicationContext appContext = new ApplicationContext())
            {
                var searchTitle = appContext.GuideCookings.Select(x => x.Title).ToList();

                foreach (var item in searchTitle)
                {
                    id = appContext.GuideCookings.FirstOrDefault(x => x.Title == item).Id;
                    res = LargestCommonSubstring(title.ToUpper(), item.ToUpper());
                    if (res.Length > resLenghtMax)
                    {
                        resLenghtMax = res.Length;
                        resultId = id;
                    }
                }
            }
            return resultId;
        }

        public static void Search(Image image, int id, System.Windows.Controls.TextBox tbTitle, System.Windows.Controls.Label labelCookingTime, System.Windows.Controls.TextBox tbDescription, System.Windows.Controls.ListBox listBox)
        {
            using (ApplicationContext appContext = new ApplicationContext())
            {
                appContext.GuideCookings.Load();
                var imag = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).UrlImage;
                var uri = new Uri(@imag);
                var bitmap = new BitmapImage(uri);
                image.Source = bitmap;

                var title = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).Title;
                tbTitle.Text = title;

                labelCookingTime.Content = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).CookingTime.ToString();


                tbDescription.Text = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).Description;

                List<GuideCooking> items = new List<GuideCooking>();
                List<string> products = new List<string>();

                var s = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).Products;
                s.Trim();
                string[] subs = s.Split('.');

                foreach (string sub in subs)
                {
                    if (sub != string.Empty)
                        products.Add(sub);
                }

                for (int i = 0; i < products.Count; i++)
                {
                    items.Add(new GuideCooking { Products = products[i] });

                }

                listBox.ItemsSource = items;
            }
        }
        public static void Search(System.Windows.Controls.TextBox min, System.Windows.Controls.TextBox max, Image image, int id, System.Windows.Controls.TextBox tbTitle, System.Windows.Controls.Label labelCookingTime, System.Windows.Controls.TextBox tbDescription, System.Windows.Controls.ListBox listBox)
        {
            using (ApplicationContext appContext = new ApplicationContext())
            {
                appContext.GuideCookings.Load();
                var title = appContext.GuideCookings.Where(x => x.CookingTime >= uint.Parse(min.Text) && x.CookingTime <= uint.Parse(max.Text)).FirstOrDefault(x => x.Id == id).Title;
                tbTitle.Text = title;
                var imag = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).UrlImage;
                var uri = new Uri(@imag);
                var bitmap = new BitmapImage(uri);
                image.Source = bitmap;

                labelCookingTime.Content = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).CookingTime.ToString();


                tbDescription.Text = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).Description;

                List<GuideCooking> items = new List<GuideCooking>();
                List<string> products = new List<string>();

                var s = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).Products;
                s.Trim();
                string[] subs = s.Split('.');

                foreach (string sub in subs)
                {
                    if (sub != string.Empty)
                        products.Add(sub);
                }

                for (int i = 0; i < products.Count; i++)
                {
                    items.Add(new GuideCooking { Products = products[i] });
                }

                listBox.ItemsSource = items;
            }
        }
    }
}
