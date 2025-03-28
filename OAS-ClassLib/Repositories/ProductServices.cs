using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using NLog.Internal;
using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Repositories
{
    public class ProductServices
    {
        private readonly AppDbContext _context;

        public ProductServices(AppDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.Find(product.ProductID);
            if (existingProduct != null)
            {
                existingProduct.SellerID = product.SellerID;
                existingProduct.Title = product.Title;
                existingProduct.Description = product.Description;
                existingProduct.StartPrice = product.StartPrice;
                existingProduct.Category = product.Category;
                existingProduct.Status = product.Status;

                _context.SaveChanges();
            }
        }

        public void RemoveProduct(int productID)
        {
            var product = _context.Products.Find(productID);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
    }
}
