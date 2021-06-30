using IsibaniClient.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Controllers
{
    public class EmployeesController : Controller
    {
       private readonly ClientDbContext empDB;
        // GET: Home  
        public EmployeesController(ClientDbContext _empDB )
        {
            empDB = _empDB;
        }
            public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(empDB.Employees.ToList());
        }
        public JsonResult Add(Employee emp)
        {
            return Json(empDB.Add(emp));
        }
        public JsonResult GetbyID(int ID)
        {
            var Employee = empDB.Employees.ToList().Find(x => x.EmployeeID.Equals(ID));
            return Json(Employee);
        }
        public JsonResult Update(Employee emp)
        {
            return Json(empDB.Update(emp));
        }
        public JsonResult Delete(int ID)
        {
            var hh = empDB.Employees.Find(ID);
            return Json(empDB.Employees.Remove(hh));
        }
    }
}
