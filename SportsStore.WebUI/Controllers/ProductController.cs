using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {

        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: Product
        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
}



//Kayak	A boat for one person	Watersports	275.00
//Lifejacket	Protective and fashionable	Watersports	48.55
//Soccer Ball	FIFA-Approved size and weitght	Soccer	19.50
//Coner Flags	Give your playing field a professional touch	Soccer	34.95
//Stadium	Flat-packed 35000 seat statium	Soccer	79500.00
//Thinking Cap	Improve your brain effieiency by 75%	Chess	16.00
//Unsteady Chair	give your opponent a disadvantage	Chess	29.95
//Human Chess Board	A fun game for the family	Chess	75.00
//Bling-Bling King	Gold-plated, diamond King	Chess	1200.00