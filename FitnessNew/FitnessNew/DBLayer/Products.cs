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
    public class Products :
        IInsertable<ProductsWideDetails>,
        IUpdatable<ProductsWideDetails>,
        IGettableDetailsById<ProductsWideDetails>,
        IDeletable
    {
        ProductsWideDetails det = new ProductsWideDetails();

        MovementDetails movDet = new MovementDetails();

        RestDetails restDet = new RestDetails();

        #region Get List

        public static ArrayList GetList()
        {
            string sql = "SELECT p.[Name], p.[Id], p.Barcode, p.GroupId, p.DimensionId, p.[Description], d.[Name] AS Dimension, g.[Name] AS [Group], p.Price ";
            sql += " FROM Products AS p INNER JOIN ProductGroup AS g ON p.[GroupId] = g.[Id] ";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = p.[DimensionId] ";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Products.Products_WideDetails det = new DBLayer.Products.Products_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                det.Barcode = dr["Barcode"].ToString();

                det.Description = dr["Description"].ToString();

                if (!dr.IsNull("GroupId"))
                    det.GroupId = Convert.ToInt32(dr["GroupId"]);

                if (!dr.IsNull("Price"))
                    det.Price = Convert.ToDouble(dr["Price"]);

                if (!dr.IsNull("DimensionId"))
                    det.DimensionId = Convert.ToInt32(dr["DimensionId"]);

                det.DimensionName = dr["Dimension"].ToString();

                det.GroupName = dr["Group"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region Get List

        public static ArrayList GetList(int id)
        {
            string sql = "SELECT p.[Name], p.[Id], p.Barcode, p.GroupId, p.DimensionId, p.[Description], d.[Name] AS Dimension, g.[Name] AS [Group], p.Price ";
            sql += " FROM Products AS p INNER JOIN ProductGroup AS g ON p.[GroupId] = g.[Id] ";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = p.[DimensionId] WHERE GroupId = " + id.ToString(); ;

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Products.Products_WideDetails det = new DBLayer.Products.Products_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                det.Barcode = dr["Barcode"].ToString();

                det.Description = dr["Description"].ToString();

                if (!dr.IsNull("GroupId"))
                    det.GroupId = Convert.ToInt32(dr["GroupId"]);

                if (!dr.IsNull("Price"))
                    det.Price = Convert.ToDouble(dr["Price"]);

                if (!dr.IsNull("DimensionId"))
                    det.DimensionId = Convert.ToInt32(dr["DimensionId"]);

                det.DimensionName = dr["Dimension"].ToString();

                det.GroupName = dr["Group"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region Get List

        public static ArrayList GetProducts(DateTime date)
        {

            string sql = "SELECT DISTINCT p.[Id], p.[Name] AS ProductName, p.Barcode, pg.Name AS ProductGroup, d.[Name] AS DimensionName, p.Price, (SELECT SUM(Quantity) FROM Sales AS s WHERE s.ProductId = p.Id AND [Date] = '" + date.ToString("yyyyMMdd") + "') AS Sold";
            sql += " FROM Products AS p INNER JOIN ProductGroup AS pg ON pg.[Id] = p.GroupId ";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = p.DimensionId ";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Products.Products_WideDetails det = new DBLayer.Products.Products_WideDetails();

                det.DimensionName = dr["DimensionName"].ToString();

                if (!dr.IsNull("Sold"))
                    det.Sold = Convert.ToInt32(dr["Sold"]);

                det.GroupName = dr["ProductGroup"].ToString();

                det.Name = dr["ProductName"].ToString();

                det.Price = Convert.ToDouble(dr["Price"]);

                det.Id = Convert.ToInt32(dr["Id"]);

                det.Barcode = dr["Barcode"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region GetRest

        public static ArrayList GetRest(DateTime date1, DateTime date2)
        {
            string sql = "SELECT c.[ProductId], c.ProductName, c.DimensionName, c.GroupName, SUM(c.Arrived) AS Rest, pr.Price FROM ";
            sql += " (SELECT * FROM ";
            sql += " (SELECT DISTINCT p.[Id] AS ProductId, p.[Name] AS ProductName, ISNULL(SUM(ad.Quantity), 0) AS Arrived, d.[Name] AS DimensionName, pg.[Name] AS GroupName, ad.[Date] ";
            sql += " FROM Products AS p INNER JOIN ArrivalDetails AS ad ON ad.[ProductId] = p.[Id] ";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = p.DimensionId ";
            sql += " INNER JOIN ProductGroup AS pg ON pg.[Id] = p.GroupId ";
            sql += " GROUP BY p.[Name], d.[Name], pg.[Name], ad.[Date], p.[Id]) as a ";
            sql += " UNION ";
            sql += " SELECT * FROM ";
            sql += " (SELECT DISTINCT p.[Id] AS ProductId, p.[Name] AS ProductName, -1 * ISNULL(SUM(s.Quantity), 0) AS Arrived, d.[Name] AS DimensionName, pg.[Name] AS GroupName, s.[Date] ";
            sql += " FROM Products AS p INNER JOIN Sales AS s ON s.[ProductId] = p.[Id] ";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = p.DimensionId ";
            sql += " INNER JOIN ProductGroup AS pg ON pg.[Id] = p.GroupId ";
            sql += " GROUP BY p.[Name], d.[Name], pg.[Name], s.[Date], p.[Id]) as b) AS c ";
            sql += " INNER JOIN Products AS pr ON pr.[Id] = c.ProductId ";
            sql += " WHERE c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "'";
            sql += " GROUP BY c.ProductName, c.DimensionName, c.GroupName, c.ProductId, pr.Price ";


            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Products.Rest_Details det = new DBLayer.Products.Rest_Details();

                det.DimensionName = dr["DimensionName"].ToString();

                det.GroupName = dr["GroupName"].ToString();

                det.ProductName = dr["ProductName"].ToString();

                if (!dr.IsNull("Rest"))
                    det.Rest = Convert.ToDouble(dr["Rest"]);

                if (!dr.IsNull("Price"))
                    det.Price = Convert.ToDouble(dr["Price"]);

                if (!dr.IsNull("ProductId"))
                {
                    det.Id = Convert.ToInt32(dr["ProductId"]);
                }

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region Insert

        public  void Insert(ProductsWideDetails det)
        {
            string sql = "INSERT INTO Products (GroupId, DimensionId, [Name], Barcode, Description, Price) ";
            sql += " VALUES (" + det.GroupId.ToString() + ", " + det.DimensionId.ToString() + ", '" + det.Name + "', '" + det.Barcode + "', '" + det.Description + "', " + det.Price.ToString().Replace(",", ".") + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public void Update(ProductsWideDetails det)
        {

            ZFort.DB.Execute.ExecuteString_void("UPDATE Products SET [GroupId] = " + det.GroupId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Products SET [DimensionId] = " + det.DimensionId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Products SET [Price] = " + det.Price.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Products SET [Name] = '" + det.Name + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Products SET [Barcode] = '" + det.Barcode + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Products SET [Description] = '" + det.Description.ToString() + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public  void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Products WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public ProductsWideDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Products WHERE [Id] = " + id.ToString());

            ProductsWideDetails det = new ProductsWideDetails();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.Barcode = dr["Barcode"].ToString();

            det.Name = dr["Name"].ToString();

            if (!dr.IsNull("Description"))
                det.Description = dr["Description"].ToString();

            if (!dr.IsNull("GroupId"))
                det.GroupId = Convert.ToInt32(dr["GroupId"]);

            if (!dr.IsNull("Price"))
                det.Price = Convert.ToDouble(dr["Price"]);

            if (!dr.IsNull("DimensionId"))
                det.DimensionId = Convert.ToInt32(dr["DimensionId"]);

            return det;
        }

        #endregion        

        #region GetMovementLines

        public static ArrayList GetMovementLines(int productId)
        {
            string sql = "SELECT [Date], Quantity AS Arrived, 0 AS Dispatched FROM ArrivalDetails WHERE ProductId = " + productId.ToString();
            sql += " UNION  ";
            sql += " SELECT [Date], 0 AS Arrived, Quantity AS Dispatched FROM Sales WHERE ProductId = " + productId.ToString();

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                MovementDetails det = new MovementDetails();

                if (!dr.IsNull("Arrived"))
                {
                    det.Arrived = Convert.ToInt32(dr["Arrived"]);
                }

                if (!dr.IsNull("Dispatched"))
                {
                    det.Dispatched = Convert.ToInt32(dr["Dispatched"]);
                }

                if (!dr.IsNull("Date"))
                {
                    det.Date = Convert.ToDateTime(dr["Date"]);
                }

                al.Add(det);
            }

            return al;
        }

        #endregion
    }
}
