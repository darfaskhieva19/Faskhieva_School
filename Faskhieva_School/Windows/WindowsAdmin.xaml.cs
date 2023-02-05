using Faskhieva_School.Pages;
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
using System.Windows.Shapes;

namespace Faskhieva_School
{
    /// <summary>
    /// Логика взаимодействия для WindowsAdmin.xaml
    /// </summary>
    public partial class WindowsAdmin : Window
    {
        public WindowsAdmin()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (tbPass.Text != "")
            {
                if (tbPass.Text == "0000")
                {
                    string code = "0000";
                    ListOfService.code = code;
                    MessageBox.Show("Активирован режим администратора!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный код!\nПовторите попытку!");
                    tbPass.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Введите код для активации");
            }
           
        }

        private void btnOtm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
