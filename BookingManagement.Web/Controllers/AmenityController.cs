using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Application.Common.Utility;
using BookingManagement.Domain.Entities;
using BookingManagement.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingManagement.Web.Controllers
{
    [Authorize(Roles = AppConstants.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenities = _unitOfWork.Amenities.GetAll(null, includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityVM amenityVM = new AmenityVM
            {
                Amenity = new Amenity(),
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }).ToList()

            };

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Create(AmenityVM amenityVMObj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Amenities.Add(amenityVMObj.Amenity);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Amenity");

            }

            amenityVMObj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();

            return View(amenityVMObj);
        }

        public IActionResult Update(int? amenityId)
        {
            AmenityVM amenityVM = new AmenityVM
            {
                Amenity = _unitOfWork.Amenities.Get(u => u.Id == amenityId),
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }).ToList()
            };

            return View(amenityVM);

        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenities.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Amenity");
            }

            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(amenityVM);

        }

        [HttpPost]
        public IActionResult Delete(int? amenityId)
        {
            if (amenityId != null)
            {
                var amenityToDelete = _unitOfWork.Amenities.Get(u => u.Id == amenityId);
                _unitOfWork.Amenities.Delete(amenityToDelete);
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", "Amenity");
        }

    }
}
