using System;
using System.Collections.Generic;
using System.Text;

namespace CleanIT
{
    public class CorporateCustomer : Customer
    {
        private string companyName;
        private int seNumber;
        private int amountSpent;


        public CorporateCustomer(string companyName, int seNumber, int phoneNumber, int id, int amountSpent) : base(phoneNumber, id)
        {
            this.CompanyName = companyName;
            this.SeNumber = seNumber;
            this.AmountSpent = amountSpent;
        }

        public string CompanyName { get => companyName; set => companyName = value; }
        public int SeNumber { get => seNumber; set => seNumber = value; }
        public int AmountSpent { get => amountSpent; set => amountSpent = value; }
    }
}
