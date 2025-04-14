using BookingManagement.Domain.Entities;
using BookingManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagement.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var villas = _dbContext.Villas.ToList();
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
                _dbContext.Villas.Add(addVilla);
                _dbContext.SaveChanges();
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

           Villa? getVilla = _dbContext.Villas.FirstOrDefault(u => u.Id == villaId);

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
                _dbContext.Villas.Update(villaObj);
                _dbContext.SaveChanges();
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

            Villa? villaObj = _dbContext.Villas.FirstOrDefault(v => v.Id == villaId);

            if (villaObj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            _dbContext.Villas.Remove(villaObj);
            _dbContext.SaveChanges();
            TempData["Success"] = "Villa deleted successfully";

            return RedirectToAction("Index", "Villa");

        }
    }
}


