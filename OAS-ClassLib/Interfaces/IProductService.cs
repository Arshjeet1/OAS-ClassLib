using Microsoft.AspNetCore.Http;
using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Interfaces
{
    public interface IProductCrudService
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void RemoveProduct(int productID);
        List<Product> GetAllProducts();
    }
    public interface IProductImageService
    {
        Task<string> UploadImageAsync(IFormFile image, int productId);
        FileStream DownloadImage(string fileName);
        IEnumerable<ProductImage> GetImagesByProductId(int productId);
    }
    public interface IProductStatisticsService
    {
        int GetProductCount();
        decimal GetTotalStartPrice();
        decimal GetAverageStartPrice();
        decimal GetMinStartPrice();
        decimal GetMaxStartPrice();
    }
    public interface IProductQueryService
    {
        List<object> GetProductsByCategory();
        List<string> GetDistinctCategories();
        List<Product> GetProductsOrderedByStartPrice();
        List<Product> GetProductsOrderedByStartPriceDesc();
    }
}

