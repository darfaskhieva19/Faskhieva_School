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
    /// Логика взаимодействия для ExitAdmin.xaml
    /// </summary>
    public partial class ExitAdmin : Window
    {
        public ExitAdmin()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOtm_Click(object sender, RoutedEventArgs e)
        {
            //ClassFrame.frameL.Navigate(new Pages.ListOfService(admin));
        }
    }
}
