using SagarPractical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SagarPractical.Models.Services;
using SagarPractical.ViewModels;

namespace SagarPractical.Controllers
{
    public class PlayerController : Controller
    {
        clsPlayerService objPlayerService = new clsPlayerService();
        clsCommonFunction objCommonFunction = new clsCommonFunction();
        public ActionResult Index()
        {
            if (!objCommonFunction.CheckPlayerSession())
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                List<clsPlayerViewModel> lstPlayer = new List<clsPlayerViewModel>();
                lstPlayer = objPlayerService.GetAllPlayers();


                int LogiPlayerRole = 0;
                bool blnPlayerSelected = false;

                clsPlayerViewModel objPlayerDetails = (clsPlayerViewModel)HttpContext.Session["playerdetails"];

                LogiPlayerRole = objPlayerDetails != null ? objPlayerDetails.role.Value : 0;

                blnPlayerSelected = lstPlayer != null ? lstPlayer.Where(x => x.playerid == objPlayerDetails.playerid).FirstOrDefault().isselected.Value : false;

                string strMsg = "";

                switch (LogiPlayerRole)
                {
                    case 1:
                        lstPlayer = lstPlayer.Where(x => x.role != LogiPlayerRole).ToList();
                        break;
                    case 2:
                        lstPlayer = lstPlayer.Where(x => x.role != LogiPlayerRole && x.role != 1).ToList();
                        break;
                    case 3:
                        strMsg = blnPlayerSelected == true ? "You are selected" : "You are not selected";
                        break;
                }

                ViewBag.PlayerDetails = objPlayerDetails;
                ViewBag.PlayerMessage = strMsg;

                return View(lstPlayer);
            }
        }

        public ActionResult SelectCaption(int playerid)
        {
            bool blnResponce = false;

            try
            {
                objPlayerService.SelectCaption(playerid);
                blnResponce = true;
            }
            catch (Exception)
            {
                blnResponce = false;
            }

            return Json(new { success = blnResponce }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectTeam(int playerid)
        {
            bool blnResponce = false;

            try
            {
                objPlayerService.SelectTeamMember(playerid);
                blnResponce = true;
            }
            catch (Exception)
            {
                blnResponce = false;
            }

            return Json(new { success = blnResponce }, JsonRequestBehavior.AllowGet);
        }
    }
}