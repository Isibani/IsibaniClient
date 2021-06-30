using IsibaniClient.Data;
using IsibaniClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly ClientDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ProductController(ClientDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ProductList()
        {

            return Json(new { data = _context.Products.ToList() });
        }
        public async Task<ActionResult> RegisterProduct(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.ProductId <= 0)
                {

                    try
                    {
                        var client = new Product { Name = model.Name, Description = model.Description, Type = model.Type,Version=model.Version };
                        await _context.Products.AddAsync(client);
                        var save = _context.SaveChangesAsync();
                        return new JsonResult(new { isvalid = true, result = "record added successfully" });

                    }
                    catch (Exception ex)
                    {

                        return new JsonResult(new { isvalid = false, result = "record not added successfully error: " + ex.Message });
                    }

                }
                else
                {
                    try
                    {
                        var findproduct = await _context.Products.FindAsync(model.ProductId);

                        {
                            if (findproduct != null)
                            {
                                //var client = new Client { Name = objUser.Name, Address = objUser.Address, IdentityUser = finduser };
                                findproduct.Name = model.Name;
                                findproduct.Description = model.Description;
                                findproduct.Type = model.Type;
                                findproduct.Version = model.Version;
                                _context.Products.Update(findproduct);
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


            return new JsonResult(new { isValid = false, result = model });
        }

        [HttpGet]
        public JsonResult EditProduct(int Id = 0)    //For getting details of the selected User  
        {
            try
            {
                var user = (from product in _context.Products
                            where product.ProductId==Id
                            orderby product.Name
                            select new ProductViewModel
                            {
                                ProductId=product.ProductId,
                                Name = product.Name,
                                Description = product.Description,
                                Type = product.Type,
                                Version = product.Version,

                            }
                ).FirstOrDefault();

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
        [HttpPost]
        public async Task<JsonResult> EditProduct(RegisterViewModel objUser)      //For Updating changes   
        {
            try
            {
                var findclient = await _context.Clients.FindAsync(objUser.ClientId);
                var finduser = await _userManager.FindByIdAsync(objUser.Id);
                {
                    if (finduser != null && findclient != null)
                    {

                        finduser.UserName = objUser.Email;
                        finduser.Email = objUser.Email;
                        finduser.PhoneNumber = objUser.PhoneNumber;

                        /*{ UserName = objUser.Email, Email = objUser.Email, PhoneNumber = objUser.PhoneNumber };*/
                        var result = await _userManager.UpdateAsync(finduser);

                        if (result.Succeeded)
                        {
                            //var client = new Client { Name = objUser.Name, Address = objUser.Address, IdentityUser = finduser };
                            findclient.Name = objUser.Name;
                            findclient.Address = objUser.Address;
                            findclient.UserId = objUser.Id;
                            _context.Clients.Update(findclient);
                            var save = _context.SaveChangesAsync();
                            return new JsonResult(new { isvalid = true, result = "record added successfully" });
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                return null;
            }
            return new JsonResult(new { isvalid = false, result = "record not added successfully" });
        }
        [HttpPost]
        public JsonResult DeleteProduct(int Id)
        {
            try
            {
                var User1 = _context.Products.Find(Id);
                


                //fetching the user with Id   
                if (User1 != null)
                {
                    _context.Products.Remove(User1);
                    
                    _context.SaveChanges();
                    return Json(User);
                }
                return Json(null);
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
