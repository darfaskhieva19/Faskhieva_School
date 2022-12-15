using System;
using System.Collections.Generic;
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

namespace Faskhieva_School
{
    /// <summary>
    /// Логика взаимодействия для PageListOfService.xaml
    /// </summary>
    public partial class PageListOfService : Page
    {
        List<Service> listFilter = new List<Service>();
        public PageListOfService()
        {
            InitializeComponent();
            LService.ItemsSource = DataBase.Base.Service.ToList();
            cbPrice.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0; 

        }
        public void Filter()
        {
            switch (cbPrice.SelectedIndex)
            {
                case 1:
                    //listFilter.Sort((x, y) => x.NameGroup.CompareTo(y.));
                    break;
                case 2:
                    //listFilter.Sort((x, y) => x.NameGroup.CompareTo(y.NameGroup));
                    listFilter.Reverse();
                    break;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
