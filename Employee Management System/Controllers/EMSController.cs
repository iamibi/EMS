using Microsoft.AspNetCore.Mvc;
using Employee_Management_System.Models;
using Employee_Management_System.Platform;
using Employee_Management_System.Constants;
using Microsoft.AspNetCore.Http;

namespace Employee_Management_System.Controllers
{
    public class EMSController : Controller
    {
        private readonly PlatformHelpers PlatformHelper = new PlatformHelpers();

        public IActionResult Index()
        {
            string emailId = HttpContext.Session.GetString("username");
            ViewBag.username = emailId;
            EMSUser user = PlatformHelper.GetUser(emailId);
            if (user == null && emailId == null) return View();
            else if (user.Role == EMSUserRoles.Employee.ToString()) return EmployeeView();
            else if (user.Role == EMSUserRoles.Manager.ToString()) return ManagerView();
            else if (user.Role == EMSUserRoles.IT_Department.ToString()) return ITDepartmentView();

            return View("Error/PageNotFound");
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
                        return View("RegisterSuccessful");
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
                string emailId = loginModel.Email.Trim();
                string password = loginModel.Password.Trim();

                if (PlatformHelper.ValidateEMSUserCredentials(emailId, password))
                {
                    HttpContext.Session.SetString("username", emailId);

                    EMSUser user = PlatformHelper.GetUser(emailId);
                    if (user == null) return View("Error/Failure");
                    if (user.Role == EMSUserRoles.Employee.ToString()) return EmployeeView();
                    else if (user.Role == EMSUserRoles.Manager.ToString()) return ManagerView();
                    else if (user.Role == EMSUserRoles.IT_Department.ToString()) return ITDepartmentView();
                    else return View("Error/PageNotFound");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Username or Password";
                }
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult EmployeeView()
        {
            if (ModelState.IsValid)
            {
                string emailId = HttpContext.Session.GetString("username").Trim();
                if (emailId == string.Empty) return View("Error/Failure");

                ViewBag.EmployeeTasks = PlatformHelper.GetAllTasksForUser(emailId);
                return View("EmployeeView");
            }
            return View("Error/Failure");
        }

        [HttpPost]
        public ActionResult EmployeeView(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                string emailId = HttpContext.Session.GetString("username").Trim();
                if (emailId == string.Empty) return View("Error/Failure");

                // Verify the task update.
                if (PlatformHelper.UpdateTaskStatusOfUser(emailId, employeeVM))
                {
                    ViewBag.EmployeeTask = PlatformHelper.GetAllTasksForUser(emailId);
                    return EmployeeView();
                }
            }

            return View("Error/Failure");
        }

        [HttpGet]
        public ActionResult ManagerView()
        {
            if (!ModelState.IsValid) return View("Error/Faliure");

            string emailId = HttpContext.Session.GetString("username").Trim();
            if (emailId == string.Empty) return View("Error/Failure");

            var userWithCompletedTaskCount = PlatformHelper.GetCompletedTaskCount(emailId);
            if (userWithCompletedTaskCount == null) return View("Error/PageNotFound");
            ViewBag.usersTasks = userWithCompletedTaskCount;

            return View("ManagerView");
        }

        [HttpPost]
        public ActionResult ManagerView(ManagerViewModel managerVM)
        {
            if (!ModelState.IsValid) return View("Error/Failure");
            
            return View();
        }

        public ActionResult AddUserToManager()
        {
            string managerEmailId = HttpContext.Session.GetString("username");
            ViewBag.usersList = PlatformHelper.GetAvailableUsers(managerEmailId);
            return View("AddUserToManager");
        }

        [HttpPost]
        public ActionResult AddUserToManager(string emailId)
        {
            string managerEmailId = HttpContext.Session.GetString("username");
            if (emailId != null && emailId.Trim() != string.Empty && managerEmailId != null && managerEmailId.Trim() != string.Empty)
            {
                PlatformHelper.AddUserForManager(managerEmailId, emailId);
                HttpContext.Session.SetString("first_name", emailId);
                return View("RegisterSuccessful");
            }

            return View("Error/PageNotFound");
        }

        [HttpGet]
        public ActionResult ITDepartmentView()
        {
            if (!ModelState.IsValid) return View("Error/Failure");
            string emailId = HttpContext.Session.GetString("username");
            if (!PlatformHelper.IsAdmin(emailId)) return View("Error/Failure");

            var users = PlatformHelper.GetAllUsers(emailId);
            ViewBag.Users = users;
            return View("ITDepartmentView");
        }

        [HttpPost]
        public ActionResult ITDepartmentView(string emailId)
        {
            if (!ModelState.IsValid) return View("Error/Failure");
            string adminEmailId = HttpContext.Session.GetString("username");
            if (!PlatformHelper.ValidateUser(emailId) || !PlatformHelper.ValidateUser(adminEmailId)) return View("Error/Failure");

            PlatformHelper.RemoveUser(adminEmailId, emailId);
            ViewBag.EmailId = emailId;
            return View("AccountRemoved");
        }

        [HttpGet]
        public ActionResult AccountRemoved()
        {
            return View();
        }

        public ActionResult Logout()
        {
            foreach(var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            HttpContext.Session.Clear();
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
