using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectronicGuideCookingWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ApplicationContext appContext;

            try
            {
                InitializeComponent();
                using (appContext = new ApplicationContext())
                {
                    appContext.GuideCookings.Load();
                    var imag = appContext.GuideCookings.FirstOrDefault(x => x.Id == 1).UrlImage;
                    var uri = new Uri(@imag);
                    var bitmap = new BitmapImage(uri);
                    image.Source = bitmap;

                    var title = appContext.GuideCookings.FirstOrDefault(x => x.Id == 1).Title;
                    tbTitle.Text = title;

                    labelCookingTime.Content = appContext.GuideCookings.FirstOrDefault(x => x.Id == 1).CookingTime.ToString();

                    tbDescription.Text += appContext.GuideCookings.FirstOrDefault(x => x.Id == 1).Description;

                    List<GuideCooking> items = new List<GuideCooking>();
                    List<string> products = new List<string>();

                    var s = appContext.GuideCookings.FirstOrDefault(x => x.Id == 1).Products;
                    s.Trim();
                    string[] subs = s.Split('.');

                    foreach (string sub in subs)
                    {
                        if (sub != string.Empty)
                            products.Add(sub);
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        items.Add(new GuideCooking { Products = products[i] });
                    }

                    listBox.ItemsSource = items;
                    ObservableCollection<GuideCooking> ItemsForRoles = new ObservableCollection<GuideCooking>();
                    var query = appContext.GuideCookings.ToList();

                    foreach (var roles in query)
                    {
                        ItemsForRoles.Add(
                            new GuideCooking { Id = roles.Id, Title = roles.Title}
                            );
                    }
                    comboBox.ItemsSource = ItemsForRoles;
                }
                //Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int GetResId(string title)
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

        string LargestCommonSubstring(string left, string right)
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
        public void Initialize()
        {
           // GuideCooking guideCooking = new GuideCooking();

            //AddTitle(guideCooking, "Салат с тунцом, фетой и помидорами");
            //AddImage();
            //AddTimeCoocing(guideCooking, 10);
            //AddItem(guideCooking);
            //AddDescription(guideCooking, "Этот нарядный, яркий и аппетитный салат замечательно украсит праздничный стол. Салат с сыром \"Фетакса в рассоле\" очень вкусный, кубики сыра отлично держат форму. А готовится салат за считаные минуты.");
        }

        public void AddImage()
        {
            var uri = new Uri(@"https://img1.russianfood.com/dycontent/images_upl/617/sm_616120.jpg");
            var bitmap = new BitmapImage(uri);
            image.Source = bitmap;
        }
        //public void AddTimeCoocing(GuideCooking guideCooking, uint cookingTime)
        //{
        //    guideCooking.CookingTime = cookingTime;
        //    tbCookingTime.Text = GuideCooking.WriteCookingTime(guideCooking) + " хвилин";
        //}
        //public void AddTitle(GuideCooking guideCooking, string title)
        //{
        //    guideCooking.Title = title;
        //    tbTitle.Text = GuideCooking.WriteTitle(guideCooking);
        //}

        //public void AddDescription(GuideCooking guideCooking, string description)
        //{
        //    guideCooking.Description = description;
        //    tbDescription.Text = GuideCooking.WriteDescription(guideCooking);
        //}

        

        public async void AddItem(GuideCooking guideCooking)
        {
            //List<GuideCooking> item = new List<GuideCooking>();
            //item.Add(new GuideCooking { Id = "1", Title = "qw", UrlImage = @"https://img1.russianfood.com/dycontent/images_upl/617/sm_616120.jpg", Description = "dfdssadfhsfsdhfdsuf", CookingTime = 12, Type = "Салат"});
            //item.Add(new GuideCooking { Id = "2", Title = "qw", UrlImage = @"https://img1.russianfood.com/dycontent/images_upl/617/sm_616120.jpg", Description = "dfdssadfhsfsdhfdsuf", CookingTime = 12, Type = "Салат" }); 
            //item.Add(new GuideCooking { Id = "3", Title = "qw", UrlImage = @"https://img1.russianfood.com/dycontent/images_upl/617/sm_616120.jpg", Description = "dfdssadfhsfsdhfdsuf", CookingTime = 12, Type = "Салат" });
            //FileWriter(item);
            List<GuideCooking> items = new List<GuideCooking>();
           // await FileReader("file.txt", "");
            List<string> products = new List<string>();
            products.Add("Complete this WPF tutorial1");
            products.Add("Complete this WPF tutorial2");
            products.Add("Complete this WPF tutorial3");

            for (int i = 0; i < 3; i++)
            {
                guideCooking.Products = products[i];
                //items.Add(new GuideCooking { Id = "1", Products = guideCooking.Products });

                FileWriterProducts(items);
            }
            string path = "Products.txt";

            //listBox.ItemsSource = await FileReaderProducts(path, "1");
            //FileReader();
        }

        public async void FileWriterProducts(List<GuideCooking> items)
        {
            string path = "Products.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var item in items)
                {
                    await writer.WriteLineAsync($"{item.Id} {item.Products}");
                }
            }
        }
        public async void FileWriter(List<GuideCooking> items)
        {
            string path = "file.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var item in items)
                {
                    await writer.WriteLineAsync($"{item.Id} {item.Title} {item.UrlImage} {item.Description} {item.CookingTime} {item.Type}");
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != string.Empty)
            {
                Search(GetResId(tbSearch.Text));
            }
                //using (ApplicationContext appContext = new ApplicationContext())
                //{
                //    var id = GetResId(tbSearch.Text);
                //    appContext.GuideCookings.Load();
                //    var imag = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).UrlImage;
                //    var uri = new Uri(@imag);
                //    var bitmap = new BitmapImage(uri);
                //    image.Source = bitmap;

                //    var title = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).Title;
                //    tbTitle.Text = title;

                //    labelCookingTime.Content = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).CookingTime.ToString();


                //    tbDescription.Text = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).Description;

                //    List<GuideCooking> items = new List<GuideCooking>();
                //    List<string> products = new List<string>();


                //    var s = appContext.GuideCookings.FirstOrDefault(x => x.Id == id).Products;
                //    s.Trim();
                //    string[] subs = s.Split('.');

                //    foreach (string sub in subs)
                //    {
                //        if (sub != string.Empty)

                //            products.Add(sub);
                //    }

                //    for (int i = 0; i < 3; i++)
                //    {
                //        //guideCooking.Products = products[i];
                //        items.Add(new GuideCooking { Products = products[i] });

                //    }

                //    listBox.ItemsSource = items;
                //}
        }

        void Search(int id)
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

                for (int i = 0; i < 3; i++)
                {
                    //guideCooking.Products = products[i];
                    items.Add(new GuideCooking { Products = products[i] });

                }

                listBox.ItemsSource = items;
            }
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox.SelectedValue.ToString());
            Search((int)comboBox.SelectedValue);
        }
        //public async Task<List<GuideCooking>> FileReaderProducts(string path, string id)
        //{
        //    List<GuideCooking> items = new List<GuideCooking>();
        //    List<string> products = new List<string>();

        //    using (StreamReader reader = new StreamReader(path))
        //    {
        //        GuideCooking line = new GuideCooking();
        //        while ((line.Products = await reader.ReadLineAsync()) != null)
        //        {
        //            MessageBox.Show(line.Products);

        //            line.Id = line.Products.Substring(0, line.Products.IndexOf(" "));

        //            if (line.Id == id)
        //            {
        //                line.Products = line.Products.Remove(0, line.Products.IndexOf(" "));

        //                //MessageBox.Show(line.Products);
        //                items.Add(new GuideCooking { Id = id, Products = line.Products });
        //            }

        //        }
        //    }
        //    return items;
        //}

        //public async Task<List<GuideCooking>> FileReader(string path, string id)
        //{
        //    List<GuideCooking> items = new List<GuideCooking>();
        //    List<string> lines = new List<string>();

        //    lines = File.ReadAllLines(path).ToList();

        //    foreach (var line in lines)
        //    {
        //        string[] item = line.Split(',');
        //        GuideCooking guideCooking = new GuideCooking(item[0], item[1], item[2], item[3], uint.Parse(item[4]), item[5]);
        //        items.Add(guideCooking);

        //    }
        //    foreach (var item in items)
        //    {
        //        MessageBox.Show(item.Id + " " + item.UrlImage);
        //    }

        //    using (StreamReader reader = new StreamReader(path))
        //    {
        //        GuideCooking line = new GuideCooking();
        //        while ((line.Products = await reader.ReadLineAsync()) != null)
        //        {
        //            MessageBox.Show(line.Products);

        //            line.Id = line.Products.Substring(0, line.Products.IndexOf(" "));

        //            if (line.Id == id)
        //            {
        //                line.Products = line.Products.Remove(0, line.Products.IndexOf(" "));

        //                //MessageBox.Show(line.Products);
        //                items.Add(new GuideCooking { Id = id, Products = line.Products });
        //            }

        //        }
        //    }
        //    return items;
        //}
    }
}
