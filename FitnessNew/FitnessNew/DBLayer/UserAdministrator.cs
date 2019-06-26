using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class UserAdministrator
    {
        #region Details

        public class UserAdministrator_Details
        {
            #region Constructor

            public UserAdministrator_Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int AdministratorId = 0;
            public int UserId = 0;

            #endregion
        }

        #endregion

        #region Details

        public class UserAdministrator_WideDetails
        {
            #region Constructor

            public UserAdministrator_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int AdministratorId = 0;
            public int UserId = 0;

            public string AdministratorName = "";
            public string UserName = "";

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList(int id)
        {

            string sql = "SELECT ua.[Id], ua.UserId, ua.AdministratorId, a.[FIO] AS AdminName, u.FIO ";
            sql += " FROM UserAdministrator AS ua INNER JOIN Administrators AS a ON ua.[AdministratorId] = a.[Id] ";
            sql += " INNER JOIN Users AS u ON u.[Id] = ua.UserId WHERE ua.UserId = " + id.ToString();

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.UserAdministrator.UserAdministrator_WideDetails det = new DBLayer.UserAdministrator.UserAdministrator_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                //det.AdminName = dr["AdminName"].ToString();

                det.UserName = dr["FIO"].ToString();

                if (!dr.IsNull("UserId"))
                    det.UserId = Convert.ToInt32(dr["UserId"]);

                if (!dr.IsNull("AdministratorId"))
                    det.AdministratorId = Convert.ToInt32(dr["AdministratorId"]);

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region Insert

        public static void Insert(DBLayer.UserAdministrator.UserAdministrator_Details det)
        {
            string sql = "INSERT INTO UserAdministrator (UserId, AdministratorId) ";
            sql += " VALUES (" + det.UserId.ToString() + ", " + det.AdministratorId.ToString() + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion        

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM UserAdministrator WHERE [Id] = " + id.ToString());
        }

        #endregion       
    }
}
