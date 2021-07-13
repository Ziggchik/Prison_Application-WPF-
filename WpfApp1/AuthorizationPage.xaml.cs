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
using Microsoft.Win32;
using System.Net;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Window
    {
        public AuthorizationPage()
        {
            SystemChek();
            switch (startup)
            {
                case true:
                    InitializeComponent();
                    tbEnterLogin.Clear();
                    ChangeSize((int)Width, (int)Height);
                    break;
                case false:
                    Close();
                    break;
            }
        }

        private void AuthorizForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Покинуть приложение?", "Закрытие приложения", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                case MessageBoxResult.Yes:
                    e.Cancel = false;
                    break;
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void ChangeSize(int x, int y)
        {
            if ((x >= 0 && y >= 0) && (x < 800 && y < 600))
            {
                lblEnterLogin.FontSize = 12;
                tbEnterLogin.FontSize = 12;
                btEnter.FontSize = 12;
                btLeave.FontSize = 12;
            }
            else
            {
                if ((x >= 800 && y >= 600) && (x <= 1200 && y <= 1024))
                {
                    lblEnterLogin.FontSize = 24;
                    tbEnterLogin.FontSize = 24;
                    btEnter.FontSize = 24;
                    btLeave.FontSize = 24;
                }
                else
                {
                    if ((x >= 1280 && y >= 1024) && (x <= 1680 && y <= 1050))
                    {
                        lblEnterLogin.FontSize = 36;
                        tbEnterLogin.FontSize = 36;
                        btEnter.FontSize = 36;
                        btLeave.FontSize = 36;
                    }
                    else
                    {
                        if (x >= 1680 && y >= 1050)
                        {
                            lblEnterLogin.FontSize = 48;
                            tbEnterLogin.FontSize = 48;
                            btEnter.FontSize = 48;
                            btLeave.FontSize = 48;
                        }
                    }
                }
            }
        }

        private void BtLeave_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        bool startup = true;
        private void SystemChek()
        {
            int Major = Environment.OSVersion.Version.Major;
            int Minor = Environment.OSVersion.Version.Minor;
            if ((Major >= 6) && (Minor >= 0))
            {
                RegistryKey registrySQL =
                Registry.LocalMachine.
                OpenSubKey(@"SOFTWARE\MICROSOFT\Microsoft SQL Server");
                if (registrySQL == null)
                {
                    MessageBox.Show("Запуск системы не возможен, " +
                        "в системе отсутвует Microsoft SQL Server",
                        "Не поняль");
                    startup = false;
                }
                else
                {
                    try
                    {
                        DBConnection.connection.Open();
                    }
                    catch
                    {
                        MessageBox.Show("Не возможно подключиться к " +
                            "источнику данных", "Не поняль");
                        startup = false;
                    }
                    finally
                    {
                        DBConnection.connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Данная операционная система не предназначена для запуска приложения", "Не поняль");
                startup = false;
            }

            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://google.ru");
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                MessageBox.Show("Нет соединения с интернетом", "Не поняль");
            }
        }
        private void AuthorizForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize((int)e.NewSize.Width, (int)e.NewSize.Height);
        }

        private void BtEnter_Click(object sender, RoutedEventArgs e)
        {
            switch (tbEnterLogin.Password == "Porasha")
            {
                case (false):
                    tbEnterLogin.Clear();
                    MessageBox.Show("Не верно введён логин или пароль! " +
                        "\n Повторите попытку ввода!", "Не поняль",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                default:
                    Choise choise = new Choise();
                    choise.Show();
                    Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
