using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SagarPractical.ViewModels;

namespace SagarPractical.Models
{
    public class clsPlayer : clsBaseDAL
    {
        private string strQuery = string.Empty;
        private List<SqlParameter> objSqlParameter;
        public int InsertPlayerDetails(clsPlayerViewModel objPlayerEntity)
        {
            int intPlayerId = 0;

            objSqlParameter = new List<SqlParameter>();
            objSqlParameter.Add(new SqlParameter("firstname", objPlayerEntity.firstname ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("lastname", objPlayerEntity.lastname ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("totalmatchesplayed", objPlayerEntity.totalmatchesplayed ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("contactnumber", objPlayerEntity.contactnumber ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("email", objPlayerEntity.email ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("password", objPlayerEntity.password ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("dateofBirth", objPlayerEntity.dateofBirth ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("playerheight", objPlayerEntity.playerheight ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("playerweight", objPlayerEntity.playerweight ?? (object)DBNull.Value));
            objSqlParameter.Add(new SqlParameter("role", objPlayerEntity.role ?? (object)DBNull.Value));

            strQuery = @"INSERT INTO player (firstname, lastname, totalmatchesplayed, contactnumber, email, password, dateofBirth, playerheight, 
                            playerweight, role)
                            values(@firstname, @lastname, @totalmatchesplayed, @contactnumber, @email, @password, @dateofBirth, @playerheight, 
                            @playerweight, @role)";

            intPlayerId = base.ExecuteStoreCommand(strQuery, objSqlParameter, "InsertPlayerDetails", 'Y');

            return intPlayerId;
        }


        public int GetTotalRegistation()
        {
            int intTotalRegistation = 0;

            objSqlParameter = new List<SqlParameter>();
            strQuery = @"SELECT COUNT(playerid) from player WHERE ISNULL(flagdeleted, 0) = 0";

            intTotalRegistation =  base.ExecuteScalarQuery<int>(strQuery, objSqlParameter, "GetTotalRegistation");

            return intTotalRegistation;
        }

        public bool IsCoachAdded()
        {
            bool blnResult = false;
            int intResult = 0;

            objSqlParameter = new List<SqlParameter>();
            strQuery = @"SELECT COUNT(playerid) AS coach FROM player WHERE role = 1 AND ISNULL(flagdeleted, 0) = 0 ";

            intResult = base.ExecuteScalarQuery<int>(strQuery, objSqlParameter, "IsCoachAdded");

            blnResult = intResult != 0 ? true : false;

            return blnResult;
        }


        public clsPlayerViewModel ValidatePlayerForLogin(string strEmail, string strPassword)
        {
            clsPlayerViewModel objplayerDetail = new clsPlayerViewModel();

            objSqlParameter = new List<SqlParameter>();
            objSqlParameter.Add(new SqlParameter("email", strEmail));
            objSqlParameter.Add(new SqlParameter("password", strPassword));

            strQuery = @"SELECT 
                                playerid,
	                            firstname,
	                            lastname,
	                            email,
	                            contactnumber,
                                role
                            FROM 
	                            player
                            WHERE
	                            email = @email AND
	                            password = @password AND
	                            ISNULL(flagdeleted, 0) = 0";

            objplayerDetail = base.ExecuteScalarQuery<clsPlayerViewModel>(strQuery, objSqlParameter, "ValidatePlayerForLogin");

            return objplayerDetail;
        }


        public List<clsPlayerViewModel> GetAllPlayers()
        {
            List<clsPlayerViewModel> lstPlayer = new List<clsPlayerViewModel>();

            objSqlParameter = new List<SqlParameter>();

            strQuery = @"SELECT 
	                            playerid, 
	                            firstname, 
	                            lastname, 
	                            totalmatchesplayed, 
	                            contactnumber, 
	                            email, 
	                            dateofBirth, 
	                            playerheight, 
	                            playerweight, 
	                            role, 
	                            ISNULL(isselected, 0) AS isselected
                            FROM
	                            player WHERE ISNULL(flagdeleted, 0) = 0 ";

            lstPlayer = base.ExecuteStoreQuery<clsPlayerViewModel>(strQuery, objSqlParameter, "GetAllPlayers");

            return lstPlayer;
        }

        public void SelectCaption(int intPlayerId)
        {
            objSqlParameter = new List<SqlParameter>();
            objSqlParameter.Add(new SqlParameter("playerid", intPlayerId));

            strQuery = @"UPDATE player set role = 2 where playerid = @playerid";

            base.ExecuteStoreCommand(strQuery, objSqlParameter, "SelectCaption");

        }

        public void SelectTeamMember(int intPlayerId)
        {
            objSqlParameter = new List<SqlParameter>();
            objSqlParameter.Add(new SqlParameter("playerid", intPlayerId));

            strQuery = @"UPDATE player set isselected = 1 where playerid = @playerid";

            base.ExecuteStoreCommand(strQuery, objSqlParameter, "SelectTeamMember");

        }
    }
}