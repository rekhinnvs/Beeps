using System.Data;
using System.Windows;
using Beeps.Classes;
using System.Data.SqlClient;
using System.Globalization;
using System;

namespace Beeps.Views
{
    /// <summary>
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Window
    {

        public AdminHome()
        {
            InitializeComponent();
            newTenderCanvas.Visibility = Visibility.Collapsed;
            viewTenderCanvas.Visibility = Visibility.Collapsed;
            repairCanvas.Visibility = Visibility.Collapsed;

        }


        private void BtnNewTender_Click(object sender, RoutedEventArgs e)
        {
            newTenderCanvas.Visibility = Visibility.Visible;
            viewTenderCanvas.Visibility = Visibility.Collapsed;
            repairCanvas.Visibility = Visibility.Collapsed;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var productName = txtProductName.Text;
            var department = txtDepartment.Text;
            var maxPrice = txtMaxAmount.Text;
            DateTime closingDate = (DateTime)datePicker.SelectedDate;
            //insert the data in to a table
            DBClass.OpenConnection();
            DBClass.sql = $"INSERT INTO Tender_table (ProductName, Department, MaxAmount, TenderClosing, TenderStatus) " +
                $"VALUES ('{productName}','{department}','{maxPrice}','{closingDate}', 'New')";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;
           DBClass.cmd.ExecuteNonQuery();
            lblInfo.Content = "Created a new tender";
            DBClass.CloseConnection();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtProductName.Text=null;
            txtDepartment.Text=null;
            txtMaxAmount.Text=null;
            datePicker.SelectedDate = null;
        }

        private void BtnViewTender_Click(object sender, RoutedEventArgs e)
        {
            newTenderCanvas.Visibility = Visibility.Collapsed;
            viewTenderCanvas.Visibility = Visibility.Visible;
            repairCanvas.Visibility = Visibility.Collapsed;

            //get the data from the table in show it in a data grid
            DBClass.OpenConnection();
            DBClass.sql = "SELECT * FROM Tender_table";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;
            DBClass.dAdapter = new SqlDataAdapter(DBClass.cmd);
            DBClass.dTable = new DataTable();
            DBClass.dAdapter.Fill(DBClass.dTable);

            viewTenderDataGrid.ItemsSource = DBClass.dTable.DefaultView;
            DBClass.CloseConnection();
        }

        private void BtnRepair_Click(object sender, RoutedEventArgs e)
        {
            newTenderCanvas.Visibility = Visibility.Collapsed;
            viewTenderCanvas.Visibility = Visibility.Collapsed;
            repairCanvas.Visibility = Visibility.Visible;
        }
    }
}
