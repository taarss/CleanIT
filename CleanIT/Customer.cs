using System;
using System.Collections.Generic;
using System.Text;

namespace CleanIT
{
    public class Customer
    {
        private int phoneNumber;

        public Customer(int phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }

        public int PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
    }
}
