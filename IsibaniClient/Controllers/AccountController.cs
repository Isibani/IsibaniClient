using AutoMapper;
using IsibaniClient.Data;
using IsibaniClient.IRepository;
using IsibaniClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClientDbContext _context;
     
        private readonly IAccountRepository db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController( ClientDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IAccountRepository clientrepo)
        {
    
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            this.db = clientrepo;


        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ClientList()
        {
           
            return Json ( new {data= db.GetAll() });
        }
        //public mxolisi IActionResult ProductList()
        //{
        //    var data = new { data = _context.Products.ToList() };
        //    return new JsonResult(_context.Products.ToList());
        //}
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                if (model.ClientId <= 0)
                {


                    try
                    {
                        var role = new IdentityRole();
                        role.Name = "Admin";
                        var user = new IdentityUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };

                        var result = await _userManager.CreateAsync(user, "Password01@");
                        if (result.Succeeded)
                        {
                            var client = new Client { Name = model.Name, Address = model.Address, IdentityUser = user };
                            await _context.Clients.AddAsync(client);
                            var save = _context.SaveChangesAsync();
                            return new JsonResult(new { isvalid = true, result = "record added successfully" });
                        }
                        else
                        {
                            var errorsd = string.Join(", ", result.Errors.Select(x => x.Description));
                            return new JsonResult(new { isvalid = false, result = "record not added successfully error: " + errorsd });
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
                        var findclient = await _context.Clients.FindAsync(model.ClientId);
                        var finduser = await _userManager.FindByIdAsync(model.Id);
                        {
                            if (finduser != null && findclient != null)
                            {

                                finduser.UserName = model.Email;
                                finduser.Email = model.Email;
                                finduser.PhoneNumber = model.PhoneNumber;

                                /*{ UserName = objUser.Email, Email = objUser.Email, PhoneNumber = objUser.PhoneNumber };*/
                                var result = await _userManager.UpdateAsync(finduser);

                                if (result.Succeeded)
                                {
                                    //var client = new Client { Name = objUser.Name, Address = objUser.Address, IdentityUser = finduser };
                                    findclient.Name = model.Name;
                                    findclient.Address = model.Address;
                                    findclient.UserId = model.Id;
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
            }
          

                return new JsonResult(new { isValid = false, result = model });
        }

        [HttpGet]
        public JsonResult EditUserJ(int Id = 0)    //For getting details of the selected User  
        {
            try
            {

                var user = _context.Clients.Where(x=>x.ClientId==Id).Select(user => new RegisterViewModel
                {
                    Id = user.IdentityUser.Id,
                    ClientId = user.ClientId,
                    Email = user. IdentityUser.Email,
                    PhoneNumber = user.IdentityUser.PhoneNumber,
                    Name = user.Name,
                    Address = user.Address,
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
        [HttpPost]
        public async Task<JsonResult> EditUserJ(RegisterViewModel objUser)      //For Updating changes   
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
        public JsonResult DeleteUserJ(string Id)
        {
            try
            {
                var User1 = _context.Clients.Where(x=>x.UserId==Id).FirstOrDefault();
                var User2 = _context.Users.Find(User1.UserId);


                //fetching the user with Id   
                if (User1 != null)
                    {
              _context.Clients.Remove(User1);
               _context.Users.Remove(User2);
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
