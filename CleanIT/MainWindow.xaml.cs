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

namespace CleanIT
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

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            homeScreen.Visibility = Visibility.Visible;
        }

        private void overviewBtn_Click(object sender, RoutedEventArgs e)
        {
            homeScreen.Visibility = Visibility.Hidden;

        }

        private void bookingBtn_Click(object sender, RoutedEventArgs e)
        {
            homeScreen.Visibility = Visibility.Hidden;
        }

        private void closeProgram_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pickCustomerFromDb_Click(object sender, RoutedEventArgs e)
        {
            existingCustomerList.Visibility = Visibility.Visible;
        }
    }
}
