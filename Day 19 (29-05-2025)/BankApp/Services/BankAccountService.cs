using BankApp.DTOs;
using BankApp.Interfaces;
using BankApp.Models;
using BankApp.Contexts;

namespace BankApp.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _repo;

        public BankAccountService(IBankAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<BankAccount> CreateAccount(CreateAccountDto dto)
        {
            var account = new BankAccount
            {
                Name = dto.Name,
                AccNumber = GenerateAccountNumber(),
                Balance = 0
            };
            return await _repo.Add(account);
        }

        public async Task<BankAccount> Deposit(string AccNumber, decimal amount)
        {
            var account = await _repo.GetByAccNum(AccNumber);
            if (account == null) throw new Exception("Account not found");
            account.Balance += amount;
            return await _repo.Update(account);
        }

        public async Task<BankAccount> Withdraw(string AccNumber, decimal amount)
        {
            var account = await _repo.GetByAccNum(AccNumber);
            if (account == null) throw new Exception("Account not found");
            if (account.Balance < amount) throw new Exception("Insufficient balance");
            account.Balance -= amount;
            return await _repo.Update(account);
        }

        public async Task<BankAccount> GetByAccNum(string AccNumber)
        {
            return await _repo.GetByAccNum(AccNumber);
        }

        private string GenerateAccountNumber()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 6);

        }
        
        
        public async Task<bool> TransferAsync(TransferRequestDto transferRequest)
        {
            var fromAccount = await _repo.GetByAccNum(transferRequest.FromAccNumber);
            var toAccount = await _repo.GetByAccNum(transferRequest.ToAccNumber);


            if (fromAccount == null || toAccount == null)
                return false;  

            if (fromAccount.Balance < transferRequest.Amount)
                return false;  

            fromAccount.Balance -= transferRequest.Amount;
            toAccount.Balance += transferRequest.Amount;

            var transaction = new Transaction
            {
                FromAccNumber = transferRequest.FromAccNumber,
                ToAccNumber = transferRequest.ToAccNumber,
                Amount = transferRequest.Amount,
                Timestamp = DateTime.UtcNow
            };

            await _repo.AddTransaction(transaction);


            return true;
        }



    }
}
