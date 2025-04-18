using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Domain.Entities;
using BookingManagement.Infrastructure;
using BookingManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagement.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            _unitOfWork.Villa.Delete(villaObj);
            _unitOfWork.Save();
            TempData["Success"] = "Villa deleted successfully";

            return RedirectToAction("Index", "Villa");

        }
    }
}


