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
    public class Sales :
        IInsertable<SalesWideDetails>,
        IUpdatable<SalesWideDetails>,
        IGettableDetailsById<SalesWideDetails>,
        IDeletable
    {
        SalesWideDetails det = new SalesWideDetails();

        #region Get List

        public static ArrayList GetList(int id, DateTime date1, DateTime date2)
        {
            string sql = "SELECT s.[Id], s.[Date], p.[Name] AS ProductName, u.FIO AS 'UserName', d.[Name] AS DimensionName, s.Quantity, s.[Time], pg.[Name] AS ProductGroupName, p.Price AS Cost, s.IsDeleted, s.DeleteDate, s.DeleteReason ";
            sql += " FROM Sales AS s INNER JOIN Products AS p ON s.ProductId = p.[Id] ";
            sql += " INNER JOIN Users AS u ON u.[Id] = s.UserId INNER JOIN ProductGroup AS pg ON pg.[Id] = p.GroupId";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = p.DimensionId";
            sql += " WHERE s.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "'";
            sql += " ORDER BY s.[Date]";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Sales.Sales_WideDetails det = new DBLayer.Sales.Sales_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("Date"))
                    det.Date = Convert.ToDateTime(dr["Date"]);

                if (!dr.IsNull("Quantity"))
                    det.Quantity = Convert.ToInt32(dr["Quantity"]);

                det.DimensionName = dr["DimensionName"].ToString();

                det.ProductGroupName = dr["ProductGroupName"].ToString();

                det.Time = dr["Time"].ToString();

                det.UserName = dr["UserName"].ToString();

                det.ProductName = dr["ProductName"].ToString();

                det.Cost = Convert.ToDouble(dr["Cost"]);

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

        public static void SetDelete(DBLayer.Sales.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Sales SET IsDeleted = 1 WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Sales SET DeleteDate = '" + det.DeleteDate.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Sales SET DeleteReason = '" + det.DeleteReason + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Insert

        public void Insert(SalesWideDetails det)
        {
            string sql = "INSERT INTO Sales (ProductId, UserId, [Date], [Time], Quantity, IsDeleted) ";
            sql += " VALUES (" + det.ProductId.ToString() + ", " + det.UserId.ToString() + ", '" + det.Date.ToString("yyyyMMdd") + "', '" + det.Time + "', " + det.Quantity.ToString() + ", 0)";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public void Update(SalesWideDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Sales SET [ProductId] = " + det.ProductId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Sales SET [Quantity] = " + det.Quantity.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Sales SET [UserId] = " + det.UserId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Sales SET [Date] = '" + det.Date.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Sales SET [Time] = '" + det.Time + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Sales WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public SalesWideDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Sales WHERE [Id] = " + id.ToString());

            SalesWideDetails det = new SalesWideDetails();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("ProductId"))
                det.ProductId = Convert.ToInt32(dr["ProductId"]);

            if (!dr.IsNull("UserId"))
                det.UserId = Convert.ToInt32(dr["UserId"]);

            if (!dr.IsNull("Quantity"))
                det.Quantity = Convert.ToInt32(dr["Quantity"]);

            if (!dr.IsNull("Date"))
                det.Date = Convert.ToDateTime(dr["Date"]);

            det.Time = dr["Time"].ToString();

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
