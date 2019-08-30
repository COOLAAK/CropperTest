using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ContentResult UploadImage(string croppedImage)
        {
            string filePath = String.Empty;

            string base64 = croppedImage;
            byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
            Bitmap bmp;
            using (var ms = new MemoryStream(bytes))
            {
                bmp = new Bitmap(ms);
            }
            string path = Server.MapPath("/Images/");
            Bitmap imageBig = Utils.CreateImage(bmp, 360, 480);
            filePath = path + Guid.NewGuid() + ".png";
            imageBig.Save(filePath, ImageFormat.Jpeg);
            string json = JsonConvert.SerializeObject(filePath);
            return Content(json, "application/json");

        }
    }
}
