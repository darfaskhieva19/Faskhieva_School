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
using static System.Net.Mime.MediaTypeNames;

namespace Faskhieva_School.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddUpdate.xaml
    /// </summary>
    public partial class PageAddUpdate : Page
    {
        public object OpenFileDialoge { get; private set; }

        Service service;
        bool flag;

        ServicePhoto photo;        
        bool servicePh;

        string path;

        public PageAddUpdate() //для добавления
        {
            InitializeComponent();
            ImageSer.Source = new BitmapImage(new Uri("..\\resource\\picture.png", UriKind.Relative));
        }

        public PageAddUpdate(Service service) //конструктор для редактирования
        {
            this.service = service;
            InitializeComponent();
            flag = true;
            tbID.Visibility = Visibility.Visible;

            tbID.Text = "Индентификатор:" + Convert.ToString(service.ID);
            tbName.Text = service.Title;
            double cost = Convert.ToInt32(service.Cost);
            tbPrice.Text = Convert.ToString(cost);
            tbTime.Text = Convert.ToString(service.DurationInSeconds / 60);
            tbDiscount.Text = Convert.ToString(service.Discount * 100);
            tbDes.Text = service.Description;
            txtH.Text = "Изменение услуги";
            btSave.Content = "Изменить";

            if (service.MainImagePath != null)
            {
                path = service.MainImagePath;
                //ImageSer.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + service.MainImagePath, UriKind.Absolute));
                btnUpPhoto.Content = "Изменить фото";
            }
            else
            {
                path = null;
                ImageSer.Source = new BitmapImage(new Uri("..\\resource\\picture.png", UriKind.Relative));
            }
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                if(tbName.Text == "" || tbPrice.Text == "" || tbTime.Text == "")
                {
                    MessageBox.Show("Не все обязательные поля заполнены!");
                }
                else
                {
                    service = new Service();
                    service.Title = tbName.Text;
                    service.Cost = Convert.ToInt32(tbPrice.Text);
                    double skidka = Convert.ToDouble(tbDiscount.Text) / 100;
                    service.Discount = skidka;
                    service.DurationInSeconds = Convert.ToInt32(tbTime.Text);
                    if (tbDes.Text == "")
                    {
                        service.Description = null;
                    }
                    else
                    {
                        service.Description = tbDes.Text;
                    }
                    service.MainImagePath = path;
                    DataBase.Base.Service.Add(service);
                    MessageBox.Show("Успешное добавление услуги!");
                    DataBase.Base.SaveChanges();
                    ClassFrame.frameL.Navigate(new Pages.ListOfService());
                }
            }
            else
            {
                if (tbName.Text == "" || tbPrice.Text == "" || tbTime.Text == "")
                {
                    MessageBox.Show("Обязательные поля не заполнены!");
                }
                else
                {
                    service.Title = tbName.Text;
                    service.Cost = Convert.ToInt32(tbPrice.Text);
                    double skidka = Convert.ToDouble(tbDiscount.Text) / 100;
                    service.Discount = skidka;
                    service.DurationInSeconds = Convert.ToInt32(tbTime.Text);
                    if (tbDes.Text == "")
                    {
                        service.Description = null;
                    }
                    else
                    {
                        service.Description = tbDes.Text;
                    }
                    service.MainImagePath = path;
                    DataBase.Base.Service.Add(service);
                    MessageBox.Show("Успешное изменение услуги!");
                    DataBase.Base.SaveChanges();
                    ClassFrame.frameL.Navigate(new Pages.ListOfService());
                }
            }
        }        
        
        private void btnUpPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.InitialDirectory = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - 9) + "Images\\";
                OFD.ShowDialog();
                path = OFD.FileName;
                string[] arrayPath = path.Split('\\');
                path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];
                ImageSer.Source = new BitmapImage(new Uri(path, UriKind.Relative));
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
    }
}
