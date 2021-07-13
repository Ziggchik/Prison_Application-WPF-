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
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string QR = "";
        private void ProductWindow_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrWeapon;
            dgFill(QR);
            lbFill();
            lbFill1();
            lbFill2();
        }
        private void dgFill(string qr)
        {
            DBConnection connection = new DBConnection();
            DBConnection.qrWeapon = qr;
            connection.WeaponFill();
            dgWeapon.ItemsSource = connection.dtWeapon.DefaultView;
            dgWeapon.Columns[0].Visibility = Visibility.Collapsed;
            dgWeapon.Columns[2].Visibility = Visibility.Collapsed;
            dgWeapon.Columns[3].Visibility = Visibility.Collapsed;
            dgWeapon.Columns[5].Visibility = Visibility.Collapsed;
            dgWeapon.Columns[6].Visibility = Visibility.Collapsed;
            dgWeapon.Columns[8].Visibility = Visibility.Collapsed;
            dgWeapon.Columns[9].Visibility = Visibility.Collapsed;
        }
        private void lbFill()
        {
            DBConnection connection = new DBConnection();
            connection.Type_of_Weapon_Fill();
            lbType_of_Weapon.ItemsSource
                = connection.dtType_of_Weapon.DefaultView;
            lbType_of_Weapon.SelectedValuePath = "ID_Type_of_Weapon";
            lbType_of_Weapon.DisplayMemberPath = "Name_of_Type_of_Weapon";
        }
        private void lbFill1()
        {
            DBConnection connection = new DBConnection();
            connection.Nakladnaya_Fill();
            lbNakladnaya.ItemsSource
                = connection.dtNakladnaya.DefaultView;
            lbNakladnaya.SelectedValuePath = "ID_Nakladnaya";
            lbNakladnaya.DisplayMemberPath = "Nomer_Nakladnaya";
        }

        private void lbFill2()
        {
            DBConnection connection = new DBConnection();
            connection.GuardianFill();
            lbGuardians.ItemsSource
                = connection.dtGuardian.DefaultView;
            lbGuardians.SelectedValuePath = "ID_Guardian";
            lbGuardians.DisplayMemberPath = "Информация об охраннике";
        }


        private void LbType_of_Weapon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (chbFilter.IsChecked)
            {
                case (true):
                    string newQR = QR +
                        " where [Type_of_Weapon_ID] = "
                        + lbType_of_Weapon.SelectedValue.ToString();
                    dgFill(newQR);
                    break;
                case (false):
                    dgFill(QR);
                    break;
            }
        }

        DBProcedures procedures = new DBProcedures();
        private void BtInsertType_Click(object sender, RoutedEventArgs e)
        {
            procedures.spType_of_Weapon_insert(tbName_of_Type.Text);
            lbFill();
            lbFill1();
            lbFill2();
        }

        private void BtUpdateType_Click(object sender, RoutedEventArgs e)
        {
            procedures.spType_of_Weapon_update(Convert.ToInt32(
                lbType_of_Weapon.SelectedValue.ToString()),
                tbName_of_Type.Text);
            lbFill();
            lbFill1();
            lbFill2();
        }

        private void BtDeleteType_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить выбранную запись?",
                "Удаление записи", MessageBoxButton.YesNo,
                MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    procedures.spType_of_Weapon_delete(
                        Convert.ToInt32(lbType_of_Weapon.
                        SelectedValue.ToString()));
                    lbFill();
                    lbFill1();
                    lbFill2();
                    break;
            }
        }

        private void LbNakladnaya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (chbFilter.IsChecked)
            {
                case (true):
                    string newQR = QR +
                        " where [Nakladnaya_ID] = "
                        + lbNakladnaya.SelectedValue.ToString();
                    dgFill(newQR);
                    break;
                case (false):
                    dgFill(QR);
                    break;
            }
        }

        private void BtInsertNakladnaya_Click(object sender, RoutedEventArgs e)
        {
            procedures.spNakladnaya_insert(tbNomer_Nakladnaya.Text);
            lbFill();
            lbFill1();
            lbFill2();
        }
        private void BtUpdateNakladnaya_Click(object sender, RoutedEventArgs e)
        {
            procedures.spNakladnaya_update(Convert.ToInt32(
                lbNakladnaya.SelectedValue.ToString()),
                tbNomer_Nakladnaya.Text);
            lbFill();
            lbFill1();
            lbFill2();
        }

        private void BtDeleteNakladnaya_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить выбранную запись?",
               "Удаление записи", MessageBoxButton.YesNo,
               MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    procedures.spNakladnaya_delete(
                        Convert.ToInt32(lbNakladnaya.
                        SelectedValue.ToString()));
                    lbFill();
                    lbFill1();
                    lbFill2();
                    break;
            }
        }

        private void LbGuardians_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (chbFilter.IsChecked)
            {
                case (true):
                    string newQR = QR +
                        " where [Guardians_ID] = "
                        + lbGuardians.SelectedValue.ToString();
                    dgFill(newQR);
                    break;
                case (false):
                    dgFill(QR);
                    break;
            }
        }

        private void BtInsertGuardian_Click(object sender, RoutedEventArgs e)
        {
            procedures.spGuardians_insert(tbSurname_Guard.Text,Name_Guard.Text,MiddleName_Guard.Text);
            lbFill();
            lbFill1();
            lbFill2();
        }

        private void BtUpdateGuardian_Click(object sender, RoutedEventArgs e)
        {
            procedures.spGuardians_update(Convert.ToString(lbGuardians.SelectedValue), tbSurname_Guard.Text, Name_Guard.Text, Convert.ToInt32( MiddleName_Guard.Text));
             lbFill();
             lbFill1();
             lbFill2();
        }

        private void BtDeleteGuardian_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить выбранную запись?",
               "Удаление записи", MessageBoxButton.YesNo,
               MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    procedures.spGuardians_delete(
                        Convert.ToInt32(lbGuardians.
                        SelectedValue.ToString()));
                    lbFill();
                    lbFill1();
                    lbFill2();
                    break;
            }
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgWeapon.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[7].ToString() == tbSearch.Text)
                {
                    dgWeapon.SelectedItem = dataRow;
                }
            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            Choise choise = new Choise();
            choise.Show();
            Visibility = Visibility.Collapsed;
        }

        private void DgWeapon_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_Weapon"):
                    e.Column.Header = "Название оружия";
                    break;
                case ("Name_of_Type_of_Weapon"):
                    e.Column.Header = "Тип оружия";
                    break;
            }
        }

        private void BtInsertWeapon_Click(object sender, RoutedEventArgs e)
        {
            procedures.spWeapon_insert(tbName_Weapon.Text, Convert.ToInt32(lbType_of_Weapon.SelectedValue),Convert.ToInt32(lbGuardians.SelectedValue), Convert.ToInt32 (lbNakladnaya.SelectedValue)); 
            dgFill(QR);
        }

        private void BtUpdateWeapon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView ID = (DataRowView)dgWeapon.SelectedItems[0];
                if (ID == null)
                    MessageBox.Show("", "");
                else
                    procedures.spWeapon_update(Convert.ToInt32(
                        ID["ID_Weapon"]), tbName_Weapon.Text, Convert.ToInt32(lbType_of_Weapon.SelectedValue), Convert.ToInt32(lbGuardians.SelectedValue), Convert.ToInt32(lbNakladnaya.SelectedValue));
                dgFill(QR);
            }
            catch
            {
                
            }
            
        }

        private void BtDeleteWeapon_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить выбранную запись?",
                "Удаление записи", MessageBoxButton.YesNo,
                MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    DataRowView ID =
                        (DataRowView)dgWeapon.SelectedItems[0];
                    procedures.spWeapon_delete(
                        Convert.ToInt32(ID["ID_Weapon"]));
                    dgFill(QR);
                    break;
            }
        }

        private void BtFilter_Click(object sender, RoutedEventArgs e)
        {
            switch (chbFilter.IsChecked)
            {
                case (true):
                    string newQR = QR + " where [Name_Weapon] like '%" + tbSearch.Text + "%'or" +
                "[Nomer_Nakladnaya] like '%" + tbSearch.Text + "%'or " +
                "[Name_of_Type_of_Weapon] like '%" + tbSearch.Text + "%'or " +
                "[Surname_Guardian]+' '+[Name_Guardian]+' '+[MiddleName_Guardian] like '%" + tbSearch.Text + "%'";
                    dgFill(newQR);
                    break;
                case (false):
                    dgFill(QR);
                    break;
            }
        }
    }
}
