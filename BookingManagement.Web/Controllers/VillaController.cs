using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Domain.Entities;
using BookingManagement.Infrastructure;
using BookingManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BookingManagement.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Villa addVilla)
        {
            if(addVilla.Name == addVilla.Description)
            {
                ModelState.AddModelError("Description", "Name and Description cannot be same");
            }

            if (ModelState.IsValid)
            {
                if(addVilla.Image != null)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(addVilla.Image.FileName);
                    string path = Path.Combine(webRootPath + "/images/villas/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        addVilla.Image.CopyTo(fileStream);
                    }

                    addVilla.ImageUrl = "/images/villas/" + fileName;
                }

                _unitOfWork.Villa.Add(addVilla);
                _unitOfWork.Save();
                TempData["Success"] = "Villa Created successfully";
                return RedirectToAction("Index", "Villa");
            }
            else {  return View(addVilla); }
        }

        public IActionResult Update(int? villaId)
        {
            if(villaId == null)
            {
                return RedirectToAction("Error", "Home");
            }

           var getVilla = _unitOfWork.Villa.Get(u => u.Id == villaId);

            if(getVilla == null)
            {
                return RedirectToAction("Error", "Home");
            }
            
            return View(getVilla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Villa? villaObj)
        {
            if(villaObj == null)
            {
                return NotFound();
            }

            if(ModelState.IsValid && villaObj.Id >0)
            {
                if(villaObj.Image != null)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(villaObj.Image.FileName);
                    string path = Path.Combine(webRootPath + "/images/villas", fileName);
                    if(villaObj.ImageUrl != null)
                    {
                        string imageUrl = villaObj.ImageUrl;
                        DeleteImage(webRootPath, imageUrl);
                    }
                                      

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        villaObj.Image.CopyTo(fileStream);
                    }
                   
                    villaObj.ImageUrl = "/images/villas/" + fileName;
                    
                }

                _unitOfWork.Villa.Update(villaObj);
                _unitOfWork.Save();
                TempData["Success"] = "Villa Updated successfully";
                return RedirectToAction("Index", "Villa");
            }

            return View(villaObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? villaId)
        {
            if(villaId == null)
            {
                return RedirectToAction("Error", "Home");
            }

            Villa villaObj = _unitOfWork.Villa.Get(u => u.Id == villaId);           

            if (villaObj == null)
            {
                return RedirectToAction("Error", "Home");
            }

            string webRootPath = _webHostEnvironment.WebRootPath;
            if (villaObj.ImageUrl != null)
            {
                string imageUrl = villaObj.ImageUrl;
                DeleteImage(webRootPath, imageUrl);
            }

            _unitOfWork.Villa.Delete(villaObj);
            _unitOfWork.Save();
            TempData["Success"] = "Villa deleted successfully";

            return RedirectToAction("Index", "Villa");

        }

        public void DeleteImage(string webRootPath, string? imageUrl)
        {
            string existingPath = Path.Combine(webRootPath + imageUrl);

            FileInfo file = new FileInfo(existingPath);

            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}


