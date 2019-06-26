using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class AbonementPriceDynamic
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int AbonementId = 0;

            public double Price = 0;
            public DateTime DateStart = DateTime.MinValue;
            public DateTime DateFinish = DateTime.MinValue;

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList(int id)
        {

            string sql = "SELECT * FROM AbonementPriceDynamic WHERE AbonementId = " + id.ToString();

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.AbonementPriceDynamic.Details det = new DBLayer.AbonementPriceDynamic.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("AbonementId"))
                    det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

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

        public static void Insert(DBLayer.AbonementPriceDynamic.Details det)
        {
            string sql = "INSERT INTO AbonementPriceDynamic (AbonementId, Price, [DateStart], [DateFinish]) ";
            sql += " VALUES (" + det.AbonementId.ToString() + ", " + det.Price.ToString().Replace(",", ".") + ", '" + det.DateStart.ToString("yyyyMMdd") + "', '" + det.DateFinish.ToString("yyyyMMdd") + "')";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.AbonementPriceDynamic.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementPriceDynamic SET [AbonementId] = " + det.AbonementId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementPriceDynamic SET [Price] = " + det.Price.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementPriceDynamic SET [DateStart] = '" + det.DateStart.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementPriceDynamic SET [DateFinish] = '" + det.DateFinish.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM AbonementPriceDynamic WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.AbonementPriceDynamic.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM AbonementPriceDynamic WHERE [Id] = " + id.ToString());

            DBLayer.AbonementPriceDynamic.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("AbonementId"))
                det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

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
