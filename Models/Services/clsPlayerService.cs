using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SagarPractical.Models;
using SagarPractical.ViewModels;

namespace SagarPractical.Models.Services
{
    public class clsPlayerService
    {
        clsPlayer objPlayer = new clsPlayer();
        public void InserPlayerDetails(clsPlayerViewModel objPlayerEntity)
        {
            if(objPlayerEntity != null)
            {
                objPlayerEntity.password = clsEncryptDecrypt.Encrypt("team1234");
                objPlayer.InsertPlayerDetails(objPlayerEntity);
            }
        }

        public int GetTotalRegistation()
        {
            return objPlayer.GetTotalRegistation();
        }

        public bool IsCoachAdded()
        {
            return objPlayer.IsCoachAdded();
        }

        public bool AuthenticatePlayerDetails(clsPlayerViewModel objPlayerEntity)
        {
            bool blnResponce = false;

            string strEmail = objPlayerEntity.email;
            string strPassword = clsEncryptDecrypt.Encrypt(objPlayerEntity.password);

            clsPlayerViewModel objPlayerDetails = new clsPlayerViewModel();

            objPlayerDetails = objPlayer.ValidatePlayerForLogin(strEmail, strPassword);

            if (objPlayerDetails != null)
            {
                blnResponce = true;

                HttpContext.Current.Session["playerid"] = objPlayerDetails.playerid;
                HttpContext.Current.Session["playerdetails"] = objPlayerDetails;

            }
            else
            {
                blnResponce = false;
            }

            return blnResponce;
        }


        public List<clsPlayerViewModel> GetAllPlayers()
        {
            return objPlayer.GetAllPlayers();
        }

        public void SelectCaption(int intPlayerId)
        {
            objPlayer.SelectCaption(intPlayerId);

        }

        public void SelectTeamMember(int intPlayerId)
        {
            objPlayer.SelectTeamMember(intPlayerId);
        }
    }
}