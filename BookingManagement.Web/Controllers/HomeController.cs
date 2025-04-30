using BookingManagement.Application.Common.Infrastructure;
using BookingManagement.Domain.ViewModels;
using BookingManagement.Infrastructure.Repository;
using BookingManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Villa = _unitOfWork.Villa.GetAll(null, includeProperties: "Amenities"),
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                NoOfDays = 1

            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
