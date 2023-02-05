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
            tbTime.Text = service.Time + "минут";
            cbClient.ItemsSource = DataBase.Base.Client.ToList();
            cbClient.SelectedValuePath = "ID";
            cbClient.DisplayMemberPath = "FIO";
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
                            clientService.ClientID = (int)cbClient.SelectedIndex+1;
                            clientService.ServiceID = service.ID;

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

        private void tTime2_PreviewTextInput(object sender, TextCompositionEventArgs e) //запрет на ввод символов
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void tTime_TextChanged(object sender, RoutedEventArgs e)
        {
            ShowTime();
        }

        private void tTime2_TextChanged(object sender, RoutedEventArgs e)
        {
            ShowTime();
        }

        private void tTime_PreviewTextInput(object sender, TextCompositionEventArgs e) //запрет на ввод символов
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        public void ShowTime()
        {
            try
            {
                string hour = tTime.Text;
                string minute = tTime2.Text;
                int hours = Convert.ToInt32(hour);
                int min = Convert.ToInt32(minute);
                if (hours < 24 && min < 60)
                {
                    MessageBox.Show("Введите часы корректно!", "Запись на услугу");
                }
                else
                {
                    if (min > 0 || min < 60)
                    {
                        MessageBox.Show("Введите минуты корректно!", "Запись на услугу");
                    }
                    else
                    {

                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так..");
            }
        }
    }
}
