using System;
using System.Collections.Generic;
using System.Data;
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
        private int tempId;
        private bool passTempId;
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            passTempId = false;
            overview.Visibility = Visibility.Hidden;
            homeScreen.Visibility = Visibility.Visible;
            booking.Visibility = Visibility.Hidden;
            newPrivatCustomer.Visibility = Visibility.Hidden;
            newCorperateCustomer.Visibility = Visibility.Hidden;
            inputWorkInfo.Visibility = Visibility.Hidden;
            bookingCreated.Visibility = Visibility.Hidden;
            clearTextBox(newPrivatCustomer);
            clearTextBox(newCorperateCustomer);
            clearTextBox(inputWorkInfo);

        }

        private void overviewBtn_Click(object sender, RoutedEventArgs e)
        {
            privateCustomerList.Children.Clear();
            CorporateCustomerList.Children.Clear();
            passTempId = false;
            homeScreen.Visibility = Visibility.Hidden;
            booking.Visibility = Visibility.Hidden;
            newPrivatCustomer.Visibility = Visibility.Hidden;
            newCorperateCustomer.Visibility = Visibility.Hidden;
            inputWorkInfo.Visibility = Visibility.Hidden;
            bookingCreated.Visibility = Visibility.Hidden;
            overview.Visibility = Visibility.Visible;          
        }

        private void openCustomerDetail_Click(object sender, RoutedEventArgs e)
        {
            PrivateCustomerRepository privateCustomerRepository = new PrivateCustomerRepository();
            CorperateCustomerRepository corperateCustomerRepository = new CorperateCustomerRepository();
            customerDetails.Visibility = Visibility.Visible;
            Button button = sender as Button;
            int index = Convert.ToInt32(button.Tag);
            List<PrivateCustomer> privateCustomers = privateCustomerRepository.GetPrivateCustomers();
            List<CorporateCustomer> corporateCustomers = corperateCustomerRepository.GetCorporateCustomers();
            foreach (var item in privateCustomers)
            {
                //Hjælp fra nico, erik og anton. Kunne ikke se et fejlplaceret ';'
                if (item.Id == index)
                {
                    customerName.Text = item.FirstName + " " + item.LastName;
                    customerPhoneNumber.Text = item.PhoneNumber.ToString();
                    customerAmountSpent.Text = item.AmountSpent.ToString();
                }
            }
            foreach (var item in corporateCustomers)
            {
                if (item.Id == index)
                {
                    customerName.Text = item.CompanyName;
                    customerPhoneNumber.Text = item.PhoneNumber.ToString();
                    customerAmountSpent.Text = item.AmountSpent.ToString();
                }
            }
        }



        private void bookingBtn_Click(object sender, RoutedEventArgs e)
        {
            homeScreen.Visibility = Visibility.Hidden;
            booking.Visibility = Visibility.Visible;
            overview.Visibility = Visibility.Hidden;
            listCustomer.Visibility = Visibility.Hidden;
        }

        private void closeProgram_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
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
            inputWorkInfo.Visibility = Visibility.Visible;
            Button button = sender as Button;
            tempId = Convert.ToInt32(button.Tag);
            passTempId = true;

        }

        private void corperateCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            newCorperateCustomer.Visibility = Visibility.Visible;
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

                MessageBox.Show("Forket input");
            }
            
            
        }
        private void createBookingBtn_Click(object sender, RoutedEventArgs e)
        {           
                if (dateInputDate.Text != "" && dateInputMonth.Text != "" && dateInputYear.Text != "")
                {
                    Convert.ToInt32(dateInputDate.Text);
                    Convert.ToInt32(dateInputMonth.Text);
                    Convert.ToInt32(dateInputYear.Text);
                    Convert.ToInt32(inputHourlyPay.Text);
                    Convert.ToInt32(inputWorkingHours.Text);
                }
                else
                {
                    MessageBox.Show("Indtast alle dato felter");
                }
                if (dateInputDate.Text.Length > 2 && dateInputMonth.Text.Length > 2 && dateInputYear.Text.Length > 4)
                {
                    MessageBox.Show("Indtast en gyldig dato.");
                }
                if (bookingDescription.Text == "")
                {
                    MessageBox.Show("Du skal angive en booking beskrivelse");
                }
                if (inputWorkingHours.Text == "")
                {
                    MessageBox.Show("Du skal angive hvor mange timer abrjedet vil tage.");
                }
                string date = biz.formatDate(dateInputDate.Text, dateInputMonth.Text, dateInputYear.Text);
                if (biz.IsDateBooked(date))
                {
                    MessageBox.Show("Denne dato er alerade booket.");
                }
                else
                {
                    bool isNewCustomer;
                    if (passTempId == false)
                    {
                        isNewCustomer = true;
                    }
                    
                    else
                    {
                        isNewCustomer = false;
                    }
                    biz.CreateBooking(date, Convert.ToInt32(inputWorkingHours.Text), Convert.ToInt32(inputHourlyPay.Text), bookingDescription.Text, isNewCustomer, tempId);
                bookingCreated.Visibility = Visibility.Visible;
                }

            
            


        }

        private void clearTextBox(Canvas canvasName)
        {

            foreach (UIElement control in canvasName.Children)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    TextBox txtBox = (TextBox)control;
                    txtBox.Text = null;
                }
            }


            
        }

        private void closeDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            customerDetails.Visibility = Visibility.Hidden;
            bookingDetails.Visibility = Visibility.Hidden;
        }


        void OnMouseMoveHandler(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(RootCanvas);
            double pX = p.X;
            double pY = p.Y;
            Canvas.SetTop(customPointer, pY);
            Canvas.SetLeft(customPointer, pX);
            Cursor = Cursors.None;
        }

        private void customerBtn_Click(object sender, RoutedEventArgs e)
        {
            privateCustomerList.Children.Clear();
            CorporateCustomerList.Children.Clear();
            viewCustomers.Visibility = Visibility.Visible;
            bookingList.Visibility = Visibility.Hidden;
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
                button.Click += new RoutedEventHandler(openCustomerDetail_Click);
                CorporateCustomerList.Children.Add(button);
            }
            List<PrivateCustomer> privateCustomers = privateCustomer.GetPrivateCustomers();
            foreach (var item in privateCustomers)
            {
                Button button = new Button();
                button.Width = 235;
                button.Content = item.FirstName + " " + item.LastName;
                button.Tag = item.Id;
                button.Click += new RoutedEventHandler(openCustomerDetail_Click);
                privateCustomerList.Children.Add(button);
            }
        }

        private void bookingsBtn_Click(object sender, RoutedEventArgs e)
        {
            viewCustomers.Visibility = Visibility.Hidden;
            bookingList.Visibility = Visibility.Visible;
            List<booking> bookings = biz.getBookings();
            foreach (var item in bookings)
            {
                if (item.WorkComplete == true)
                {
                    Button button = new Button();
                    button.Width = 235;
                    button.Content = item.Date;
                    button.Tag = item.Id;
                    button.Click += new RoutedEventHandler(openBookingDetail_Click);
                    bookingsListComplete.Children.Add(button);
                }
                else
                {
                    Button button = new Button();
                    button.Width = 235;
                    button.Content = item.Date;
                    button.Tag = item.Id;
                    button.Click += new RoutedEventHandler(openBookingDetail_Click);
                    bookingsListIncomplete.Children.Add(button);
                }
            }           
        }

        private void openBookingDetail_Click(object sender, RoutedEventArgs e)
        {
            bookingDetails.Visibility = Visibility.Visible;
            Button button = sender as Button;
            int index = Convert.ToInt32(button.Tag);
            List<booking> bookings =  biz.getBookings();
            foreach (var item in bookings)
            {
                if (item.Id == index)
                {
                    workDate.Text = item.Date;
                    workPay.Text = item.HourlyPay.ToString();
                    workTime.Text = item.WorkingHours.ToString();
                }
            }
        }



        private void overviewBackBtn_Click(object sender, RoutedEventArgs e)
        {
            viewCustomers.Visibility = Visibility.Hidden;
            bookingList.Visibility = Visibility.Hidden;
        }
    }
}
