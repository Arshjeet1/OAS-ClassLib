using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OAS_ClassLib.Repositories
{
    public class TransactionServices
    {
        public void AddTransaction(Transaction transaction)
        {
            using(var context = new AppDbContext())
            {
                context.Transactions.Add(transaction);
                context.SaveChanges();
            }
        }

        public List<Transaction> GetAllTransactions()
        {
            using (var context = new AppDbContext())
            {
                return context.Transactions.ToList();
            }
        }
    }
}
