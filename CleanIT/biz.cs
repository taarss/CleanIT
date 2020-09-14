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

        private PrivateCustomer tempPrivateCustomer;
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

        public void createPrivateCustomer(string firstName, string lastName, string address, int zipCode, int phoneNumber)
        {
            if (DoesPrivateCustomerExists(phoneNumber) == true)
            {
                MessageBox.Show("En kunde med samme telfon nummber findes alerede");
            }
            else
            {
                tempPrivateCustomer = new PrivateCustomer(firstName, lastName, address, zipCode, phoneNumber, 0);

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



    }
}
