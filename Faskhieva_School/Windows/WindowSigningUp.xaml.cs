using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Faskhieva_School
{
    /// <summary>
    /// Логика взаимодействия для WindowSigningUp.xaml
    /// </summary>
    public partial class WindowSigningUp : Window
    {
        Service service;

        public WindowSigningUp(Service service)
        {
            InitializeComponent();

            this.service = service;
            tbName.Text = service.Title;
            tbTime.Text = service.Time + " ";
            cbClient.ItemsSource = DataBase.Base.Client.ToList();
            tTime.Text = DateTime.Now.ToString("HH");
            tTime2.Text = DateTime.Now.ToString("mm");
            cbClient.SelectedValuePath = "ID";
            cbClient.DisplayMemberPath = "FIO";

            int HH = Convert.ToInt32(DateTime.Now.ToString("HH"));
            int mm = Convert.ToInt32(DateTime.Now.ToString("mm"));
            DateTime time = new DateTime(2000, 2, 2, HH, mm, 0);
            DateTime date = time.AddMinutes(Convert.ToInt32(service.DurationInSeconds / 60));
            tEndTime.Text = date.ToShortTimeString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbClient.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите клиента из списка");
                }
                else
                {
                    if (dpDate.SelectedDate == null)
                    {
                        MessageBox.Show("Выберите дату занятия");
                    }
                    else
                    {
                        if ((!Regex.IsMatch(tTime.Text, @"^[0-9]{2}$")) && (!Regex.IsMatch(tTime2.Text, @"^[0-9]{2}")))
                        {
                            MessageBox.Show("Введите время корректно");
                        }
                        else
                        {
                            ClientService clientService = new ClientService();                           
                            clientService.ServiceID = service.ID;
                            clientService.ClientID = (int)cbClient.SelectedIndex + 1;

                            DateTime dateT = dpDate.SelectedDate.Value;
                            dateT = dateT.Add(new TimeSpan(Convert.ToInt32(tTime.Text), Convert.ToInt32(tTime2.Text), 0));
                            clientService.StartTime = dateT;

                            DataBase.Base.ClientService.Add(clientService);
                            DataBase.Base.SaveChanges();
                            MessageBox.Show("Клиент успешно записался на занятие!");
                            Close();
                        }
                    }                                    
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так..");
            }
        }

        private void tTime_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tTime.Text != "" && tTime2.Text != "" && tTime.Text.Length == 2 && tTime2.Text.Length == 2)
                {
                    int hour = Convert.ToInt32(tTime.Text);
                    int min = Convert.ToInt32(tTime2.Text);

                    if (hour < 24 || min < 60)
                    {
                        int HH = Convert.ToInt32(hour);
                        int MM = Convert.ToInt32(min);
                        DateTime time = new DateTime(2000, 2, 2, HH, MM, 0);
                        DateTime data = time.AddMinutes(Convert.ToInt32(service.DurationInSeconds / 60));
                        tEndTime.Text = data.ToShortTimeString();
                    }
                    else
                    {
                        MessageBox.Show("Введите время корректно!", "Ошибка", MessageBoxButton.OK);
                    }                    
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так..");
            }
        }

        private void tTime_PreviewTextInput(object sender, TextCompositionEventArgs e) //запрет на ввод символов
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
