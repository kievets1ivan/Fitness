using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class ProductPriceDynamic
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int ProductId = 0;

            public double Price = 0;
            public DateTime DateStart = DateTime.MinValue;
            public DateTime DateFinish = DateTime.MinValue;

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList(int id)
        {
            string sql = "SELECT * FROM ProductPriceDynamic WHERE ProductId = " + id.ToString();

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.ProductPriceDynamic.Details det = new DBLayer.ProductPriceDynamic.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("ProductId"))
                    det.ProductId = Convert.ToInt32(dr["ProductId"]);

                if (!dr.IsNull("Price"))
                    det.Price = Convert.ToDouble(dr["Price"]);

                if (!dr.IsNull("DateStart"))
                    det.DateStart = Convert.ToDateTime(dr["DateStart"]);

                if (!dr.IsNull("DateFinish"))
                    det.DateFinish = Convert.ToDateTime(dr["DateFinish"]);

                al.Add(det);
            }

            return al;
        }

        #endregion    
        
        #region Insert

        public static void Insert(DBLayer.ProductPriceDynamic.Details det)
        {
            string sql = "INSERT INTO ProductPriceDynamic (ProductId, Price, [DateStart], [DateFinish]) ";
            sql += " VALUES (" + det.ProductId.ToString() + ", " + det.Price.ToString().Replace(",", ".") + ", '" + det.DateStart.ToString("yyyyMMdd") + "', '" + det.DateFinish.ToString("yyyyMMdd") + "')";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.ProductPriceDynamic.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE ProductPriceDynamic SET [ProductId] = " + det.ProductId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ProductPriceDynamic SET [Price] = " + det.Price.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ProductPriceDynamic SET [DateStart] = '" + det.DateStart.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ProductPriceDynamic SET [DateFinish] = '" + det.DateFinish.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM ProductPriceDynamic WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.ProductPriceDynamic.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM ProductPriceDynamic WHERE [Id] = " + id.ToString());

            DBLayer.ProductPriceDynamic.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("ProductId"))
                det.ProductId = Convert.ToInt32(dr["ProductId"]);

            if (!dr.IsNull("Price"))
                det.Price = Convert.ToDouble(dr["Price"]);

            if (!dr.IsNull("DateStart"))
                det.DateStart = Convert.ToDateTime(dr["DateStart"]);

            if (!dr.IsNull("DateFinish"))
                det.DateFinish = Convert.ToDateTime(dr["DateFinish"]);

            return det;
        }

        #endregion        
    }
}
