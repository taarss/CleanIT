using System;
using System.Collections.Generic;
using System.Text;

namespace CleanIT
{
    public class booking
    {
        private Customer customer;
        private string date;
        private int workingHours;
        private int hourlyPay;
        private string bookingDescription;
        private bool workComplete;
        private int id;

        public booking(Customer customer, string date, int workingHours, int hourlyPay, string bookingDescription, bool workComplete, int id)
        {
            this.Customer = customer;
            this.Date = date;
            this.WorkingHours = workingHours;
            this.HourlyPay = hourlyPay;
            this.BookingDescription = bookingDescription;
            this.WorkComplete = workComplete;
            this.Id = id;
        }

        public Customer Customer { get => customer; set => customer = value; }
        public string Date { get => date; set => date = value; }
        public int WorkingHours { get => workingHours; set => workingHours = value; }
        public int HourlyPay { get => hourlyPay; set => hourlyPay = value; }
        public string BookingDescription { get => bookingDescription; set => bookingDescription = value; }
        public bool WorkComplete { get => workComplete; set => workComplete = value; }
        public int Id { get => id; set => id = value; }
    }
}
