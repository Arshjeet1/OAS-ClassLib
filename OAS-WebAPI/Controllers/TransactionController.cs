using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;
using System.Transactions;

namespace OAS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionServices _transactionServices;

        public TransactionController()
        {
            _transactionServices = new TransactionServices();
        }

        [HttpPost]
        public IActionResult AddTransaction([FromBody] OAS_ClassLib.Models.Transaction transaction)
        {
            _transactionServices.AddTransaction(transaction);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var transactions = _transactionServices.GetAllTransactions();
            return Ok(transactions);
        }
    }
}
