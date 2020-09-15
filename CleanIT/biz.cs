using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CleanIT.dal;
using System.Windows;
using System.Windows.Controls;

namespace CleanIT
{
    public class biz
    {
        private CorporateCustomer tempCorporateCustomer;
        private PrivateCustomer tempPrivateCustomer;
        private booking booking;
        private int id;
        private string connectionString = "Data Source=PC-BB-5987;Initial Catalog=cleanIt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DataSet Execute(string query)
        {
            
            DataSet resultSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, new SqlConnection(connectionString))))
            {
                adapter.Fill(resultSet);
            }
            return resultSet;
        }

        public void CreateBooking(string date, int workingHours, int hourlyPay, string bookingDescription, bool isNewCustomer, int tempId)
        {
            booking = new booking(tempPrivateCustomer, date, workingHours, hourlyPay, bookingDescription, false);
            //checks if it needs to create a new customer or just refrence an already existing one.
            if (isNewCustomer == true)
            {
                //Is a private type customer
                if (tempPrivateCustomer != null)
                {   
                    String query = "INSERT INTO PrivateCustomer (firstName,lastname,address,zipCode,phoneNumber,amountSpent) VALUES (@firstName, @lastName, @address, @zipCode, @phoneNumber, @amountSpent)";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", tempPrivateCustomer.FirstName);
                        command.Parameters.AddWithValue("@lastName", tempPrivateCustomer.LastName);
                        command.Parameters.AddWithValue("@address", tempPrivateCustomer.Address);
                        command.Parameters.AddWithValue("@zipCode", tempPrivateCustomer.ZipCode);
                        command.Parameters.AddWithValue("@phoneNumber", tempPrivateCustomer.PhoneNumber);
                        command.Parameters.AddWithValue("@amountSpent", tempPrivateCustomer.AmountSpent);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Error while inserting private customer into database");
                    }
                    DataSet dataSet = Execute("SELECT id FROM PrivateCustomer ORDER BY id ASC");
                    DataTable customerTable = dataSet.Tables[0];
                    List<int> privateCustomers = new List<int>();
                    foreach (DataRow itemRow in customerTable.Rows)
                    {
                        privateCustomers.Add(
                            (int)itemRow["id"]);
                    }
                    id = privateCustomers[privateCustomers.Count - 1];
                               
                }

              //is a corporate type customer
                else
                {                   
                        String query = "INSERT INTO CorporateCustomer (companyName,seNumber,phoneNumber,amountSpent) VALUES (@companyName, @seNumber, @phoneNumber, @amountSpent)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@companyName", tempCorporateCustomer.CompanyName);
                            command.Parameters.AddWithValue("@seNumber", tempCorporateCustomer.SeNumber);
                            command.Parameters.AddWithValue("@phoneNumber", tempCorporateCustomer.PhoneNumber);
                            command.Parameters.AddWithValue("@amountSpent", tempCorporateCustomer.AmountSpent);
                        connection.Open();
                            int result = command.ExecuteNonQuery();

                            // Check Error
                            if (result < 0)
                                MessageBox.Show("Error while inserting private customer into database. Restart and try again");
                        }
                        DataSet dataSet = Execute("SELECT id FROM CorporateCustomer ORDER BY id ASC");
                        DataTable customerTable = dataSet.Tables[0];
                        List<int> privateCustomers = new List<int>();
                        foreach (DataRow itemRow in customerTable.Rows)
                        {
                            privateCustomers.Add(
                                (int)itemRow["id"]);
                        }
                        id = privateCustomers[privateCustomers.Count - 1];                                
                } 
                //After creation of customer create booking.
                String bookingQuery = "INSERT INTO Booking (workingHours,hourlyPay,bookingDescription,workComplete,foreignKey) VALUES (@workingHours, @hourlyPay, @bookingDescription, @workComplete, @foreignKey)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(bookingQuery, connection))
                {
                    command.Parameters.AddWithValue("@workingHours", booking.WorkingHours);
                    command.Parameters.AddWithValue("@hourlyPay", booking.HourlyPay);
                    command.Parameters.AddWithValue("@bookingDescription", booking.BookingDescription);
                    command.Parameters.AddWithValue("@workComplete", booking.WorkComplete);
                    command.Parameters.AddWithValue("@foreignKey", id);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        MessageBox.Show("Error while inserting private customer into database");
                }
                
            }


            //creates a booking with an id refrence to an already existing customer            
            else
            {
                //After creation of customer create booking.
                String bookingQuery = "INSERT INTO Booking (workingHours,hourlyPay,bookingDescription,workComplete,foreignKey,date) VALUES (@workingHours, @hourlyPay, @bookingDescription, @workComplete, @foreignKey, @date)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(bookingQuery, connection))
                {
                    command.Parameters.AddWithValue("@workingHours", booking.WorkingHours);
                    command.Parameters.AddWithValue("@hourlyPay", booking.HourlyPay);
                    command.Parameters.AddWithValue("@bookingDescription", booking.BookingDescription);
                    command.Parameters.AddWithValue("@workComplete", booking.WorkComplete);
                    command.Parameters.AddWithValue("@foreignKey", tempId);
                    command.Parameters.AddWithValue("@date", booking.Date);
                    PrivateCustomerRepository privateCustomerRepository = new PrivateCustomerRepository();
                    CorperateCustomerRepository corperateCustomerRepository = new CorperateCustomerRepository();
                    List<PrivateCustomer> privateCustomers = privateCustomerRepository.GetPrivateCustomers();
                    List<CorporateCustomer> corporateCustomers = corperateCustomerRepository.GetCorporateCustomers();
                    foreach (var item in privateCustomers)
                    {
                        if (item.Id == tempId)
                        {
                            int amountSpent = item.AmountSpent + (booking.WorkingHours * booking.HourlyPay);
                            InsertAmountSpent("PrivateCustomer", tempId.ToString(), amountSpent);
                        }
                    }
                    foreach (var item in corporateCustomers)
                    {
                        if (item.Id == tempId)
                        {
                            int amountSpent = item.AmountSpent + (booking.WorkingHours * booking.HourlyPay);
                            InsertAmountSpent("CorporateCustomer", tempId.ToString(), amountSpent);
                        }
                    }

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        MessageBox.Show("Error while inserting private customer into database");
                }
            }
            
            

            tempCorporateCustomer = null;
            tempPrivateCustomer = null;

        }


        private void InsertAmountSpent(string typeCustomer, string id, int amountSpent)
        {
            //String bookingQuery = $"UPDATE {typeCustomer} (amountSpent) VALUES (@amountSpent)";
            String bookingQuery = $"UPDATE {typeCustomer} SET amountSpent = @amountSpent WHERE id = {id} "; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(bookingQuery, connection))
            {
                command.Parameters.AddWithValue("@amountSpent", amountSpent);
                connection.Open();
                int result = command.ExecuteNonQuery();               
            }
        }


        public bool CreatePrivateCustomer(string firstName, string lastName, string address, int zipCode, int phoneNumber)
        {
            if (phoneNumber.ToString().Length != 8)
            {
                MessageBox.Show("Telefon nummer er ikke gyldigt");
                return false;
            }
            if (DoesPrivateCustomerExists(phoneNumber) == true)
            {
                MessageBox.Show("Der findes allerede en kunde med dette nummer i systemet");
                return false;
            }
            else
            {
                tempPrivateCustomer = new PrivateCustomer(firstName, lastName, address, zipCode, phoneNumber, 0, 0);
                return true;
            }
        }

        public bool CreateCorporateCustomer(string companyName, int phoneNumber, int seNumber)
        {
            if (phoneNumber.ToString().Length != 8)
            {
                MessageBox.Show("Telefon nummer er ikke gyldigt");
                return false;
            }
            if (DoesCorporateCustomerExist(phoneNumber) == true)
            {
                MessageBox.Show("Der findes allerede en kunde med dette nummer i systemet");
                return false;
            }
            else
            {
                tempCorporateCustomer = new CorporateCustomer(companyName, seNumber, phoneNumber, 0, 0);
                return true;
            }

        }



        private bool DoesPrivateCustomerExists(int phoneNumber)
        {
            PrivateCustomerRepository privateCustomer = new PrivateCustomerRepository();           
            foreach (var item in privateCustomer.GetPrivateCustomers())
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    return true;
                }
                
            }            
            return false;           
        }

        private bool DoesCorporateCustomerExist(int phoneNumber)
        {
            CorperateCustomerRepository corperateCustomer = new CorperateCustomerRepository();
            foreach (var item in corperateCustomer.GetCorporateCustomers())
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    return true;
                }
            }
            
            return false;
        }
        /*
        {
        private bool IsDatedBooked(string date)
            DataSet dataSet =Execute("SELECT * FROM Bookings");
            DataTable customerTable = dataSet.Tables[0];
            List<booking> bookings = new List<booking>();
            foreach (DataRow itemRow in customerTable.Rows)
            {

                
                privateCustomers.Add(
                    new PrivateCustomer((string)itemRow["firstName"], (string)itemRow["lastName"], (string)itemRow["address"], (int)itemRow["zipCode"], (int)itemRow["phoneNumber"], (int)itemRow["id"], (int)itemRow["amountSpent"]));
            
            }

            CorperateCustomerRepository corperateCustomer = new CorperateCustomerRepository();
            foreach (var item in corperateCustomer.GetCorporateCustomers())
            {
                if (item.PhoneNumber == phoneNumber)
                {
                    return true;
                }
            }
        }
            return false;*/



    }
}
