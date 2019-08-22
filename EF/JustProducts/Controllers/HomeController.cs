using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JustProducts.Models;

namespace JustProducts.Controllers
{
    public class HomeController : Controller
    {
        private List<Product> GetProducts()
        {
            return dbContext.Products
                .OrderByDescending(p => p.Price).ToList();
        }
        private ProductsViewModel GetProductsViewModel()
        {
            return new ProductsViewModel()
            {
                AllProducts = GetProducts()
            };
        }
        private ProductsContext dbContext;
        public HomeController(ProductsContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            return View(GetProducts());
        }
        [HttpGet("two")]
        public IActionResult IndexTwo()
        {
            return View(GetProductsViewModel());
        }
        [HttpPost("create")]
        public IActionResult Create(Product newProduct)
        {
            if(ModelState.IsValid)
            {
                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            // query for view stuff here
            return View("Index", GetProducts());
        }

        [HttpPost("create/new")]
        public IActionResult CreateTwo(ProductsViewModel model)
        {
            Product newProduct = model.NewProduct;
            if(ModelState.IsValid)
            {
                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();
                return RedirectToAction("IndexTwo");
            }
            // query for view stuff here
            return View("IndexTwo", GetProductsViewModel());
        }
    }
}
