using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CleanIT;

namespace CleanIT.dal
{
    public class PrivateCustomerRepository
    {
        public List<PrivateCustomer> GetPrivateCustomers()
        {
            biz business = new biz();
            DataSet dataSet = business.Execute("SELECT * FROM PrivateCustomer");
            DataTable customerTable = dataSet.Tables[0];
            List<PrivateCustomer> privateCustomers = new List<PrivateCustomer>();
            foreach (DataRow itemRow in customerTable.Rows)
            {
                privateCustomers.Add(
                    new PrivateCustomer((string)itemRow["firstName"], (string)itemRow["lastName"], (string)itemRow["address"], (int)itemRow["zipCode"], (int)itemRow["phoneNumber"])
                    );
            }
            return privateCustomers;
        }
    }
}
