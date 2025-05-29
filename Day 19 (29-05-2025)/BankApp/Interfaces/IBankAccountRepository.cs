using BankApp.Models;
using BankApp.DTOs;

namespace BankApp.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> Add(BankAccount account);
        Task<BankAccount> GetByAccNum(string AccNumber);
        Task<BankAccount> Update(BankAccount account);
        Task<bool> TransferAsync(TransferRequestDto request); 
        Task<Transaction> AddTransaction(Transaction transaction);


    }
}