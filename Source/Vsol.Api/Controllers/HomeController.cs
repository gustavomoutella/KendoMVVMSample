using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vsol.Api.VSolTables.Domain.Services;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Vsol.Api.VSolTables.Domain.Commands.Inputs;
using Microsoft.AspNetCore.Authorization;
using Vsol.Api.Shared.Domain;
using Vsol.Api;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApplicationService productApp;

        public HomeController(IProductApplicationService productApp)
        {
            this.productApp = productApp;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Policies.VSol_Product_Select)]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Policies.VSol_Product_Select)]
        public MemoryStream GetProducts()
        {
            return GetAllProducts();
        }

        private MemoryStream GetAllProducts()
        {
            var products = productApp.GetAsync().Result;

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(products);
            var ret = (new MemoryStream(Encoding.UTF8.GetBytes(jsonString)));

            return ret;
        }

        //[Authorize(Policies.VSol_Product_Save)]
        [HttpPost]
        public ActionResult Create([FromBody]IEnumerable<InsertProductCommand> products)
        {
            NotificationResult retorno = null;

            foreach (var item in products)
            {
                retorno = productApp.InsertAsync(item).Result;

                if (!retorno.IsValid)
                {
                    Ok(retorno);
                }
            }

            return Ok(retorno);
        }

        [HttpPost]
        public ActionResult Update([FromBody]IEnumerable<UpdateProductCommand> products)
        {
            NotificationResult retorno = null;

            foreach (var item in products)
            {
                retorno = productApp.UpdateAsync(item).Result;

                if (!retorno.IsValid)
                {
                    Ok(retorno);
                }
            }

            return Ok(retorno);
        }

        //[HttpPost]
        //public ActionResult Create1([FromBody]dynamic body)
        //{
        //    return null;
        //}

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
