﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Models;
using Employee_Management_System.Platform;
using Microsoft.AspNetCore.Http;

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
                try
                {
                    if (PlatformHelper.RegisterNewUser(registerModel))
                    {
                        HttpContext.Session.SetString("first_name", registerModel.FirstName);
                        return RedirectToAction("RegisterSuccessful");
                    }
                }
                catch
                {
                    return View("Error/Failure");
                }
            }
            return View("Error/Failure");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                if (PlatformHelper.ValidateEMSUserCredentials(loginModel.Email.Trim(), loginModel.Password.Trim()))
                {
                    HttpContext.Session.SetString("username", loginModel.Email);
                    return View("Success");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}
