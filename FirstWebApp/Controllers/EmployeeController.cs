using FirstWebApp.Data;
using FirstWebApp.Models;
using FirstWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FirstWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly FirstWebAppContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailService _emailService;
        //public EmailService EmailService { get; set; }

        public EmployeeController(ILogger<EmployeeController> logger, FirstWebAppContext dbConxt
            , IWebHostEnvironment webHostEnvironment, IEmailService emailService)
        {
            _logger = logger;
            _dbContext = dbConxt;
            _webHostEnvironment = webHostEnvironment;
          _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Models.Employee> employees = Enumerable.Empty<Models.Employee>(); 
            try
            {
                employees = await _dbContext.Employee.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)
        {

            if (ModelState.IsValid)
            {
                var emailExists = await _dbContext.Employee.AnyAsync(e => e.Email == model.Email);
                if (emailExists)
                {
                    ModelState.AddModelError("Email", "An employee with this email already exists.");
                    return View(model);
                }

                if(model.Attachment is not null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetExtension(model.Attachment.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Attachment.CopyToAsync(fileStream);
                        model.PhotoPath = "/uploads/" + uniqueFileName;
                    }
                }

                await _dbContext.Employee.AddAsync(model);
                await _dbContext.SaveChangesAsync();

                _emailService.SendEmail();

                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _dbContext.Employee.FindAsync(id);
            
            if (employee == null)
                return NotFound();

            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee model)
        {
            if(!ModelState.IsValid)
                return View(model);

            if(id != model.Id)
                return BadRequest();

            //var employeeExists = await _dbContext.Employee.AnyAsync(e => e.Id == id);

            //if (!employeeExists)
            //    return NotFound();
            //_dbContext.Employee.Update(model);
            //await _dbContext.SaveChangesAsync();
            var emailExists = await _dbContext.Employee.AnyAsync(e => e.Email == model.Email && e.Id != id);
            if (emailExists)
            {
                ModelState.AddModelError("Email", "An employee with this email already exists.");
                return View(model);
            }

            try
            {
                if (model.Attachment is not null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetExtension(model.Attachment.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Attachment.CopyToAsync(fileStream);
                        model.PhotoPath = "/uploads/" + uniqueFileName;
                    }
                }

                _dbContext.Employee.Update(model);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!await _dbContext.Employee.AnyAsync(e => e.Id == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int id)
        {
            var employee = await _dbContext.Employee.FindAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _dbContext.Employee.FindAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _dbContext.Employee.FindAsync(id);

            if (employee is not null)
            {
                _dbContext.Employee.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
