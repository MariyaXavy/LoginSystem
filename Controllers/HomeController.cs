using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RegLogin.Entities;
using RegLogin.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Transactions;

namespace RegLogin.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}


        private readonly RegDbContext dbContext;
        public HomeController(RegDbContext dbcontext)
        {
            this.dbContext = dbcontext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(NewRegModel obj)
        {
            var regModel = new RegModel
            {
                Id = obj.Id,
                Name = obj.Name,
                Email = obj.Email,
                PhNo = obj.PhNo,
                Password = obj.Password,
                ConfirmPassword = obj.ConfirmPassword
            };
            if (ModelState.IsValid) {
                dbContext.RegTable.Add(regModel);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(LoginModel obj1)
        { 
            var reg=dbContext.RegTable.ToList();
            foreach (var item in reg)
            {
                if (item.Name == obj1.Name && item.Password == obj1.Password)
                {
                    TempData["ab"] = item.Name;
                    return RedirectToAction("Success");
                }

            }

            return View();
        }

        public IActionResult Success()
        {
            ViewBag.data= TempData["ab"] as string;
            var reg=dbContext.RegTable.ToList();
            return View(reg);
        }

        [HttpGet]
        public IActionResult Update(int Id)
        {
            var reg = dbContext.RegTable.FirstOrDefault(x=>x.Id==Id);
            if(reg != null)
            {
                var update = new UpdateModel()
                {
                    Id = reg.Id,
                    Name=reg.Name,
                    Email=reg.Email,
                    PhNo = reg.PhNo,
                    //Password =reg.Password,
                    //ConfirmPassword=reg.ConfirmPassword,
                };
                return View(update);
            }
            return RedirectToAction("Success");
        }

        [HttpPost]
        public IActionResult Update(UpdateModel obj)
        {
            var reg=dbContext.RegTable.Find(obj.Id);
            if( reg != null)
            {
                reg.Name=obj.Name;
                reg.Email=obj.Email;
                reg.PhNo=obj.PhNo;
                //reg.Password=obj.Password;
                //reg.ConfirmPassword=obj.ConfirmPassword;
                dbContext.SaveChanges();
                return RedirectToAction("Success");
            }
            return RedirectToAction("Success");
        }

        
        
        public IActionResult Delete(int Id)
        {
            var reg = dbContext.RegTable.Find(Id);
            if (reg != null)
            {
                dbContext.RegTable.Remove(reg);
                dbContext.SaveChanges();
                return RedirectToAction("Success");
            }
            return RedirectToAction("Success");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}