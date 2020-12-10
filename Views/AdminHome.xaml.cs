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
            repairRequests.Visibility = Visibility.Collapsed;

        }

        public void OnWindowLoaded(object sender, RoutedEventArgs e)

    {

            //MessageBox.Show("hellow world");

    }

    private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newTenderCanvas.Visibility = Visibility.Visible;
        }
    }
}
