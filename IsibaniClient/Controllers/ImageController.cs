using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IsibaniClient.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetBase64Image()
        {
            string path
                = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images")
                 + "\\" + "Tiger.jpg";

            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[(int)fileStream.Length];
            fileStream.Read(data, 0, data.Length);

            return Json(new { base64imgage = Convert.ToBase64String(data) }
                );
        }
    }
}
