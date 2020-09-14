using System;
using System.Collections.Generic;
using System.Text;

namespace CleanIT
{
    public class PrivateCustomer : Customer
    {
        private string firstName;
        private string lastName;
        private string address;
        private int zipCode;

        public PrivateCustomer(string firstName, string lastName, string address, int zipCode, int phoneNumber) : base(phoneNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.ZipCode = zipCode;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Address { get => address; set => address = value; }
        public int ZipCode { get => zipCode; set => zipCode = value; }
    }
}
