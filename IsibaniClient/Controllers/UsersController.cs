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
    public class UsersController : Controller
    {
        private readonly ClientDbContext empDB;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(ClientDbContext _empDB, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            empDB = _empDB;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [BindProperty]
        public RegisterViewModel Newmodel { get; set; }

       
        public IActionResult Index()
        {
            var userVM = empDB.Clients.Select(user => new RegisterViewModel
            {
                Id = user.IdentityUser.Id,
                Email = user.IdentityUser.Email,
                Address = user.Address,
                ClientId=user.ClientId,
            }).ToList();
            Newmodel = new RegisterViewModel();
            var list = new List<RegisterViewModel>();

           
            return View(userVM);
        }
        [HttpPost]
        public JsonResult CreateUser(RegisterViewModel objUser)         //objUser is object which should be same as in javascript function  
        {
            try
            {            
                        //returning user to javacript  
                 return Json(objUser);   
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        [HttpGet]
        public JsonResult EditUserJ(int Id=0)    //For getting details of the selected User  
        {
            try
            {
                var user = (from Client in empDB.Clients
                           join IdentityUser in empDB.Users
                           on Client.UserId equals IdentityUser.Id
                           orderby Client.Name
                           select new RegisterViewModel
                           {

                                 Id = IdentityUser.Id,
                               ClientId = Client.ClientId,
                               Email = IdentityUser.Email,
                               PhoneNumber=IdentityUser.PhoneNumber,
                               Name = Client.Name,
                               Address = Client.Address,
                               
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
        public async Task<JsonResult> EditUserJ(RegisterViewModel objUser)      //For Updating changes   
        {
            try
            {
                var findclient = await empDB.Clients.FindAsync(objUser.ClientId);
                var finduser = await _userManager.FindByIdAsync(objUser.Id);
                {
                    if(finduser!=null && findclient!=null)
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
                            empDB.Clients.Update(findclient);
                            var save = empDB.SaveChangesAsync();
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
}
