using System.Windows;
using Beeps.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System;

namespace Beeps.Views
{
    /// <summary>
    /// Interaction logic for ProductHomePage.xaml
    /// </summary>
    public partial class ProductHomePage : Window
    {
        public ProductHomePage()
        {
            InitializeComponent();
        }

        void ViewTenderCanvasLoad(object sender, RoutedEventArgs e)
        {
            ViewTenderCanvas.Visibility = Visibility.Visible;
            MyEqipmentCanvas.Visibility = Visibility.Collapsed;
            OfferTenderCanvas.Visibility = Visibility.Collapsed;

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

        private void BtnViewTender_Click(object sender, RoutedEventArgs e)
        {
            ViewTenderCanvas.Visibility = Visibility.Visible;
            MyEqipmentCanvas.Visibility = Visibility.Collapsed;
            OfferTenderCanvas.Visibility = Visibility.Collapsed;
            ViewTenderCanvasLoad(this, new RoutedEventArgs());
        }

        private void BtnMyProducts_Click(object sender, RoutedEventArgs e)
        {
            ViewTenderCanvas.Visibility = Visibility.Collapsed;
            MyEqipmentCanvas.Visibility = Visibility.Visible;
            OfferTenderCanvas.Visibility = Visibility.Collapsed;
        }

        private void DataGridDoubleClick(object sender, MouseButtonEventArgs e)
        {
   
            try
            {
                if (viewTenderDataGrid.SelectedItem == null)
                {
                    return;
                }                    
                DataRowView dataRowView = viewTenderDataGrid.SelectedItem as DataRowView;
                DataRow dataRow = dataRowView.Row;
                LblProductName.Content = Convert.ToString(dataRow.ItemArray[1]);
                LblDepartment.Content = Convert.ToString(dataRow.ItemArray[2]);
                LblMaxPrice.Content = Convert.ToString(dataRow.ItemArray[3]);
                LblClosingDate.Content = Convert.ToString(dataRow.ItemArray[4]);

                //Display the offer tender canvas with the details
                ViewTenderCanvas.Visibility = Visibility.Collapsed;
                MyEqipmentCanvas.Visibility = Visibility.Collapsed;
                OfferTenderCanvas.Visibility = Visibility.Visible;

                // MessageBox.Show(productName + department + maxAmount + closingDate);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnSubmitOffer_Click(object sender, RoutedEventArgs e)
        {
            string productName = LblProductName.Content.ToString();
            string department = LblDepartment.Content.ToString();
            string maxPrice = LblMaxPrice.Content.ToString();
            DateTime closingDate = DateTime.Parse(LblClosingDate.Content.ToString());
            var offeredPrice = TxtOfferedAmount.Text;
            //MessageBox.Show(productName+department+maxPrice+closingDate+offeredPrice);

            string command = $"INSERT INTO Product_table (ProductName, Department, MaxAmount, ClosingDate, PriceOffered) " +
                $"VALUES ('{productName}','{department}','{maxPrice}','{closingDate}', '{offeredPrice}')";
            ExecuteSqlQuery(command);
            LblStatus.Content = "Offered the tender amount";
        }

        private void BtnCancelOffer_Click(object sender, RoutedEventArgs e)
        {
            ViewTenderCanvas.Visibility = Visibility.Visible;
            MyEqipmentCanvas.Visibility = Visibility.Collapsed;
            OfferTenderCanvas.Visibility = Visibility.Collapsed;
        }

        public void ExecuteSqlQuery(string command)
        {
            DBClass.OpenConnection();
            DBClass.sql = command;
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            try
            {
                DBClass.cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            DBClass.CloseConnection();
        }
    }

    public class RowType
    {
        public string ProductName { get; set; }
        public string Department { get; set; }
        public string MaxAmount { get; set; }
        public string TenderClosing { get; set; }
    }
}
