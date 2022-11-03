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
            try
            {
                InitializeComponent();
                InitializeData();
                AddItemToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InitializeData()
        {
            using (ApplicationContext appContext = new ApplicationContext())
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

                for (int i = 0; i < products.Count; i++)
                {
                    items.Add(new GuideCooking { Products = products[i] });
                }

                listBox.ItemsSource = items;
            }
        }
        void AddItemToComboBox()
        {
            using (ApplicationContext appContext = new ApplicationContext())
            {
                ObservableCollection<GuideCooking> ItemsForRoles = new ObservableCollection<GuideCooking>();
                var query = appContext.GuideCookings.ToList();

                foreach (var roles in query)
                {
                    ItemsForRoles.Add(
                        new GuideCooking { Id = roles.Id, Title = roles.Title }
                        );
                }
                comboBox.ItemsSource = ItemsForRoles;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != string.Empty && tbMinCookingTime.Text == string.Empty && tbMaxCookingTime.Text == string.Empty)
            {
                GuideCookingDataGateway.Search(image, (int)comboBox.SelectedValue, tbTitle, labelCookingTime, tbDescription, listBox);
            }
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (tbMinCookingTime.Text == string.Empty && tbMaxCookingTime.Text == string.Empty)
                    GuideCookingDataGateway.Search(image, (int)comboBox.SelectedValue, tbTitle, labelCookingTime, tbDescription, listBox);
                else
                    GuideCookingDataGateway.Search(tbMinCookingTime, tbMaxCookingTime, image, (int)comboBox.SelectedValue, tbTitle, labelCookingTime, tbDescription, listBox);
            }
            catch
            {
                MessageBox.Show("Страви по заданим параметрам не знайдено!");
            }
        }
     }
}
