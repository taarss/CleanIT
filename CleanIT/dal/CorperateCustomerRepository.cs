using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CleanIT;

namespace CleanIT.dal
{
    public class CorperateCustomerRepository
    {
        public List<CorporateCustomer> GetCorporateCustomers()
        {
            biz business = new biz();
            DataSet dataSet = business.Execute("SELECT * FROM CorporateCustomer");
            DataTable customerTable = dataSet.Tables[0];
            List<CorporateCustomer> corporateCustomers = new List<CorporateCustomer>();
            foreach (DataRow itemRow in customerTable.Rows)
            {
                corporateCustomers.Add(
                    new CorporateCustomer((string)itemRow["companyName"], (int)itemRow["seNumber"], (int)itemRow["phoneNumber"])
                    );
            }
            return corporateCustomers;
        }
    }
}
