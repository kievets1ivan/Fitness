using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Library.Data;
using Library.Logic;

namespace FitnessProject.DBLayer
{
    public class UserAdministrator :
        IInsertable<UserAdministratorWideDetails>,
        IDeletable
    {
        UserAdministratorWideDetails det = new UserAdministratorWideDetails();

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

        public void Insert(UserAdministratorWideDetails det)
        {
            string sql = "INSERT INTO UserAdministrator (UserId, AdministratorId) ";
            sql += " VALUES (" + det.UserId.ToString() + ", " + det.AdministratorId.ToString() + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion        

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM UserAdministrator WHERE [Id] = " + id.ToString());
        }

        #endregion       
    }
}
