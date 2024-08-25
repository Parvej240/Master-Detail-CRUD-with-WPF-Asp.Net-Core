
using Meeting.Data;
using Meeting.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Customer.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MeetingDbcontext _context;
       
        public CustomerController(MeetingDbcontext context)
        {
            _context = context;

        }
        
        public IActionResult Index()
        {
            var corporates = _context.Corporates.ToList();

            List<Corporate> cl = new List<Corporate>();
            cl = (from c in _context.Corporates select c).ToList();
            cl.Insert(0, new Corporate { Id = 0, Name = "--Select Country Name--" });
            ViewBag.message = cl;


            ViewBag.Corporates = corporates;
            return View(corporates);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Corporate corporate = new Corporate();
            corporate.Experiences.Add(new Experience() { ExperienceId = 1 });
            //applicant.Experiences.Add(new Experience() { ExperienceId = 2 });
            //applicant.Experiences.Add(new Experience() { ExperienceId = 3 });
            return View(corporate);
        }
        [HttpPost]
        public IActionResult Create(Corporate corporate)
        {

            //string uniqueFileName = GetUploadedFileName(applicant);
            //applicant.PhotoUrl = uniqueFileName;

            _context.Add(corporate);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
