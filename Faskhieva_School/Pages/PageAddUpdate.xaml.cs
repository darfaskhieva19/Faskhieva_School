using Microsoft.Win32;
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

namespace Faskhieva_School.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddUpdate.xaml
    /// </summary>
    public partial class PageAddUpdate : Page
    {
        public object OpenFileDialoge { get; private set; }

        Service service;
        bool flag = false;

        string path;

        public PageAddUpdate() //для добавления
        {
            InitializeComponent();
            ImageSer.Source = new BitmapImage(new Uri("..\\resourse\\picture.png", UriKind.Relative));
        }

        public PageAddUpdate(Service service) //конструктор для редактирования
        {
            this.service = service;
            InitializeComponent();
            flag = true;
            tbID.Visibility = Visibility.Visible;

            tbID.Text = "Индентификатор:" + Convert.ToString(service.ID);
            tbName.Text = service.Title;
            tbPrice.Text = Convert.ToString(service.Cost);
            tbTime.Text = Convert.ToString(service.DurationInSeconds / 60);
            tbDiscount.Text = Convert.ToString(service.Discount);

            btnUpPhoto.Content = "Изменить фото";
            txtH.Text = "Изменение услуги";
            btnSave.Content = "Изменить";

        }

        private void btnUpPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.ShowDialog();
                path = OFD.FileName;
                string[] arrayPath = path.Split('\\');
                path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];
                BitmapImage bitmapImage = new BitmapImage(new Uri(path, UriKind.Relative));
                ImageSer.Source = bitmapImage;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так...");
            }
        }

        private void btnDopPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameL.Navigate(new ListOfService());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbName.Text != "")
                {
                    if (tbPrice.Text != "")
                    {
                        if (tbTime.Text != "")
                        {
                            if (Times(tbTime.Text))
                            {
                                if (flag == false)
                                {
                                    service = new Service();
                                }
                                service.Title = tbName.Text;
                                service.Cost = Convert.ToDecimal(tbPrice.Text);
                                service.DurationInSeconds = Convert.ToInt32(tbTime.Text);
                                if (tbDes.Text == "")
                                {
                                    service.Description = null;
                                }
                                else
                                {
                                    service.Description = tbDes.Text;
                                }
                                if (tbDiscount.Text == "")
                                {
                                    service.Discount = null;
                                }
                                else
                                {
                                    service.Discount = Convert.ToDouble(tbDiscount.Text) / 100;
                                }                               
                                DataBase.Base.Service.Add(service);
                                DataBase.Base.SaveChanges();
                                if (flag)
                                {
                                    MessageBox.Show("Успешное изменение услуги!");
                                }
                                else
                                {
                                    MessageBox.Show("Успешное добавление услуги!");
                                }
                                ClassFrame.frameL.Navigate(new Pages.ListOfService());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Продолжительность оказания услуги должно быть заполнено!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Стоимость услуги должна быть заполнена!");
                    }
                }
                else
                {
                    MessageBox.Show("Наименование услуги должно быть заполнено!");
                }
            }
            catch
            {
                if (flag == true)
                {
                    MessageBox.Show("При изменение возникла ошибка!");
                }
                else
                {
                    MessageBox.Show("При добавление возникла ошибка!");
                }
            }
        }
        public bool Times(string t)
        {
            int time = Convert.ToInt32(t);
            if ((time > 0) && (time <= 240))
            {
                return true;
            }
            else
            {
                return false;
                MessageBox.Show("Продолжительность занятия не может быть отрицательным и превышать 4 часов!");
            }
        }
    }
}
