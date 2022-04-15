using BusinessLayer.RegistrationBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SchoolRecordSystem.Models;
using SharedModel.Registration;

namespace SchoolRecordSystem.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationBusiness _registrationBusiness;
        private readonly IConfiguration _configuration;

        public RegistrationController(IRegistrationBusiness registrationBusiness, IConfiguration configuration)
        {
            _registrationBusiness = registrationBusiness;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            TempData.Keep();
            var userName = HttpContext.Session.GetString("username");
            var userList = _registrationBusiness.GetRegisteredUserList(userName);
            return View(userList);
        }


        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(RegistrationDetailsModel registrationDetailsModel)
        {
            if (ModelState.IsValid)
            {
                RegistrationDetails registrationDetails = new RegistrationDetails
                {
                    Username = registrationDetailsModel.Username,
                    Password = registrationDetailsModel.Password,
                    CreatedBy = HttpContext.Session.GetString("username")
                };
                var response = _registrationBusiness.RegisterUser(registrationDetails);
                {
                    TempData["Code"] = response.Code;
                    TempData["Message"] = response.Message;
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public IActionResult ResetPassword()
        {
            var currentUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(currentUsername))
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Please login before continuing ";
                return RedirectToAction("Index", "Login");
            }
            var resetPasswordDetails = new ResetPasswordDetails();
            return View(resetPasswordDetails);
            /*  return View();
              var userName = HttpContext.Session.GetString("username");
              var password = _configuration["DefaultPassword"];
              var response = _registrationBusiness.ChangePassword(userName, id, password);
              TempData["Code"] = response.Code;
              TempData["Message"] = response.Message;
              return RedirectToAction("Index");*/
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordDetails resetPasswordDetails)
        {
            var currentUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(currentUsername))
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Please login before continuing ";
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                if (resetPasswordDetails.NewPassword == resetPasswordDetails.ConfirmNewPassword)
                {
                    var response = _registrationBusiness.ResetPassword(currentUsername, resetPasswordDetails);
                    {
                        TempData["Code"] = response.Code;
                        TempData["Message"] = response.Message;

                        if (response.Code == "0")
                        {
                            HttpContext.Session.Remove("username");
                            HttpContext.Session.Remove("isLoggedIn");
                            return RedirectToAction("index", "Login");
                        }
                        return View();
                    }
                }
                else
                {
                    TempData["Code"] = "2";
                    TempData["Message"] = "Your new password and confirm password didn't match, please try again.";
                    return View();
                }
            }
            return View();
        }
    }
}
