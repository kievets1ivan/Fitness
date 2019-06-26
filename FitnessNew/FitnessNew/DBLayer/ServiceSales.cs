using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class ServiceSales
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int ServiceId = 0;
            public int UserId = 0;
            public DateTime Date = DateTime.MinValue;
            public string Time = "";
            public int Quantity = 0;

            public bool IsDeleted = false;
            public DateTime DeleteDate = DateTime.MinValue;
            public string DeleteReason = "";

            #endregion
        }

        #endregion

        #region WideDetails

        public class ServiceSales_WideDetails
        {
            #region Constructor

            public ServiceSales_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int ServiceId = 0;
            public string ServiceName = "";

            public int UserId = 0;
            public string UserName = "";

            public DateTime Date = DateTime.MinValue;
            public string Time = "";
            public int Quantity = 0;

            public int DimensionId = 0;
            public string DimensionName = "";

            public double Cost = 0;

            public bool IsDeleted = false;
            public DateTime DeleteDate = DateTime.MinValue;
            public string DeleteReason = "";

            public int Type = 0;

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList(int id, DateTime date1, DateTime date2)
        {

            string sql = "SELECT ss.[Id], ss.Quantity, ss.[Date], ss.Time, s.Name AS ServiceName, ss.Quantity, d.[Name] AS DimensionName, u.FIO AS UserName, s.CostPerUnit, ss.IsDeleted, ss.DeleteDate, ss.DeleteReason, s.Type ";
            sql += " FROM  ServiceSales AS ss INNER JOIN Services AS s ON s.[Id] = ss.[ServiceId] ";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = s.DimensionId INNER JOIN Users AS u ON u.[Id] = ss.UserId";
            sql += " WHERE ss.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "'";
            sql += " ORDER BY ss.[Date]";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.ServiceSales.ServiceSales_WideDetails det = new DBLayer.ServiceSales.ServiceSales_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("Type"))
                    det.Type = Convert.ToInt32(dr["Type"]);

                if (!dr.IsNull("Date"))
                    det.Date = Convert.ToDateTime(dr["Date"]);

                if (!dr.IsNull("Quantity"))
                    det.Quantity = Convert.ToInt32(dr["Quantity"]);

                det.ServiceName = dr["ServiceName"].ToString();

                det.Time = dr["Time"].ToString();

                det.UserName = dr["UserName"].ToString();

                det.DimensionName = dr["DimensionName"].ToString();

                det.Cost = Convert.ToDouble(dr["CostPerUnit"]);

                if (!dr.IsNull("IsDeleted"))
                    det.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

                if (!dr.IsNull("DeleteDate"))
                    det.DeleteDate = Convert.ToDateTime(dr["DeleteDate"]);

                det.DeleteReason = dr["DeleteReason"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion     
        
        #region SetDelete

        public static void SetDelete(DBLayer.ServiceSales.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET IsDeleted = 1 WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET DeleteDate = '" + det.DeleteDate.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET DeleteReason = '" + det.DeleteReason + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Insert

        public static void Insert(DBLayer.ServiceSales.Details det)
        {
            string sql = "INSERT INTO ServiceSales (ServiceId, UserId, [Date], [Time], [Quantity], IsDeleted) ";
            sql += " VALUES (" + det.ServiceId.ToString() + ", " + det.UserId.ToString() + ", '" + det.Date.ToString("yyyyMMdd") + "', '" + det.Time + "', " + det.Quantity.ToString() + ", 0)";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.ServiceSales.Details det)
        {

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [ServiceId] = " + det.ServiceId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [UserId] = " + det.UserId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [Date] = '" + det.Date.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [Time] = '" + det.Time + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [Quantity] = " + det.Quantity.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM ServiceSales WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.ServiceSales.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM ServiceSales WHERE [Id] = " + id.ToString());

            DBLayer.ServiceSales.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("Date"))
                det.Date = Convert.ToDateTime(dr["Date"]);

            det.Time = dr["Time"].ToString();

            if (!dr.IsNull("Quantity"))
                det.Quantity = Convert.ToInt32(dr["Quantity"]);

            if (!dr.IsNull("ServiceId"))
                det.ServiceId = Convert.ToInt32(dr["ServiceId"]);

            if (!dr.IsNull("UserId"))
                det.UserId = Convert.ToInt32(dr["UserId"]);

            if (!dr.IsNull("IsDeleted"))
                det.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

            if (!dr.IsNull("DeleteDate"))
                det.DeleteDate = Convert.ToDateTime(dr["DeleteDate"]);

            det.DeleteReason = dr["DeleteReason"].ToString();

            return det;
        }

        #endregion        
    }
}
