using System.Diagnostics;
using DataAccessLayer.SchoolRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolRecordSystem.Models;
using SharedModel.School;

namespace SchoolRecordSystem.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ILogger<SchoolController> _logger;
        private ISchoolRepository _schoolRepository;

        public SchoolController(ILogger<SchoolController> logger, ISchoolRepository schoolRepository)
        {
            _logger = logger;
            _schoolRepository = schoolRepository;
        }

        /// <summary>
        /// List Of School
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Check Login Status
            var currentUsername = HttpContext.Session.GetString("username");
            Debug.WriteLine("Username: " + currentUsername);
            if (string.IsNullOrEmpty(currentUsername))
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Please login before continuing ";
                return RedirectToAction("Index", "Login");
            }

            var schoolList = _schoolRepository.GetSchoolList();
            return View(schoolList);
        }


        /// <summary>
        /// Form To Add New School
        /// </summary>
        /// <returns></returns>
        public IActionResult AddSchool()
        {
            var currentUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(currentUsername))
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Please login before continuing ";
                return RedirectToAction("Index", "Login");
            }
            SchoolDetailsModel schoolDetailsModel = new SchoolDetailsModel();
            return View(schoolDetailsModel);
        }

        /// <summary>
        /// Add School Post Method
        /// </summary>
        /// <param name="schoolDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSchool(SchoolDetailsModel schoolDetailsModel)
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
                SchoolDetails schoolDetails = convertModelToSchoolDetails(schoolDetailsModel);
                var schoolAddResponse = _schoolRepository.AddSchool(schoolDetails);
                //TempData["Code"] = schoolAddResponse.Code;
                //TempData["Message"] = schoolAddResponse.Message;
                //return RedirectToAction("index");

                if (schoolAddResponse.Code == "0")
                {
                    TempData["Code"] = schoolAddResponse.Code;
                    TempData["Message"] = schoolAddResponse.Message;
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["Code"] = schoolAddResponse.Code;
                    TempData["Message"] = schoolAddResponse.Message;
                    return RedirectToAction("index");
                }
            }
            return View();
        }


        public IActionResult EditSchool(string id)
        {
            var currentUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(currentUsername))
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Please login before continuing ";
                return RedirectToAction("Index", "Login");
            }
            if (!string.IsNullOrEmpty(id))
            {
                var schoolDetails = _schoolRepository.GetSchoolDetails(id);
                if (!string.IsNullOrEmpty(schoolDetails.SchoolName))
                {
                    SchoolDetailsModel schoolDetailsModel = convertSchoolDetailsToModel(schoolDetails);
                    return View(schoolDetailsModel);
                }
                else
                {

                    TempData["Code"] = "2";
                    TempData["Message"] = "No details with the Id exists";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Invalid Id";
                return RedirectToAction("Index");
            }
        }

        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditSchool(SchoolDetailsModel schoolDetailsModel)
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
                SchoolDetails schoolDetails = convertModelToSchoolDetails(schoolDetailsModel);
                // var writer = new System.IO.StringWriter();
                // ObjectDumper.Dumper.Dump(schoolDetails, "Testing whether school deatils is good or not?", writer);
                // Debug.Write(writer);
                var schoolAddResponse = _schoolRepository.EditSchool(schoolDetails);
                if (!string.IsNullOrEmpty(schoolAddResponse.Code))
                {
                    Debug.WriteLine("I am inside if");
                    TempData["Code"] = schoolAddResponse.Code;
                    TempData["Message"] = schoolAddResponse.Message;

                    return RedirectToAction("Index");
                }
                TempData["Code"] = "2";
                TempData["Message"] = "Invalid Id";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        public IActionResult DeleteSchool(string Id)
        {
            var currentUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(currentUsername))
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Please login before continuing ";
                return RedirectToAction("Index", "Login");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                var SchoolDetails = _schoolRepository.DeleteSchool(Id);
                if (!string.IsNullOrEmpty(SchoolDetails.Code))
                {
                    TempData["Code"] = SchoolDetails.Code;
                    TempData["Message"] = SchoolDetails.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Code"] = "2";
                    TempData["Message"] = "No details with the Id exists";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Invalid Id";
                return RedirectToAction("Index");
            }

        }
        public IActionResult Search(SearchSchool searchSchool)
        {
            var currentUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(currentUsername))
            {
                TempData["Code"] = "2";
                TempData["Message"] = "Please login before continuing ";
                return RedirectToAction("Index", "Login");
            }
            if (searchSchool == null)
            {
                searchSchool = new SearchSchool();
            }
            if (searchSchool.Schools == null)
            {
                searchSchool.Schools = new System.Collections.Generic.List<SchoolDetails>();
            }

            if (searchSchool.SearchText != null)
            {
                searchSchool.Schools = _schoolRepository.SearchSchoolList(searchSchool.SearchText);
            }

            return View(searchSchool);
        }

        private SchoolDetailsModel convertSchoolDetailsToModel(SchoolDetails schoolDetails)
        {
            return new SchoolDetailsModel
            {
                SchoolId = schoolDetails.SchoolId,
                SchoolName = schoolDetails.SchoolName,
                SchoolAddress = schoolDetails.SchoolAddress,
                SchoolPrincipal = schoolDetails.SchoolPrincipal,
                SchoolRegistrationNumber = schoolDetails.SchoolRegistrationNumber,
                NumberOfTeachers = schoolDetails.NumberOfTeachers,
            };
        }

        private SchoolDetails convertModelToSchoolDetails(SchoolDetailsModel schoolDetailsModel)
        {
            return new SchoolDetails
            {
                SchoolId = schoolDetailsModel.SchoolId,
                SchoolName = schoolDetailsModel.SchoolName,
                SchoolAddress = schoolDetailsModel.SchoolAddress,
                SchoolPrincipal = schoolDetailsModel.SchoolPrincipal,
                SchoolRegistrationNumber = schoolDetailsModel.SchoolRegistrationNumber,
                NumberOfTeachers = schoolDetailsModel.NumberOfTeachers,
            };
        }
    }
}









