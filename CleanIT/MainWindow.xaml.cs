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
            try
            {
                if (firstnameInput.Text != "" && lastnameInput.Text != "" && addressInput.Text != "" && zipCodeInput.Text != "" && phoneNumberInput.Text != "")
                {
                    if (biz.CreatePrivateCustomer(firstnameInput.Text, lastnameInput.Text, addressInput.Text, Convert.ToInt32(zipCodeInput.Text), Convert.ToInt32(phoneNumberInput.Text)) == true)
                    {
                        newPrivatCustomer.Visibility = Visibility.Hidden;
                        inputWorkInfo.Visibility = Visibility.Visible;
                    } 
                }
                else
                {
                    MessageBox.Show("Husk at indtaste alle punkter");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Forket input. En eller flere felter har fået et forket slags input");
            }

        }
        private void nextInputPageCorporate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (companyNameInput.Text != "" && companyPhoneInput.Text != "" && seNumberInput.Text != "")
                {
                    if (biz.CreateCorporateCustomer(companyNameInput.Text, Convert.ToInt32(companyPhoneInput.Text), Convert.ToInt32(seNumberInput.Text)) == true)
                    {
                        newCorperateCustomer.Visibility = Visibility.Hidden;
                        inputWorkInfo.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show("Husk at indtaste alle punkter");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }
        private void createBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            string completeDate = "";
           
                if (dateInputDate.Text != "" && dateInputMonth.Text != "" && dateInputYear.Text != "")
                {
                    Convert.ToInt32(dateInputDate.Text);
                    Convert.ToInt32(dateInputMonth.Text);
                    Convert.ToInt32(dateInputYear.Text);
                    Convert.ToInt32(inputHourlyPay.Text);
                    Convert.ToInt32(inputWorkingHours.Text);
                    completeDate = $"{dateInputDate.Text.ToString()}-{dateInputMonth.Text.ToString()}-{dateInputYear.Text.ToString()}";
                }
                else
                {
                    MessageBox.Show("Indtast alle dato felter");
                }
                if (bookingDescription.Text == "")
                {
                    MessageBox.Show("Du skal angive en booking beskrivelse");
                }
                if (inputWorkingHours.Text == "")
                {
                    MessageBox.Show("Du skal angive hvor mange timer abrjedet vil tage.");
                }
                else
                {
                    int hourlyPay = 0;
                    if (inputHourlyPay.Text == "")
                    {
                        hourlyPay = 150;
                    }
                    biz.CreateBooking(completeDate, Convert.ToInt32(inputWorkingHours.Text), hourlyPay, bookingDescription.Text);
                }

            
            


        }

       
    }
}
