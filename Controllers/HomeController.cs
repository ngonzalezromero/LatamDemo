using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Latam.Models;
using Latam.Infraestructure;
using Latam.Domain;
using System.Collections;
using Latam.Model;

namespace Latam.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IRepositoryFactory _factory;
        public HomeController(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            ViewBag.DataBase = Provider.DataBase;
            ViewBag.Memory = Provider.Memory;
            return View();
        }

        [Route("Add")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.DataBase = Provider.DataBase;
            ViewBag.Memory = Provider.Memory;
            return View();
        }

        [Route("Contact")]
        [HttpGet]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("CategoriesList/{source:int}")]
        [HttpGet]
        public async Task<List<CategoryModel>> CategoriesList(int source)
        {
            if (Enum.IsDefined(typeof(Provider), source))
            {
                Provider provider = (Provider)source;
                var list = await _factory.GetProvider(provider).ListAllCategoriesAsync();
                var modelist = new List<CategoryModel>();

                foreach (var item in list)
                {
                    modelist.Add(new CategoryModel().ToModel(item));
                }
                return modelist;
            }
            else
            {
                return null;
            }
        }

        [Route("OrdersList/{source:int}")]
        [HttpGet]
        public async Task<List<OrderModel>> OrdersList(int source)
        {
            if (Enum.IsDefined(typeof(Provider), source))
            {
                Provider provider = (Provider)source;
                var list = await _factory.GetProvider(provider).ListAllOrdersAsync();
                var modelist = new List<OrderModel>();

                foreach (var item in list)
                {
                    modelist.Add(new OrderModel().ToModel(item));
                }
                return modelist;
            }
            else
            {
                return null;
            }
        }

        [Route("ProductsList/{source:int}")]
        [HttpGet]
        public async Task<List<ProductModel>> ProductsList(int source)
        {
            if (Enum.IsDefined(typeof(Provider), source))
            {
                Provider provider = (Provider)source;
                var list = await _factory.GetProvider(provider).ListAllProductsAsync();
                var modelist = new List<ProductModel>();

                foreach (var item in list)
                {
                    modelist.Add(new ProductModel().ToModel(item));
                }
                return modelist;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductModel model)
        {
            bool response = false;
            string reason = "";
            try
            {
                if (model != null && ModelState.IsValid)
                {
                    if (Enum.IsDefined(typeof(Provider), model.DataSource))
                    {
                        Provider provider = (Provider)model.DataSource;
                        var database = _factory.GetProvider(provider);
                        var productDb = await database.AddProductAsync(model.ToEntitiy());

                        if (productDb != null)
                        {
                            var orderDb = await database.AddOrderAsync(model.Order.ToEntitiy(), productDb);

                            if (orderDb != null)
                            {
                                reason = "added ";
                                response = true;
                            }
                            else
                            {
                                reason = "Invalid Order ";
                            }
                        }
                        else
                        {
                            reason = "Invalid Product ";
                        }
                    }
                    else
                    {
                        reason = "Invalid Data source";
                    }
                }
                else
                {
                    reason = "Invalid Data";
                }

            }
            catch (Exception ex)
            {
                await Console.Error.WriteAsync(ex.ToString());
                reason = "Server Error";
            }
            return Json(new { response = response, reason = reason });
        }

    }
}
