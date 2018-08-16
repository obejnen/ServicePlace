﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ServicePlace.Logic.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json.Linq;

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
        public ActionResult Upload(/*HttpPostedFileBase file*/)
        {
            foreach (string fileName in Request.Files)
            {
                var file = Request.Files[fileName];
                //Save file content goes here
                var fName = file?.FileName;
                if (file == null || file.ContentLength <= 0) continue;
                var path = Path.Combine(Server.MapPath("~/Images"),
                    Path.GetFileName(file.FileName) ?? throw new InvalidOperationException());
                file.SaveAs(path);
                _imageService.Upload(path);
                return Json(new {Message = fName});
            }

            return Json(new {Message = "Error"});
        }
    }
}