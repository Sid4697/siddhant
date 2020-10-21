using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Student student)
        {
           
            string token = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:51384/");
                var postData = httpclient.PostAsJsonAsync<Student>("api/Authentication/AuthenicateUser", student);
                var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    token = await res.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    if (token != null)
                    {
                        return RedirectToAction("Index", "dashboardPage");
                    }

                }
            }
            return View("Login");
        }
    }
}
