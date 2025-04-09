using OAS_ClassLib.Models;
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
