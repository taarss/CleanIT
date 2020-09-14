using System;
using System.Collections.Generic;
using System.Text;

namespace CleanIT
{
    public class CorporateCustomer : Customer
    {
        private string companyName;
        private int seNumber;


        public CorporateCustomer(string companyName, int seNumber, int phoneNumber, int id) : base(phoneNumber, id)
        {
            this.CompanyName = companyName;
            this.SeNumber = seNumber;
        }

        public string CompanyName { get => companyName; set => companyName = value; }
        public int SeNumber { get => seNumber; set => seNumber = value; }
    }
}
