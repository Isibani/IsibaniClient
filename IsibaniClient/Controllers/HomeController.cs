using IsibaniClient.Data;
using IsibaniClient.Repository;
using IsibaniClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IsibaniClient.IRepository;

namespace IsibaniClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientProductRepository _clientrepo;

        private readonly ClientDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ClientDbContext db, IClientProductRepository clientrepo)
        {
            _logger = logger;
            _db = db;
            _clientrepo = clientrepo;
        }

        public IActionResult Index()
        {
            return View(_clientrepo.GetAll());
        }
        public IActionResult ProductIndex()
        {
            return View();
        }
        public IActionResult ClientProductIndex()
        {
            return View();
        }
        public IActionResult ClientList()
        {
            return new JsonResult(new { isValid = false, list = _db.Users.ToList() });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult ClientProductList(int ClientId = 0)
        {
            if (ClientId == 0)
            {
                var datalist = _db.ClientProducts.Select(user => new ClientProductViewModel
                {
                    Id = user.ClientId,
                    ClientProducId = user.ClientProducId,
                    ClientName = user.Client.Name,
                    Address = user.Client.Address,
                    ProductName = user.Product.Name,
                    Type = user.Product.Type,
                    Version = user.Product.Version,
                    Description = user.Product.Description
                }).ToList();
                return Json(new { data = datalist });

            }
            else
            {
                var datalist = _db.ClientProducts.Where(x => x.ClientId == ClientId).Select(user => new ClientProductViewModel
                {
                    Id = user.ClientId,
                    ClientProducId = user.ClientProducId,
                    ClientName = user.Client.Name,
                    Address = user.Client.Address,
                    ProductName = user.Product.Name,
                    Type = user.Product.Type,
                    Version = user.Product.Version,
                    Description = user.Product.Description
                }).ToList();
                return Json(new { data = datalist });
            }

        }
        [HttpGet]
        public IActionResult GetDetails(int ClientId = 0)
        {
            if (ClientId == 0)
            {            
                return Json(new { data = "No Record Found or The unique client number doesnot exist" });
            }
            else
            {
                return Json(_clientrepo.GetDetails(ClientId));
                //return Json(new { data = _clientrepo.GetDetails(ClientId) });
            }

        }
    }
}
