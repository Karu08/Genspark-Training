using BankApp.DTOs;
using BankApp.Models;

namespace BankApp.Interfaces
{
    public interface IBankAccountService
    {
        Task<BankAccount> CreateAccount(CreateAccountDto dto);
        Task<BankAccount> GetByAccNum(string AccNumber);
        Task<BankAccount> Deposit(string AccNumber, decimal amount);
        Task<BankAccount> Withdraw(string AccNumber, decimal amount);
        Task<bool> TransferAsync(TransferRequestDto transferRequest);


        
    }
}