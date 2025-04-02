//using Microsoft.AspNetCore.Mvc;
//using OAS_ClassLib.Models;
//using OAS_ClassLib.Repositories;

//namespace OAS_MVC.Controllers
//{
//    public class ProductController : Controller
//    {
//        private readonly ProductServices _ProductService;

//        public ProductController()
//        {
//            _ProductService = new ProductServices();
//        }


//        // GET : Product/AddProduct
//        public IActionResult AddProduct()
//        {
//            return View();
//        }


//        // POST : Product/AddProduct
//        [HttpPost]
//        public IActionResult AddProduct(Product model)
//        {
//            if (ModelState.IsValid)
//            {
//                //Product product = new Product()
//                //{
//                //    //ProductID = model.ProductID,
//                //    SellerID = model.SellerID,
//                //    Title = model.Title,
//                //    Description = model.Description,
//                //    StartPrice = model.StartPrice,
//                //    Category = model.Category,
//                //    Status = model.Status
//                //};
//                _ProductService.AddProduct(model);
//                return RedirectToAction(nameof(Index));
//            }
//            return View(model);
//        }
//        public IActionResult RemoveProduct()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult RemoveProduct(int productId)
//        {
//            _ProductService.RemoveProduct(productId);
//            return RedirectToAction("Index");
//        }

//        public IActionResult Index()
//        {
//            ProductServices ProductService = new ProductServices();
//            List<Product> obj = ProductService.GetAllProducts();
//            return View(obj);
//        }
//    }
//}
