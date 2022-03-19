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
using GonnaCatchThemAll.Helpers;

namespace GonnaCatchThemAll
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        public Delegates.TransitionDelegate LoginDelegate = () => { };
        public Delegates.TransitionDelegate RegistorDelegate = () => { };
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            var username = Username_TextBox.Text;
            var password = Password_TextBox.Password;
            LoginDelegate();
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            RegistorDelegate();
        }
    }
}