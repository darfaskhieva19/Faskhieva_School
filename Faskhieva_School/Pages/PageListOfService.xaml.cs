﻿using System;
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
        public PageListOfService()
        {
            InitializeComponent();
            LService.ItemsSource = DataBase.Base.Service.ToList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
