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
    public class AbonementPriceDynamic :
        IInsertable<AbonementPriceDynamicDetails>,
        IUpdatable<AbonementPriceDynamicDetails>,
        IGettableDetailsById<AbonementPriceDynamicDetails>,
        IDeletable
    {

        AbonementPriceDynamicDetails det = new AbonementPriceDynamicDetails();

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

        public void Insert(AbonementPriceDynamicDetails det)
        {
            string sql = "INSERT INTO AbonementPriceDynamic (AbonementId, Price, [DateStart], [DateFinish]) ";
            sql += " VALUES (" + det.AbonementId.ToString() + ", " + det.Price.ToString().Replace(",", ".") + ", '" + det.DateStart.ToString("yyyyMMdd") + "', '" + det.DateFinish.ToString("yyyyMMdd") + "')";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public void Update(AbonementPriceDynamicDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementPriceDynamic SET [AbonementId] = " + det.AbonementId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementPriceDynamic SET [Price] = " + det.Price.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementPriceDynamic SET [DateStart] = '" + det.DateStart.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementPriceDynamic SET [DateFinish] = '" + det.DateFinish.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM AbonementPriceDynamic WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public AbonementPriceDynamicDetails GetDetailsById(int id)
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
