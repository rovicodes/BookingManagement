using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Domain.Entities;
using BookingManagement.Domain.ViewModels;
using BookingManagement.Infrastructure.Data;
using BookingManagement.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookingManagement.Web.Controllers
{
    public class VillaRoomsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaRoomsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var villaRooms = _unitOfWork.VillaRooms.GetAll(null, includeProperties: "Villa");
            return View(villaRooms);
        }

        public IActionResult Create()
        {
            VillaRoomsVM villaRoomVM = new VillaRoomsVM
            {
                VillaRooms = new VillaRooms(), // not necessary but better to have to avoid null reference error
                VillaList = GetVillaList()
            };

            return View(villaRoomVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VillaRoomsVM villaRoomsVM)
        {
            if (_unitOfWork.VillaRooms.GetAll().Any(u => u.Villa_RoomId == villaRoomsVM.VillaRooms.Villa_RoomId))
            {
                ModelState.AddModelError("VillaRooms.Villa_RoomId", "Room ID already exists.");
                villaRoomsVM.VillaList = GetVillaList();
                return View(villaRoomsVM);
            }
            if(ModelState.IsValid)
            {
                _unitOfWork.VillaRooms.Add(villaRoomsVM.VillaRooms);
                _unitOfWork.VillaRooms.Save();
                TempData["Success"] = "Villa Room added successfully";
                return RedirectToAction("Index", "VillaRooms");
            }

            return View(villaRoomsVM);
            
        }

        public IActionResult Update(int? villaRoomId)
        {
            if(villaRoomId == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var villaRooms = _unitOfWork.VillaRooms.Get(u => u.Villa_RoomId == villaRoomId);

            if(villaRooms == null)
            {
                return RedirectToAction("Error", "Home");
            }

            VillaRoomsVM villaRoomsVM = new VillaRoomsVM
            {
                VillaList = GetVillaList(),
                VillaRooms = new VillaRooms()
            };

            return View(villaRoomsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(VillaRoomsVM? villaRoomsVM)
        {
            if(villaRoomsVM == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var villaRoomExist = _unitOfWork.VillaRooms.GetAll().Any(u => u.Villa_RoomId == villaRoomsVM.VillaRooms.Villa_RoomId);

            if (villaRoomExist)
            {
                ModelState.AddModelError("VillaRooms.Villa_RoomId", "Villa room id already exist");
            }

            if(ModelState.IsValid)
            {
                _unitOfWork.VillaRooms.Update(villaRoomsVM.VillaRooms);
                _unitOfWork.VillaRooms.Save();
                TempData["Success"] = "Villa Room Updates successfully";
                return RedirectToAction("Index", "VillaRooms");
            }
            villaRoomsVM.VillaList = GetVillaList();

            return View(villaRoomsVM);

        }

        [HttpPost]
        public IActionResult Delete(int? villaRoomId)
        {
            if(villaRoomId == null)
            {
                return RedirectToAction("Error", "Home");
            }
            VillaRooms? villaRoom = _unitOfWork.VillaRooms.Get(u => u.Villa_RoomId == villaRoomId);

            _unitOfWork.VillaRooms.Delete(villaRoom);
            _unitOfWork.VillaRooms.Save();
            return RedirectToAction("Index", "VillaRooms");
        }

        private List<SelectListItem> GetVillaList()
        {
            return _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
        }
    }
}
