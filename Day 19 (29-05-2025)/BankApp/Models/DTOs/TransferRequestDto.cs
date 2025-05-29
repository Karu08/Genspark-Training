namespace BankApp.DTOs
{
    public class TransferRequestDto
    {
        public string FromAccNumber { get; set; } = string.Empty;
        public string ToAccNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
