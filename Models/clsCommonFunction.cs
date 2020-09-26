using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SagarPractical.Models
{
    public class clsCommonFunction
    {
        public bool CheckPlayerSession()
        {
            bool blnResult = false;

            string strPlayerId = HttpContext.Current.Session["playerid"] != null ? Convert.ToString(HttpContext.Current.Session["playerid"]) : "";
            blnResult = !string.IsNullOrEmpty(strPlayerId) ? true : false;

            return blnResult;
        }

        public void DestroyPlayerSession()
        {
            HttpContext.Current.Session["playerid"] = "";
            HttpContext.Current.Session["playerdetails"] = null;
        }
    }
}