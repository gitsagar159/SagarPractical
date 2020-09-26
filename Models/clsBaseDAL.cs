using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;

namespace SagarPractical.Models
{
    public class clsBaseDAL
    {
        protected int ExecuteStoreCommand(string strQuery, List<SqlParameter> listDbParameters = null, string strFunctionName = "", char charReqLastId = 'N')
        {
            this.PrintDetailsInTrace(strQuery, listDbParameters, strFunctionName);
            using (cricketEntities objCommonEntity = new cricketEntities())
            {
                int intLastInsertedId = 0;
                if (charReqLastId == 'Y')
                {
                    strQuery += "; DECLARE @intLastld int; SET @intLastld = SCOPE_IDENTITY(); SELECT @intlastld; ";
                    if (listDbParameters == null)
                    {
                        listDbParameters = new List<SqlParameter>();
                    }
                    intLastInsertedId = objCommonEntity.Database.SqlQuery<int>(strQuery, (listDbParameters != null) ? listDbParameters.ToArray() : null).FirstOrDefault();
                    HttpContext.Current.Trace.Warn("\n " + strFunctionName + " => Lastlnsertedld " + intLastInsertedId.ToString());
                }
                else
                {
                    objCommonEntity.Database.ExecuteSqlCommand(strQuery, (listDbParameters != null) ? listDbParameters.ToArray() : null); //SqlQuery<int>(strQuery, (listDbParameters != null) ? listDbParameters.ToArray() : null);
                }
                return intLastInsertedId;
            }
        }

        protected T ExecuteScalarQuery<T>(string strQuery, List<SqlParameter> listDbParameters = null, string strFunctionName = "")
        {
            this.PrintDetailsInTrace(strQuery, listDbParameters, strFunctionName);
            using (cricketEntities objCommonEntity = new cricketEntities())
            {
                var lstResult = objCommonEntity.Database.SqlQuery<T>(strQuery, (listDbParameters != null) ? listDbParameters.ToArray() : null).FirstOrDefault();
                return lstResult;
            }
        }

        protected List<T> ExecuteStoreQuery<T>(string strQuery, List<SqlParameter> listDbParameters = null, string strFunctionName = "")
        {
            this.PrintDetailsInTrace(strQuery, listDbParameters, strFunctionName);
            using (cricketEntities objCommonEntity = new cricketEntities())
            {
                var lstResult = objCommonEntity.Database.SqlQuery<T>(strQuery, (listDbParameters != null) ? listDbParameters.ToArray() : null).ToList();
                return lstResult;

            }
        }

        protected List<T> ExecuteFunction<T>(string strQuery, List<ObjectParameter> listDbParameters = null, string strFunctionName = "", string strDB = "")
        {
            var lstResult = new List<T>();
            this.PrintObjectParaDetailsInTrace(strQuery, listDbParameters, strFunctionName);

            using (cricketEntities objCommonEntity = new cricketEntities())
            {
                lstResult = objCommonEntity.Database.SqlQuery<T>(strQuery, (listDbParameters != null) ? listDbParameters.ToArray() : null).ToList();
            }
            return lstResult;
        }

        protected void ExecuteFunction(string strQuery, List<ObjectParameter> listDbParameters = null, string strFunctionName = "", string strDB = "")
        {
            this.PrintObjectParaDetailsInTrace(strQuery, listDbParameters, strFunctionName);

            using (cricketEntities objCommonEntity = new cricketEntities())
            {
                objCommonEntity.Database.ExecuteSqlCommand(strQuery, (listDbParameters != null) ? listDbParameters.ToArray() : null);
            }
        }

        private void PrintDetailsInTrace(string strQuery, List<SqlParameter> ObjParameterList = null, string strFunctionName = "")
        {
            HttpContext.Current.Trace.Warn("\n");
            HttpContext.Current.Trace.Warn("==================== " + strFunctionName + " : Start =================");
            HttpContext.Current.Trace.Warn("\n");

            HttpContext.Current.Trace.Warn("QUERY => " + strQuery);
            HttpContext.Current.Items["Query"] = strQuery;
            if (ObjParameterList != null && ObjParameterList.Count > 0)
            {
                HttpContext.Current.Trace.Warn("\n");
                foreach (SqlParameter objParameter in ObjParameterList)
                {
                    HttpContext.Current.Trace.Warn("ParameterName " + objParameter.ParameterName);
                    HttpContext.Current.Trace.Warn("Value " + objParameter.Value);
                }
            }

            HttpContext.Current.Trace.Warn("\n");
            HttpContext.Current.Trace.Warn("   " + strFunctionName + " : End ");
        }

        private void PrintObjectParaDetailsInTrace(string strQuery, List<ObjectParameter> ObjParameterList = null, string strFunctionName = "")
        {
            HttpContext.Current.Trace.Warn("\n");
            HttpContext.Current.Trace.Warn("================ " + strFunctionName + " : Start ====================");
            HttpContext.Current.Trace.Warn("\n");
            HttpContext.Current.Trace.Warn("QUERY => " + strQuery);
            HttpContext.Current.Items["Query"] = strQuery;
            if (ObjParameterList != null && ObjParameterList.Count > 0)
            {
                HttpContext.Current.Trace.Warn("\n");
                foreach (ObjectParameter objParameter in ObjParameterList)
                {
                    HttpContext.Current.Trace.Warn("ParameterName :: " + objParameter.Name);
                    HttpContext.Current.Trace.Warn("Value :: " + objParameter.Value);
                }
            }
            HttpContext.Current.Trace.Warn("\n");
            HttpContext.Current.Trace.Warn("===================== " + strFunctionName + " ::: End ====================");
        }

    }
}