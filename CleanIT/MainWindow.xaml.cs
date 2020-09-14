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
using CleanIT.dal;

namespace CleanIT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        biz biz = new biz();
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
            newPrivatCustomer.Visibility = Visibility.Hidden;
            newCorperateCustomer.Visibility = Visibility.Hidden;
            customerList.Children.Clear();
            CorperateCustomerRepository corperate = new CorperateCustomerRepository();
            PrivateCustomerRepository privateCustomer = new PrivateCustomerRepository();
            listCustomer.Visibility = Visibility.Visible;
            List<CorporateCustomer> corporateCustomers = corperate.GetCorporateCustomers();
            foreach (var item in corporateCustomers)
            {
                Button button = new Button();
                button.Width = 235;
                button.Content = item.CompanyName;
                button.Tag = item.Id;
                button.Click += new RoutedEventHandler(pickExistingCustomer_click);
                customerList.Children.Add(button);
            }
            List<PrivateCustomer> privateCustomers = privateCustomer.GetPrivateCustomers();
            foreach (var item in privateCustomers)
            {
                Button button = new Button();
                button.Width = 235;
                button.Content = item.FirstName + " "+ item.LastName;
                button.Tag = item.Id;
                button.Click += new RoutedEventHandler(pickExistingCustomer_click);
                customerList.Children.Add(button);
            }

        }

        private void pickExistingCustomer_click(object sender, RoutedEventArgs e)
        {
            listCustomer.Visibility = Visibility.Hidden;
            privateCustomerBtn.Visibility = Visibility.Hidden;
            corperateCustomerBtn.Visibility = Visibility.Hidden;

        }

        private void corperateCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            newCorperateCustomer.Visibility = Visibility.Visible;
            pickCustomerFromDb.Visibility = Visibility.Hidden;

        }

        private void privateCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            newPrivatCustomer.Visibility = Visibility.Visible;
            pickCustomerFromDb.Visibility = Visibility.Hidden;
        }

        private void nextInputPagePrivate_Click(object sender, RoutedEventArgs e)
        {
            
            if (firstnameInput.Text != "" && lastnameInput.Text != null && addressInput.Text != null && zipCodeInput.Text != null && phoneNumberInput != null)
            {
                inputWorkInfo.Visibility = Visibility.Visible;
                biz.createPrivateCustomer(firstnameInput.Text, lastnameInput.Text, addressInput.Text, Convert.ToInt32(zipCodeInput.Text), Convert.ToInt32(phoneNumberInput.Text));
            }
            else
            {
                MessageBox.Show("Husk at indtaste alle punkter");
            }
        }

        public void customerAlreadyExistsError()
        {
            MessageBox.Show("A customer with that phone number already exists");
        }

        private void createBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dateInputDate.Text != null && dateInputMonth != null && dateInputYear.Text != null)
            {
                string completeDate = $"{dateInputDate.Text.ToString()}-{dateInputMonth.Text.ToString()}-{dateInputYear.Text.ToString()}";
            }
        }
    }
}
