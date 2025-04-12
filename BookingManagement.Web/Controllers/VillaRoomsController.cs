using BookingManagement.Domain.Entities;
using BookingManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingManagement.Web.Controllers
{
    public class VillaRoomsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaRoomsController(ApplicationDbContext dbContext) {

            _dbContext = dbContext;

        }

        public IActionResult Index()
        {
            var villaRooms = _dbContext.VillaRooms.ToList();
            return View(villaRooms);
        }

        public IActionResult Create()
        {
            ViewBag.villaList = _dbContext.Villas.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaRooms villaRooms)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Add(villaRooms);
                _dbContext.SaveChanges();
                TempData["Success"] = "Room added successfully";
                return RedirectToAction("Index", "VillaRooms");
            }

            ViewBag.villaList = _dbContext.Villas.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }).ToList();

            return View(villaRooms);
            
        }
    }
}
