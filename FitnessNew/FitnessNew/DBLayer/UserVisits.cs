using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class UserVisits
    {
        #region Details

        public class UserVisits_Details
        {
            #region Constructor

            public UserVisits_Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int UserId = 0;
            public DateTime Date = DateTime.MinValue;
            public string TimeOn = "";
            public string TimeOff = "";

            #endregion
        }

        #endregion

        #region UserVisits_WideDetails

        public class UserVisits_WideDetails
        {
            #region Constructor

            public UserVisits_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int UserId = 0;
            public DateTime Date = DateTime.MinValue;
            public string TimeOn = "";
            public string TimeOff = "";

            public string UserFIO = "";

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList()
        {

            string sql = "SELECT uv.*, u.FIO FROM UserVisits AS uv INNER JOIN Users AS u ON u.[Id] = uv.[UserId]";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.UserVisits.UserVisits_WideDetails det = new DBLayer.UserVisits.UserVisits_WideDetails();

                det.UserId = Convert.ToInt32(dr["UserId"]);

                det.UserFIO = dr["FIO"].ToString();

                det.Date = Convert.ToDateTime(dr["Date"]);

                det.Id = Convert.ToInt32(dr["Id"]);

                det.TimeOn = dr["TimeOn"].ToString();

                det.TimeOff = dr["TimeOff"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion     
  
        #region Get List

        public static ArrayList GetList(int id)
        {

            string sql = "SELECT uv.*, u.FIO FROM UserVisits AS uv INNER JOIN Users AS u ON u.[Id] = uv.[UserId] WHERE u.[Id] = " + id.ToString();

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.UserVisits.UserVisits_WideDetails det = new DBLayer.UserVisits.UserVisits_WideDetails();

                det.UserId = Convert.ToInt32(dr["UserId"]);

                det.UserFIO = dr["FIO"].ToString();

                det.Date = Convert.ToDateTime(dr["Date"]);

                det.Id = Convert.ToInt32(dr["Id"]);

                det.TimeOn = dr["TimeOn"].ToString();

                det.TimeOff = dr["TimeOff"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion    
       
        #region Insert

        public static void Insert(DBLayer.UserVisits.UserVisits_Details det)
        {
            string sql = "INSERT INTO UserVisits (UserId, [Date], [TimeOn], [TimeOff]) ";
            sql += " VALUES (" + det.UserId.ToString() + ", '" + det.Date.Date.ToString("yyyyMMdd") + "', '" + det.TimeOn + "', '" + det.TimeOff + "')";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.UserVisits.UserVisits_Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE UserVisits SET [UserId] = " + det.UserId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserVisits SET [Date] = '" + det.Date.Date.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserVisits SET [TimeOn] = '" + det.TimeOn + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserVisits SET [TimeOff] = '" + det.TimeOff + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM UserVisits WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.UserVisits.UserVisits_Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM UserVisits WHERE [Id] = " + id.ToString());

            DBLayer.UserVisits.UserVisits_Details det = new UserVisits_Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.UserId = Convert.ToInt32(dr["UserId"]);

            det.Date = Convert.ToDateTime(dr["Date"]).Date;

            det.TimeOn = dr["TimeOn"].ToString();

            det.TimeOff = dr["TimeOff"].ToString();

            return det;
        }

        #endregion   

        #region GetLastVisit

        public static int GetLastVisit(int id)
        {
            string sql = "SELECT [Id] FROM UserVisits WHERE UserId = " + id.ToString() + " AND [TimeOff] = '' AND [Date] IN (SELECT Max([Date]) FROM UserVisits WHERE UserId = " + id.ToString() + ")";

            int ind = (int)ZFort.DB.Execute.ExecuteString_Scalar(sql);

            return ind;
        }

        #endregion

        #region SetTimeOff

        public static void SetTimeOff(int id, string time)
        {
            string sql = "UPDATE UserVisits SET TimeOff = '" + time + "' WHERE [Id] = " + id.ToString();

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion
    }
}
