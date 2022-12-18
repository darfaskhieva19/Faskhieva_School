using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

namespace Faskhieva_School
{
    /// <summary>
    /// Логика взаимодействия для PageListOfService.xaml
    /// </summary>
    public partial class PageListOfService : Page
    {
        List<Service> listFilter;
        public PageListOfService()
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
                listFilter = listFilter.Where(x => x.Description.ToLower().Contains(tbSearchDes.Text.ToLower())).ToList(); //поиск по описанию
            }

            //сортировка
            switch (cbPrice.SelectedIndex)
            {
                case 0:
                        listFilter.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    break;
                case 1:
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
            ClassFrame.frameL.Navigate(new AddUpdateService(service));
        }

        private bool GetProverkaService(int index)
        {
            ClientService CSerList = DataBase.Base.ClientService.FirstOrDefault(x => x.ServiceID == index);
            if(CSerList!= null)
            {
                MessageBox.Show("Не возможно удалить запись!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) //удаление записи
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = DataBase.Base.Service.FirstOrDefault(x => x.ID == index);
            if(GetProverkaService(index))
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Удаление отеля", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    DataBase.Base.Service.Remove(service);
                    MessageBox.Show("Успешное удаление!");
                    DataBase.Base.SaveChanges();
                    ClassFrame.frameL.Navigate(new PageListOfService());
                }
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

        private void btnUpdate_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void tbSkidka_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            if(tb.Uid != null)
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
    }
}
