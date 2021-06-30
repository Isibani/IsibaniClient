using IsibaniClient.Data;
using IsibaniClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Controllers
{
    public class ClientProductController : Controller
    {
        private readonly ClientDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ClientProductController(ClientDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult GetAll()
        {
            var obj1 = _context.Clients.ToList();
            var obj2 = _context.Products.ToList();
            return new JsonResult(new { isValid = false, clients = obj1, products = obj2 });
        }
        public IActionResult ClientProductList(int Id=0)
        {
            if(Id==0)
            {
                var datalist = _context.ClientProducts.Select(user => new ClientProductViewModel
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
                var datalist = _context.ClientProducts.Where(x=>x.ClientId==Id).Select(user => new ClientProductViewModel
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

        //public IActionResult Index()
        //{
        //    var obj = new ClientProductViewModel
        //    {

        //    Clients = _context.Clients.Select(x => new SelectListItem() { Value = x.IdentityUser.Id.ToString(), Text = x.Name })
        //                      .ToList(),
        //        Products = _context.Products.Select(x => new SelectListItem() { Value = x.ProductId.ToString(), Text = x.Name })
        //                      .ToList(),
        //    };
        //    return View(obj);
        //}
       
        public async Task<ActionResult> ClientProductRegister(int ClientProductId, int ClientId, int ProductId,int estimate,int amount,int duration)
        {
            if (ClientId == -1 || ProductId == -1)
            {
                return new JsonResult(new { isvalid = false, result = "Please Select Client and product:" });
            }
            else if (ClientProductId <= 0 && ClientId > 0 || ProductId > 0)
            {
                try
                {
                    var check = _context.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).FirstOrDefault();
                    if (check == null)
                    {
                        try
                        {
                            var clientproduct = new ClientProduct { ClientId = ClientId, ProductId = ProductId,Estimatedbudget=estimate,
                            Amountspent=amount,Duration=duration};
                            await _context.ClientProducts.AddAsync(clientproduct);
                            var save =await _context.SaveChangesAsync();
                            return new JsonResult(new { isvalid = true, result = "record added successfully" });
                        }
                        catch(Exception ex)
                        {
                           
                            return new JsonResult(new { isvalid = true, result = ex.Message });
                        }
                       
                    }
                    else
                    {

                        return new JsonResult(new { isvalid = false, result = "record not added successfully error: Client & Product Already Exist" });
                    }
                }
                catch (Exception ex)
                {
                    return new JsonResult(new { isvalid = false, result = ex.Message });
                }
            }
            else
            {
                try
                {
                    var findclient = await _context.ClientProducts.FindAsync(ClientProductId);

                    {
                        if (findclient != null && ClientId > 0 && ProductId > 0)
                        {
                            //var client = new Client { Name = objUser.Name, Address = objUser.Address, IdentityUser = finduser };
                            findclient.ClientId = ClientId;
                            findclient.ProductId = ProductId;
                            findclient.Estimatedbudget = estimate;
                            findclient.Amountspent = amount;
                            findclient.Duration = duration;
                            _context.ClientProducts.Update(findclient);
                            var save = _context.SaveChangesAsync();
                            return new JsonResult(new { isvalid = true, result = "record added successfully" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                return new JsonResult(new { isvalid = false, result = "record not added successfully" });
            }
        }

        [HttpGet]
        public JsonResult EditClientProduct(int Id = 0)    //For getting details of the selected User  
        {
            try
            {
                var user = _context.ClientProducts.Where(x=>x.ClientProducId==Id).Select(user => new ClientProductViewModel
                {
                    Id=user.ClientId,
                    ClientProducId = user.ClientProducId,
                    ClientId = user.ClientId,
                    ProductId = user.ProductId,
                    ClientName = user.Client.Name,
                    Address = user.Client.Address,
                    ProductName = user.Product.Name,
                    Type = user.Product.Type,
                    Version = user.Product.Version,
                    Description = user.Product.Description,
                    Estimatedbudget=user.Estimatedbudget,
                    Amountspent=user.Amountspent,
                    Duration=user.Duration
                }).FirstOrDefault();

                if (user != null)
                {


                    return Json(user);
                }
                return Json(null);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
