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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Choise.xaml
    /// </summary>
    public partial class Choise : Window
    {
        public Choise()
        {
            InitializeComponent();
        }

        private void BtWeapon_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationPage authorizationPage = new AuthorizationPage();
            authorizationPage.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtPrisonres_Click(object sender, RoutedEventArgs e)
        {
            Prisoners prisoners = new Prisoners();
            prisoners.Show();
            Visibility = Visibility.Collapsed;
        }
    }
}
