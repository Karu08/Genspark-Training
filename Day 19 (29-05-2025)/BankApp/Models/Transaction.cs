using System;

namespace BankApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string FromAccNumber { get; set; } = string.Empty;
        public string ToAccNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}