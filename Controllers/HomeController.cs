using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SagarPractical.Models;
using SagarPractical.Models.Services;
using SagarPractical.ViewModels;

namespace SagarPractical.Controllers
{
    
    public class HomeController : Controller
    {
        clsPlayerService objPlayerService = new clsPlayerService();
        clsCommonFunction objCommonFunction = new clsCommonFunction();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(clsPlayerViewModel objPlayerEntity)
        {
            if (objPlayerEntity != null && !string.IsNullOrEmpty(objPlayerEntity.email) && !string.IsNullOrEmpty(objPlayerEntity.password))
            {
                if (objPlayerService.AuthenticatePlayerDetails(objPlayerEntity))
                {
                    return RedirectToAction("Index", "Player");
                }
                else
                {
                    return View();
                }

            }
            else
            {

                return View();
            }

        }


        [HttpGet]
        public ActionResult Register()
        {
            int intRegistrationCount = 0;
            bool blnCoach = false;

            intRegistrationCount = objPlayerService.GetTotalRegistation();
            blnCoach = objPlayerService.IsCoachAdded();

            ViewBag.RegistationCount = intRegistrationCount;
            ViewBag.CoachAdded = blnCoach;

            return View();
        }

        [HttpPost]
        public ActionResult Register(clsPlayerViewModel objPlayerEntity)
        {

            int intRegistrationCount = 0;
            bool blnCoach = false;

            intRegistrationCount = objPlayerService.GetTotalRegistation();
            blnCoach = objPlayerService.IsCoachAdded();

            ViewBag.RegistationCount = intRegistrationCount;
            ViewBag.CoachAdded = blnCoach;

            
            try
            {
                if (objPlayerEntity != null)
                {
                    objPlayerService.InserPlayerDetails(objPlayerEntity);
                }
            }
            catch (Exception)
            {
                
            }

            ModelState.Clear();

            return View();
        }

        public ActionResult Logout()
        {
            objCommonFunction.DestroyPlayerSession();
            return RedirectToAction("Index", "Home");
        }
    }
}