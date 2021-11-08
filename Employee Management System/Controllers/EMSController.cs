using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Models;
using Employee_Management_System.Platform;

namespace Employee_Management_System.Controllers
{
    public class EMSController : Controller
    {
        private readonly PlatformHelpers PlatformHelper = new PlatformHelpers();

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRegisterDetails(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                PlatformHelper.RegisterNewUser(registerModel);
            }
            return View();
        }

        public ActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                PlatformHelper.ValidateEMSUserCredentials(loginModel.Email.Trim(), loginModel.Password.Trim());
            }
            return View();
        }
    }
}
