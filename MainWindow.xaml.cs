﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using FileManager.Models;

namespace FileManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainViewModel ViewModel { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(ListBox, TextBox);
        }


        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var model = (BaseViewModel)ListBox.SelectedItem;
            ViewModel.Open(model);
        }
        
        
    }
}