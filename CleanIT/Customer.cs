using System;
using System.Collections.Generic;
using System.Text;

namespace CleanIT
{
    public class Customer
    {
        private int phoneNumber;
        private int id;
        public Customer(int phoneNumber, int id)
        {
            this.PhoneNumber = phoneNumber;
            this.Id = id;
        }

        public int PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public int Id { get => id; set => id = value; }
    }
}
