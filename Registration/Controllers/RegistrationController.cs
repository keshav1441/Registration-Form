using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserReg.Data;
using UserReg.Models;

namespace UserReg.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                try
{
    _context.Add(user);
    _context.SaveChanges();
    ViewBag.message = "The user " + user.Username + " is saved successfully";
    return View();
}
catch (Exception ex)
{
    // Log the exception
    // For example, using a logging framework
    // _logger.LogError(ex, "Error saving user");
    ViewBag.message = "An error occurred: " + ex.Message;
    return View(user);
}

            }
            return View(user);
        }
    }
}
