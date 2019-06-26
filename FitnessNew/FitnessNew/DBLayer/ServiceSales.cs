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
    public class ServiceSales :
        IInsertable<ServiceSalesWideDetails>,
        IUpdatable<ServiceSalesWideDetails>,
        IGettableDetailsById<ServiceSalesWideDetails>,
        IDeletable
    {
        ServiceSalesWideDetails det = new ServiceSalesWideDetails();

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

        public void Insert(ServiceSalesWideDetails det)
        {
            string sql = "INSERT INTO ServiceSales (ServiceId, UserId, [Date], [Time], [Quantity], IsDeleted) ";
            sql += " VALUES (" + det.ServiceId.ToString() + ", " + det.UserId.ToString() + ", '" + det.Date.ToString("yyyyMMdd") + "', '" + det.Time + "', " + det.Quantity.ToString() + ", 0)";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public void Update(ServiceSalesWideDetails det)
        {

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [ServiceId] = " + det.ServiceId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [UserId] = " + det.UserId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [Date] = '" + det.Date.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [Time] = '" + det.Time + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServiceSales SET [Quantity] = " + det.Quantity.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM ServiceSales WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public ServiceSalesWideDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM ServiceSales WHERE [Id] = " + id.ToString());

            ServiceSalesWideDetails det = new ServiceSalesWideDetails();

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
