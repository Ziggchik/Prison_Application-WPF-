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
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Prisoners.xaml
    /// </summary>
    public partial class Prisoners : Window
    {
        public Prisoners()
        {
            InitializeComponent();
        }
        private string QR = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrPrisoner;
            dgFill(QR);
            lbFill();
            lbFill2();
        }
        private void dgFill(string qr)
        {
            DBConnection connection = new DBConnection();
            DBConnection.qrPrisoner = qr;
            connection.PrisonerFill();
            dgPrisoners.ItemsSource = connection.dtPrisoner.DefaultView;
            dgPrisoners.Columns[0].Visibility = Visibility.Collapsed;
            dgPrisoners.Columns[4].Visibility = Visibility.Collapsed;
            dgPrisoners.Columns[5].Visibility = Visibility.Collapsed;
            dgPrisoners.Columns[5].Visibility = Visibility.Collapsed;
            dgPrisoners.Columns[7].Visibility = Visibility.Collapsed;
            dgPrisoners.Columns[8].Visibility = Visibility.Collapsed;
        }
        private void lbFill()
        {
            DBConnection connection = new DBConnection();
            connection.BlockFill();
            lbBlock.ItemsSource
             = connection.dtBlock.DefaultView;
            lbBlock.SelectedValuePath = "ID_Prison_Block";
            lbBlock.DisplayMemberPath = "Name_of_block";
        }
        private void lbFill2()
        {
            DBConnection connection = new DBConnection();
            connection.GuardianFill();
            lbGurds.ItemsSource
                = connection.dtGuardian.DefaultView;
            lbGurds.SelectedValuePath = "ID_Guardian";
            lbGurds.DisplayMemberPath = "Информация об охраннике";
        }

        private void LbBlock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (chbFilter.IsChecked)
            {
                case (true):
                    string newQR = QR +
                        " where [Prison_Block_ID] = "
                        + lbBlock.SelectedValue.ToString();
                    dgFill(newQR);
                    break;
                case (false):
                    dgFill(QR);
                    break;
            }
        }

        DBProcedures procedures = new DBProcedures();
        private void BtInsertBlock_Click(object sender, RoutedEventArgs e)
        {
            procedures.spPrison_Block_insert(tbName_of_block.Text);
            lbFill();
            lbFill2();
        }

        private void BtUpdateBlock_Click(object sender, RoutedEventArgs e)
        {
            procedures.spPrison_Block_update(Convert.ToInt32(
               lbBlock.SelectedValue.ToString()),
               tbName_of_block.Text);
            lbFill();
            lbFill2();
        }

        private void BtDeleteBlock_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить выбранную запись?",
                "Удаление записи", MessageBoxButton.YesNo,
                MessageBoxImage.Warning))
            {
                case MessageBoxResult.Yes:
                    procedures.spPrison_Block_delete(
                        Convert.ToInt32(lbBlock.
                        SelectedValue.ToString()));
                    lbFill();
                    lbFill2();
                    break;
            }
        }

        private void BtInsertGuard_Click(object sender, RoutedEventArgs e)
        {
            procedures.spGuardians_insert(tbSurname_Guardian.Text, tbName_Guardian.Text, tbMiddleName_Guardian.Text);
            lbFill();
            lbFill2();
        }

        private void BtUpdateGuard_Click(object sender, RoutedEventArgs e)
        {
            procedures.spGuardians_update(Convert.ToString(lbGurds.SelectedValue), tbSurname_Guardian.Text, tbName_Guardian.Text, Convert.ToInt32(tbMiddleName_Guardian.Text));
            lbFill();
            lbFill2();
        }

        private void BtDeleteGuard_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Удалить выбранную запись?",
              "Удаление записи", MessageBoxButton.YesNo,
              MessageBoxImage.Warning))
            {
               case MessageBoxResult.Yes:
                    procedures.spGuardians_delete(
                       Convert.ToInt32(lbGurds.
                       SelectedValue.ToString()));
                    lbFill();
                   lbFill2();
                   break;
            }
        }

        private void LbGurds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (chbFilter.IsChecked)
            {
                case (true):
                    string newQR = QR +
                        " where [Guard_ID] = "
                        + lbGurds.SelectedValue.ToString();
                    dgFill(newQR);
                    break;
                case (false):
                    dgFill(QR);
                    break;
            }
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgPrisoners.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tbSearch.Text ||
                    dataRow.Row.ItemArray[7].ToString() == tbSearch.Text)
                {
                    dgPrisoners.SelectedItem = dataRow;
                }
            }
        }

        private void DgPrisoners_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Surname_Prisoner"):
                    e.Column.Header = "Фамилия заключённого";
                    break;
                case ("Name_Prisoner"):
                    e.Column.Header = "Имя заколючённого";
                    break;
                case ("MiddleName_Prisoner"):
                    e.Column.Header = "Отчество заключённого";
                    break;
                case ("Name_of_block"):
                    e.Column.Header = "Название блока";
                    break;
                case ("Surname_Guardian"):
                    e.Column.Header = "Фамилия охранника";
                    break;
                case ("Name_Guardian"):
                    e.Column.Header = "Имя охранника";
                    break;
                case ("MiddleName_Guardian"):
                    e.Column.Header = "Отчество охранника";
                    break;
            }
        }

        private void BtInsertPisoner_Click(object sender, RoutedEventArgs e)
        {
           procedures.spPrisoners_insert(tbSurname_Prisoner.Text,tbName_Prisoner.Text,tbMiddleName_Prisoner.Text, Convert.ToInt32(lbBlock.SelectedValue), Convert.ToInt32(lbGurds.SelectedValue));    
           dgFill(QR);
        }

        private void BtUpdatetPrisoner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView ID = (DataRowView)dgPrisoners.SelectedItems[0];
            if (ID == null)
                MessageBox.Show("", "");
            else
                procedures.spPrisoners_update(tbSurname_Prisoner.Text, tbName_Prisoner.Text, tbMiddleName_Prisoner.Text, Convert.ToInt32(ID["ID_Prisoner"]),Convert.ToInt32(lbGurds.SelectedValue), Convert.ToInt32(lbBlock.SelectedValue));
            dgFill(QR);
            }
            catch
            {

            }
        }

        private void BtDeletePrisoner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?",
              "Удаление записи", MessageBoxButton.YesNo,
               MessageBoxImage.Warning))
                {
                    case MessageBoxResult.Yes:
                        DataRowView ID =
                          (DataRowView)dgPrisoners.SelectedItems[0];
                        procedures.spPrisoners_delete(
                           Convert.ToInt32(ID["ID_Prisoner"]));
                        dgFill(QR);
                        break;
                }    
            }
                catch
            {

            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            Choise choise = new Choise();
            choise.Show();
            Visibility = Visibility.Collapsed;
        }

        private void BtFilter_Click(object sender, RoutedEventArgs e)
        {
            switch (chbFilter.IsChecked)
            {
                case (true):
                    string newQR = QR + " where [Surname_Prisoner] like '%" + tbSearch.Text + "%' or " +
                "[Name_Prisoner] like '%" + tbSearch.Text + "%' or " +
                "[MiddleName_Prisoner] like '%" + tbSearch.Text + "%'or " +
                "[Name_of_block] like '%" + tbSearch.Text + "%'or " +
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
