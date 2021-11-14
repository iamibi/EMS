using Microsoft.AspNetCore.Mvc;
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
            ViewBag.ErrorMessage = string.Empty;

            if (ModelState.IsValid)
            {
                if (PlatformHelper.ValidateEMSUserCredentials(loginModel.Email.Trim(), loginModel.Password.Trim()))
                {
                    HttpContext.Session.SetString("username", loginModel.Email);
                    return View("Success");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Username or Password";
                }
            }
            
            return View();
        }

        public ActionResult EmployeeView()
        {
            if (ModelState.IsValid)
            {
                string emailId = HttpContext.Session.GetString("username");
                if (emailId.Trim() == string.Empty) return View("Error/Failure");

                ViewBag.EmployeeTasks = PlatformHelper.GetAllTasksForUser(emailId.Trim());
                return View();
            }
            return View("Error/Failure");
        }

        [HttpPost]
        public ActionResult EmployeeView(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                string emailId = HttpContext.Session.GetString("username");
                if (emailId.Trim() == string.Empty) return View("Error/Failure");

                // Verify the task update.
                if (PlatformHelper.UpdateTaskStatusOfUser(emailId, employeeVM))
                    return View();
            }

            return View("Error/Failure");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

        [Route("/EMS/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {code}";
            if (code == 404)
                return View("Error/PageNotFound");
            return View("Error/Failure");
        }
    }
}
