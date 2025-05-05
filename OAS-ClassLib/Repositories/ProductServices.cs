using OAS_ClassLib.Models;
using Microsoft.AspNetCore.Http;
using OAS_ClassLib.Interfaces;


namespace OAS_ClassLib.Repositories
{
    public class ProductServices : IProductCrudService, IProductImageService, IProductStatisticsService, IProductQueryService
    {
        private readonly AppDbContext _context;
        private readonly string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");

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

        public Product GetProductById(int productId)
        {
            return _context.Products.Find(productId);
        }

        public async Task<List<string>> UploadImagesAsync(List<IFormFile> images, int productId)
        {
            if (images == null || images.Count == 0)
            {
                return null;
            }

            var existingImages = _context.ProductImage.Where(pi => pi.ProductId == productId).ToList();
            if (existingImages.Count + images.Count > 5)
            {
                throw new InvalidOperationException("A product can have a maximum of 5 images.");
            }

            var filePaths = new List<string>();

            foreach (var image in images)
            {
                var filePath = Path.Combine(_imageFolderPath, image.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var productImage = new ProductImage
                {
                    ProductId = productId,
                    ImageFileName = image.FileName,
                    ImageData = await File.ReadAllBytesAsync(filePath)
                };

                _context.ProductImage.Add(productImage);
                filePaths.Add(filePath);
            }

            _context.SaveChanges();

            return filePaths;
        }

        public List<FileStream> DownloadImagesByProductId(int productId)
        {
            var images = _context.ProductImage.Where(pi => pi.ProductId == productId).ToList();

            if (images == null || images.Count == 0)
            {
                return null;
            }

            var fileStreams = new List<FileStream>();

            foreach (var image in images)
            {
                var filePath = Path.Combine(_imageFolderPath, image.ImageFileName);

                if (File.Exists(filePath))
                {
                    fileStreams.Add(File.OpenRead(filePath));
                }
            }

            return fileStreams;
        }

        public IEnumerable<ProductImage> GetImagesByProductId(int productId)
        {
            return _context.ProductImage.Where(pi => pi.ProductId == productId).ToList();
        }

        public int GetProductCount()
        {
            return _context.Products.Count();
        }

        public decimal GetTotalStartPrice()
        {
            return _context.Products.Sum(p => p.StartPrice);
        }

        public decimal GetAverageStartPrice()
        {
            return _context.Products.Average(p => p.StartPrice);
        }

        public decimal GetMinStartPrice()
        {
            return _context.Products.Min(p => p.StartPrice);
        }

        public decimal GetMaxStartPrice()
        {
            return _context.Products.Max(p => p.StartPrice);
        }

        public List<object> GetProductsByCategory()
        {
            return _context.Products
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToList<object>();
        }

        public List<string> GetDistinctCategories()
        {
            return _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToList();
        }

        public List<Product> GetProductsOrderedByStartPrice()
        {
            return _context.Products.OrderBy(p => p.StartPrice).ToList();
        }

        public List<Product> GetProductsOrderedByStartPriceDesc()
        {
            return _context.Products.OrderByDescending(p => p.StartPrice).ToList();
        }
    }

}