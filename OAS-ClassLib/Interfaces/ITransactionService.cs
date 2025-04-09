using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Transactions;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;

namespace OAS_ClassLib.Interfaces
{
    public interface ITransactionService
    {
        void AddTransaction(Transaction transaction);
        List<Transaction> GetAllTransactions();
    }

}

