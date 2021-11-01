using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Employee_Management_System.Platform;

namespace Employee_Management_System.Controllers
{
    public class EMSController : Controller
    {
        private readonly PlatformHelpers Platform = new PlatformHelpers();

        public EMSController()
        {
        }

        // GET: PlatformController
        public ActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(string emailId, string password)
        {
            if (Platform.ValidateEMSUserCredentials(emailId.Trim(), password.Trim()))
            {
                HttpContext.Session.SetString("emailId", emailId);
                return View("Success");
            }

            // Need to display error here.
            ViewBag.error = "Invalid Email or Password";
            return View("Index");
        }

        [Route("logout")]
        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("emailId");
            return View("Index");
        }

        // GET: PlatformController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlatformController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlatformController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlatformController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlatformController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlatformController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlatformController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
