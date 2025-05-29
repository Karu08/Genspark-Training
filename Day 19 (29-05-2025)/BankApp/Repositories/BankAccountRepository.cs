using BankApp.Contexts;
using BankApp.DTOs;
using BankApp.Interfaces;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankApp.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly BankDbContext _context;

        public BankAccountRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> Add(BankAccount account)
        {
            _context.BankAccounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<BankAccount> GetByAccNum(string accNumber)
        {
            return await _context.BankAccounts.FirstOrDefaultAsync(a => a.AccNumber == accNumber);
        }

        public async Task<BankAccount> Update(BankAccount account)
        {
            _context.BankAccounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }


        public async Task<bool> TransferAsync(TransferRequestDto request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var fromAccount = await _context.BankAccounts.FirstOrDefaultAsync(a => a.AccNumber == request.FromAccNumber);
                var toAccount = await _context.BankAccounts.FirstOrDefaultAsync(a => a.AccNumber == request.ToAccNumber);

                if (fromAccount == null || toAccount == null)
                    return false;

                if (fromAccount.Balance < request.Amount)
                    return false;

                fromAccount.Balance -= request.Amount;
                toAccount.Balance += request.Amount;

                _context.BankAccounts.Update(fromAccount);
                _context.BankAccounts.Update(toAccount);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
        public async Task<Transaction> AddTransaction(Transaction transaction)  
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }


    }
}
