using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class ArrivalDetails
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int ArrivalId = 0;
            public int ProductId = 0;
            public double Price = 0;
            public double Quantity = 0;

            public DateTime Date = DateTime.MinValue;
            public int SupplierId = 0;

            public int ChargeId = 0;

            #endregion
        }

        #endregion

        #region Details

        public class ArrivalDetails_WideDetails
        {
            #region Constructor

            public ArrivalDetails_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int ArrivalId = 0;
            public string ArrivalNumber = "";

            public int ProductId = 0;
            public string ProductName = "";

            public double Price = 0;
            public double Quantity = 0;

            public string GroupName = "";
            public string DimensionName = "";

            public DateTime Date = DateTime.MinValue;
            public int SupplierId = 0;

            public string SupplierName = "";

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList(int id, DateTime date1, DateTime date2)
        {
            string sql = "SELECT a.*, p.[Name], d.[Name] AS DimensionName, pg.[Name] AS GroupName, ";
            sql += " s.[Name] AS SupplierName  FROM ArrivalDetails AS a INNER JOIN Products AS p ON a.ProductId = p.[Id]  ";
            sql += " INNER JOIN Suppliers AS s ON s.[Id] = a.[SupplierId]  ";
            sql += " INNER JOIN ProductGroup AS pg ON pg.[Id] = p.GroupId  ";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = p.DimensionId ";
            sql += " WHERE a.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' ";
            sql += " ORDER BY [Date]";


            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.ArrivalDetails.ArrivalDetails_WideDetails det = new DBLayer.ArrivalDetails.ArrivalDetails_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("ArrivalId"))
                    det.ArrivalId = Convert.ToInt32(dr["ArrivalId"]);

                if (!dr.IsNull("Price"))
                    det.Price = Convert.ToDouble(dr["Price"]);

                if (!dr.IsNull("ProductId"))
                    det.ProductId = Convert.ToInt32(dr["ProductId"]);

                det.ProductName = dr["Name"].ToString();

                if (!dr.IsNull("Quantity"))
                    det.Quantity = Convert.ToDouble(dr["Quantity"]);

                det.DimensionName = dr["DimensionName"].ToString();

                det.GroupName = dr["GroupName"].ToString();
                det.DimensionName = dr["DimensionName"].ToString();

                if (!dr.IsNull("SupplierId"))
                    det.SupplierId = Convert.ToInt32(dr["SupplierId"]);

                if (!dr.IsNull("Date"))
                    det.Date = Convert.ToDateTime(dr["Date"]);

                det.SupplierName = dr["SupplierName"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion                    

        #region Insert

        public static int Insert(DBLayer.ArrivalDetails.Details det)
        {
            string sql = "INSERT INTO ArrivalDetails (ProductId, Quantity, Price, [Date], SupplierId) ";
            sql += " VALUES (" + det.ProductId.ToString() + ", " + det.Quantity.ToString().Replace(",", ".") + ", " + det.Price.ToString().Replace(",", ".") + ", '" + det.Date.ToString("yyyyMMdd") + "', " + det.SupplierId.ToString() + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);

            sql = "SELECT Max(Id) FROM ArrivalDetails";

            return (int)ZFort.DB.Execute.ExecuteString_Scalar(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.ArrivalDetails.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE ArrivalDetails SET [ProductId] = " + det.ProductId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ArrivalDetails SET [SupplierId] = " + det.SupplierId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ArrivalDetails SET [Date] = '" + det.Date.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ArrivalDetails SET [Quantity] = " + det.Quantity.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ArrivalDetails SET [Price] = " + det.Price.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ArrivalDetails SET [ChargeId] = " + det.ChargeId.ToString() + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM ArrivalDetails WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.ArrivalDetails.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM ArrivalDetails WHERE [Id] = " + id.ToString());

            DBLayer.ArrivalDetails.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("ProductId"))
                det.ProductId = Convert.ToInt32(dr["ProductId"]);

            if (!dr.IsNull("Quantity"))
                det.Quantity = Convert.ToDouble(dr["Quantity"]);

            if (!dr.IsNull("Price"))
                det.Price = Convert.ToDouble(dr["Price"]);

            if (!dr.IsNull("SupplierId"))
                det.SupplierId = Convert.ToInt32(dr["SupplierId"]);

            if (!dr.IsNull("ChargeId"))
                det.ChargeId = Convert.ToInt32(dr["ChargeId"]);

            if (!dr.IsNull("Date"))
                det.Date = Convert.ToDateTime(dr["Date"]);

            return det;
        }

        #endregion        
    }
}
