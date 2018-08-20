using System;
using System.IO;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces.Services;

namespace ServicePlace.Website.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Upload()
        {
            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];
                if (file == null || file.ContentLength <= 0) continue;
                var path = Path.Combine(Server.MapPath("~/Images"),
                    Path.GetFileName(file.FileName) ?? throw new InvalidOperationException());
                file.SaveAs(path);
                var uploadResult = _imageService.Upload(path);
                return Json(new { url = uploadResult }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
    }
}