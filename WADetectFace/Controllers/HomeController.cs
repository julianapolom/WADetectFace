using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WADetectFace.Services;

namespace WADetectFace.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile(HttpPostedFileBase file)
        {
            List<string> Agelist = new List<string>();

            try
            {
                //Obtener el primer archivo seleccionado
                HttpPostedFileBase httpFileCollectionBase = Request.Files?[0];

                //Obtener el objeto stream del archivo 
                using (Stream stream = httpFileCollectionBase.InputStream)
                {
                    Agelist = await new ServiceFace().DetectarRostro(stream);
                }

                return Json(Agelist);
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return null;
            }
        }
    }
}