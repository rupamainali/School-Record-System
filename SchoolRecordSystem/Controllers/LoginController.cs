using BusinessLayer.LoginBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRecordSystem.Models;
using SharedModel.Login;

namespace SchoolRecordSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginBusiness _loginBusiness;

        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        /// <summary>
        /// Login View
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            TempData.Keep();
            return View();
        }

        /// <summary>
        /// Login In User Post Method
        /// </summary>
        /// <param name="loginDetailsModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginUser(LoginDetailsModel loginDetailsModel)
        {
            if (ModelState.IsValid)
            {
                LoginDetails loginDetails = new LoginDetails();
                loginDetails.Username = loginDetailsModel.Username;
                loginDetails.Password = loginDetailsModel.Password;
                var loginDetailsResponse = _loginBusiness.LoginUser(loginDetails);
                if (loginDetailsResponse.Code == "0")
                {
                    HttpContext.Session.SetString("username", loginDetailsResponse.Username);
                    return RedirectToAction("Index", "School");
                }
                else
                {
                    TempData["Code"] = loginDetailsResponse.Code;
                    TempData["Message"] = loginDetailsResponse.Message;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Code"] = "1";
                TempData["Message"] = "Please fill up the form with proper values";
                return RedirectToAction("Index");
            }
        }



        /// <summary>
        /// Logout of the system
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("isLoggedIn");
            TempData["Code"] = "0";
            TempData["Message"] = "Logged Out Successfully";
            return RedirectToAction("index");
        }
    }
}
