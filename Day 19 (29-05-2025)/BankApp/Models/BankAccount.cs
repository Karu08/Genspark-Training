namespace BankApp.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}