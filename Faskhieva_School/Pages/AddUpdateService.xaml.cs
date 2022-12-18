using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для AddUpdateService.xaml
    /// </summary>
    public partial class AddUpdateService : Page
    {
        Service service;
        bool flag = false;

        public AddUpdateService(Service service) // конструктор для создания новой группы (без аргументов)   
        {           
            InitializeComponent();
            this.service = service;
            flag = true;

            tbID.Text = "Идентификатор: " + service.ID;
            tbName.Text = service.Title;
            tbPrice.Text = Convert.ToString(service.Cost);
            tbTime.Text = Convert.ToString(service.DurationInSeconds/60);
            tbDiscount.Text = Convert.ToString(service.Discount);

            btnUpPhoto.Content = "Изменить фото";
            txtH.Text = "Изменение услуги";
            btnSave.Content = "Изменить";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new PageListOfService());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDopPhoto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
