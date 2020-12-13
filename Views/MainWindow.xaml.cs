using System;
using System.Windows;
using Beeps.Classes;
using System.Data;


namespace Beeps.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            DBClass.OpenConnection();
            DBClass.sql = $"SELECT COUNT(1) FROM Users WHERE Username= '{txtUserName.Text}' AND Password= '{txtPassword.Password}'";

            //DBClass.sql = "SELECT COUNT(1) FROM Users WHERE Username='@userName' AND Password= '@userName'";
            DBClass.cmd.CommandType = CommandType.Text;
            DBClass.cmd.CommandText = DBClass.sql;

            int count = Convert.ToInt32(DBClass.cmd.ExecuteScalar());
            

            if (count == 1)
            {
                //Check whether the user is an administrator.
                DBClass.sql = $"SELECT * FROM Users WHERE Username= '{txtUserName.Text}'";                
                DBClass.cmd.CommandType = CommandType.Text;
                DBClass.cmd.CommandText = DBClass.sql;
                DBClass.dReader = DBClass.cmd.ExecuteReader();
                if (DBClass.dReader != null)
                {
                    bool admin = false;
                    while (DBClass.dReader.Read())
                    {
                        if(DBClass.dReader["UserName"].ToString().Equals(txtUserName.Text) && DBClass.dReader["IsAdmin"].ToString().Equals("True"))
                        {
                            admin = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }

                    }

                    if(admin)
                    {
                        AdminHome adminHome = new AdminHome();
                        DBClass.CloseConnection();
                        adminHome.Show();
                        this.Close();
                    }
                    else
                    {
                        ProductHomePage productHome = new ProductHomePage();
                        DBClass.CloseConnection();
                        productHome.Show();
                        this.Close();

                    }
                }
            }
            else
            {
                MessageBox.Show("User name or password is incorrect");
                txtPassword.Password = "";
                txtUserName.Text = "";
            }
            
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
