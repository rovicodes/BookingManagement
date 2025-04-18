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
        private readonly IVillaRoomsRepository _villaRoomRepo;
        private readonly IVillaRepository _villaRepo;

        public VillaRoomsController(IVillaRoomsRepository villaRoomRepo, IVillaRepository villaRepo)
        {
            _villaRoomRepo = villaRoomRepo;
            _villaRepo = villaRepo;
        }

        public IActionResult Index()
        {
            var villaRooms = _villaRoomRepo.GetAll(null, includeProperties: "Villa");
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
            if (_villaRoomRepo.GetAll().Any(u => u.Villa_RoomId == villaRoomsVM.VillaRooms.Villa_RoomId))
            {
                ModelState.AddModelError("VillaRooms.Villa_RoomId", "Room ID already exists.");
                villaRoomsVM.VillaList = GetVillaList();
                return View(villaRoomsVM);
            }
            if(ModelState.IsValid)
            {
                _villaRoomRepo.Add(villaRoomsVM.VillaRooms);
                _villaRoomRepo.Save();
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
            var villaRooms = _villaRoomRepo.Get(u => u.Villa_RoomId == villaRoomId);

            if(villaRooms == null)
            {
                return RedirectToAction("Error", "Home");
            }

            VillaRoomsVM villaRoomsVM = new VillaRoomsVM
            {
                VillaList = GetVillaList(),
                VillaRooms = villaRooms
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

            var villaRoomExist = _villaRoomRepo.GetAll().Any(u => u.Villa_RoomId == villaRoomsVM.VillaRooms.Villa_RoomId);

            if (villaRoomExist)
            {
                ModelState.AddModelError("VillaRooms.Villa_RoomId", "Villa room id already exist");
            }

            if(ModelState.IsValid)
            {
                _villaRoomRepo.Update(villaRoomsVM.VillaRooms);
                _villaRoomRepo.Save();
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
            VillaRooms? villaRoom = _villaRoomRepo.Get(u => u.Villa_RoomId == villaRoomId);

           _villaRoomRepo.Delete(villaRoom);
            _villaRoomRepo.Save();
            return RedirectToAction("Index", "VillaRooms");
        }

        private List<SelectListItem> GetVillaList()
        {
            return _villaRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();
        }
    }
}
