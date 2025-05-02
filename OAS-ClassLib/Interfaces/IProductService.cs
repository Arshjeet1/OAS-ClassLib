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
        Product GetProductById(int productId); // Add this method here
    }


    public interface IProductImageService
    {
        Task<List<string>> UploadImagesAsync(List<IFormFile> images, int productId);
        List<FileStream> DownloadImagesByProductId(int productId);
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

