using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;
using System.Transactions;

namespace OAS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        //private readonly TransactionServices _transactionServices;

        //public TransactionController()
        //{
        //    _transactionServices = new TransactionServices();
        //}
        private readonly ITransactionService _transactionServices;

        // Constructor Injection
        public TransactionController(ITransactionService transactionServices)
        {
            _transactionServices = transactionServices ?? throw new ArgumentNullException(nameof(transactionServices));
        }

        [HttpPost]

        public IActionResult AddTransaction([FromBody] OAS_ClassLib.Models.Transaction transaction)
        {
            _transactionServices.AddTransaction(transaction);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetAllTransactions()
        {
            var transactions = _transactionServices.GetAllTransactions();
            return Ok(transactions);
        }
    }
}
