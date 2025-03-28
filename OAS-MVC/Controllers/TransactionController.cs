using Microsoft.AspNetCore.Mvc;
using OAS_ClassLib.Models;
using OAS_ClassLib.Repositories;
using System.Transactions;

namespace OAS_MVC.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionServices _transactionService;

        public TransactionController()
        {
            _transactionService = new TransactionServices();
        }

        //GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Transactions/Create
        [HttpPost]
        public IActionResult Create(OAS_ClassLib.Models.Transaction model)
        {
            if(ModelState.IsValid)
            {
                var transaction = new OAS_ClassLib.Models.Transaction
                {
                    TransactionID = model.TransactionID,
                    BuyerID = model.BuyerID,
                    AuctionID = model.AuctionID,
                    Amount = model.Amount,
                    PaymentStatus = model.PaymentStatus,
                    PaymentDate = model.PaymentDate
                };

                _transactionService.AddTransaction(transaction);
                return RedirectToAction(nameof(Index));
            }   
            return View(model);
        }
        public IActionResult Index()
        {
            TransactionServices TransactionService = new TransactionServices();
            List<OAS_ClassLib.Models.Transaction> obj = TransactionService.GetAllTransactions();
            return View(obj);
        }
    }
}
