using BankApp.DTOs;
using BankApp.Interfaces;
using BankApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountService _service;

        public BankAccountController(IBankAccountService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BankAccount>> CreateAccount(CreateAccountDto dto)
        {
            var account = await _service.CreateAccount(dto);
            return Ok(account);
        }

        [HttpPost("Deposit")]
        public async Task<ActionResult<BankAccount>> Deposit(string AccNumber, decimal amount)
        {
            var account = await _service.Deposit(AccNumber, amount);
            return Ok(account);
        }

        [HttpPost("Withdraw")]
        public async Task<ActionResult<BankAccount>> Withdraw(string AccNumber, decimal amount)
        {
            var account = await _service.Withdraw(AccNumber, amount);
            return Ok(account);
        }

        [HttpGet("{AccNumber}")]
        public async Task<ActionResult<BankAccount>> GetByAccNum(string AccNumber)
        {
            var account = await _service.GetByAccNum(AccNumber);
            if (account == null) return NotFound("Account not found");
            return Ok(account);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequestDto request)
        {
            var success = await _service.TransferAsync(request);
            if(!success)
                return BadRequest("Transfer failed. Check account numbers or balance.");
            return Ok("Transfer successful.");
        }


    }
}
