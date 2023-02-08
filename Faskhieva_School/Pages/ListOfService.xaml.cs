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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Faskhieva_School.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfService.xaml
    /// </summary>

    public partial class ListOfService : Page
    {
        public static string code;

        List<Service> listFilter;
        public ListOfService()
        {
            InitializeComponent();
            LService.ItemsSource = DataBase.Base.Service.ToList();
            cbPrice.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0;

            Filter();
        }

        void Filter() // метод для одновременной фильтрации, поиска и сортировки
        {
            listFilter = DataBase.Base.Service.ToList();
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                listFilter = listFilter.Where(x => x.Title.ToLower().Contains(tbSearch.Text.ToLower())).ToList(); //поиск по наименованию
            }
            if (!string.IsNullOrWhiteSpace(tbSearchDes.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                List<Service> des = listFilter.Where(x => x.Description != null).ToList();
                if (des.Count > 0)
                {
                    listFilter = des.Where(x => x.Description.ToLower().Contains(tbSearchDes.Text.ToLower())).ToList();//поиск по описанию
                }
                else
                {
                    MessageBox.Show("Нет описания");
                }                
            }

            //сортировка
            switch (cbPrice.SelectedIndex)
            {
                case 1:
                    listFilter.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    break;
                case 2:
                    listFilter.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    listFilter.Reverse();
                    break;
            }

            //фильтр
            switch (cbFilter.SelectedIndex)
            {
                case 1:
                    listFilter = listFilter.Where(z => z.Discount >= 0 && z.Discount < 0.05).ToList();
                    break;
                case 2:
                    listFilter = listFilter.Where(z => z.Discount >= 0.05 && z.Discount < 0.15).ToList();
                    break;
                case 3:
                    listFilter = listFilter.Where(z => z.Discount >= 0.15 && z.Discount < 0.30).ToList();
                    break;
                case 4:
                    listFilter = listFilter.Where(z => z.Discount >= 0.30 && z.Discount < 0.70).ToList();
                    break;
                case 5:
                    listFilter = listFilter.Where(z => z.Discount >= 0.70 && z.Discount < 1).ToList();
                    break;
            }
            tbCountZap.Text = listFilter.Count.ToString() + " из " + DataBase.Base.Service.ToList().Count.ToString(); //количество записей

            LService.ItemsSource = listFilter;
            if (listFilter.Count == 0)
            {
                MessageBox.Show("нет записей");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e) //переход на страницу редактирования
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = DataBase.Base.Service.FirstOrDefault(z => z.ID == index);
            ClassFrame.frameL.Navigate(new PageAddUpdate(service));
        }              

        private void btnDelete_Click(object sender, RoutedEventArgs e) //удаление записи
        {
            try
            {
                Button btn = (Button)sender;
                int index = Convert.ToInt32(btn.Uid);
                if (MessageBox.Show("Вы действительно хотите удалить данную услугу?", "Системное сообщение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (DataBase.Base.ClientService.FirstOrDefault(x => x.ServiceID == index) == null)
                    {
                        foreach (ServicePhoto servicePhoto in DataBase.Base.ServicePhoto.ToList())
                        {
                            if (servicePhoto.ServiceID == index)
                            {
                                DataBase.Base.ServicePhoto.Remove(servicePhoto);
                            }
                        }
                        Service service = DataBase.Base.Service.FirstOrDefault(x => x.ID == index);
                        DataBase.Base.Service.Remove(service);
                        DataBase.Base.SaveChanges();
                        MessageBox.Show("Успешное удаление!");
                        ClassFrame.frameL.Navigate(new Pages.ListOfService());
                    }
                }
                else
                {
                    MessageBox.Show("Невозможно удалить эту услугу!");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так..");
            }
        }

        private void cbPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearchDes_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void tbSkidka_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            if (tb.Uid != null)
            {
                tb.Visibility = Visibility.Visible;
            }
            else
            {
                tb.Visibility = Visibility.Collapsed;
            }
        }

        private void tbOldPrice_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            if (tb.Uid != null)
            {
                tb.Visibility = Visibility.Visible;
            }
            else
            {
                tb.Visibility = Visibility.Collapsed;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e) //переход на добавление услуги
        {
            
            ClassFrame.frameL.Navigate(new PageAddUpdate());
        }

        private void btnUpdate_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnUp = sender as Button;
            if (code == "0000")
            {
                btnUp.Visibility = Visibility.Visible;
            }
            else
            {
                btnUp.Visibility = Visibility.Collapsed;
            }
        }

        private void btnDelete_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (code == "0000")
            {
                btn.Visibility = Visibility.Visible;
            }
            else
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        private void btnZapis_Click(object sender, RoutedEventArgs e) //переход на окно запись на услугу
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = DataBase.Base.Service.FirstOrDefault(z => z.ID == index);

            WindowSigningUp windowSigning = new WindowSigningUp(service);
            windowSigning.ShowDialog();
            ClassFrame.frameL.Navigate(new ListOfService());
        }

        private void btnZapis_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (code == "0000")
            {
                btn.Visibility = Visibility.Visible;
            }
            else
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        private void btnZap_Click(object sender, RoutedEventArgs e) //переход на ближайшие записи
        {
            ClassFrame.frameL.Navigate(new PageUpcomingEntries());
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            WindowsAdmin windowsAdmin = new WindowsAdmin();
            windowsAdmin.ShowDialog();
            ClassFrame.frameL.Navigate(new Pages.ListOfService());
        }

        private void btnAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnAdmin = sender as Button;
            if (code == "0000")
            {
                btnAdmin.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnAdmin.Visibility = Visibility.Visible;
            }
        }

        private void btnExitAdmin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult k = MessageBox.Show("Вы действительно хотите выйти из режима администратора?", "Системное сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (k == MessageBoxResult.Yes)
            {
                MessageBox.Show("Режим администратора выключен");
                ClassFrame.frameL.Navigate(new Pages.ListOfService());
                btnAdmin.Visibility = Visibility.Visible;
                btnExitAdmin.Visibility = Visibility.Collapsed;
                Add.Visibility = Visibility.Collapsed;
                btnZap.Visibility = Visibility.Collapsed;
                code = "00";
            }            
        }

        private void btnExitAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (code == "0000")
            {
                button.Visibility = Visibility.Visible;
            }
            else
            {

                button.Visibility = Visibility.Collapsed;
            }
        }
    }
}
