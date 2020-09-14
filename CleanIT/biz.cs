using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CleanIT.dal;
using System.Windows;

namespace CleanIT
{
    public class biz
    {
        private CorporateCustomer tempCorporateCustomer;
        private PrivateCustomer tempPrivateCustomer;
        private booking booking;
        private int id;
        private string connectionString = "Data Source=DESKTOP-7VJ1O7V;Initial Catalog=cleanIt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DataSet Execute(string query)
        {
            
            DataSet resultSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, new SqlConnection(connectionString))))
            {
                adapter.Fill(resultSet);
            }
            return resultSet;
        }

        public void CreateBooking(string date, int workingHours, int hourlyPay, string bookingDescription)
        {


            //Is a private type customer
            if (tempPrivateCustomer != null)
            {
                


                if (DoesPrivateCustomerExists(tempPrivateCustomer.PhoneNumber) == false)
                {
                    
                    booking = new booking(tempPrivateCustomer, date, workingHours, hourlyPay, bookingDescription, false);
                    String query = "INSERT INTO PrivateCustomer (firstName,lastname,address,zipCode,phoneNumber) VALUES (@firstName, @lastName, @address, @zipCode, @phoneNumber)";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", tempPrivateCustomer.FirstName);
                        command.Parameters.AddWithValue("@lastName", tempPrivateCustomer.LastName);
                        command.Parameters.AddWithValue("@address", tempPrivateCustomer.Address);
                        command.Parameters.AddWithValue("@zipCode", tempPrivateCustomer.ZipCode);
                        command.Parameters.AddWithValue("@phoneNumber", tempPrivateCustomer.PhoneNumber);

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
                else
                {
                    MessageBox.Show("Der findes allerede en kunde med dette nummer i systemet");
                }                
            }











            //is a corporate type customer
            else
            {
                if (DoesPrivateCustomerExists(tempCorporateCustomer.PhoneNumber) == false)
                {
                    
                    booking = new booking(tempCorporateCustomer, date, workingHours, hourlyPay, bookingDescription, false);
                    String query = "INSERT INTO CorporateCustomer (companyName,seNumber,phoneNumber) VALUES (@companyName, @seNumber, @phoneNumber)";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@companyName", tempCorporateCustomer.CompanyName);
                        command.Parameters.AddWithValue("@seNumber", tempCorporateCustomer.SeNumber);
                        command.Parameters.AddWithValue("@phoneNumber", tempCorporateCustomer.PhoneNumber);
                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Error while inserting private customer into database");
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
                else
                {
                    MessageBox.Show("Der findes allerede en kunde med dette nummer i systemet");
                }
                
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

            tempCorporateCustomer = null;
            tempPrivateCustomer = null;

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
                tempPrivateCustomer = new PrivateCustomer(firstName, lastName, address, zipCode, phoneNumber, 0);
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
                tempCorporateCustomer = new CorporateCustomer(companyName, seNumber, phoneNumber, 0);
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



    }
}
