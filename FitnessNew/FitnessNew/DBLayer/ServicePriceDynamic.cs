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
    public class ServicePriceDynamic :
        IInsertable<ServicePriceDynamicDetails>,
        IUpdatable<ServicePriceDynamicDetails>,
        IGettableDetailsById<ServicePriceDynamicDetails>,
        IDeletable
    {
        ServicePriceDynamicDetails det = new ServicePriceDynamicDetails();

        #region Get List

        public static ArrayList GetList(int id)
        {
            string sql = "SELECT * FROM ServicePriceDynamic WHERE ServiceId = " + id.ToString();

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.ServicePriceDynamic.Details det = new DBLayer.ServicePriceDynamic.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("ServiceId"))
                    det.ServiceId = Convert.ToInt32(dr["ServiceId"]);

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

        public void Insert(ServicePriceDynamicDetails det)
        {
            string sql = "INSERT INTO ServicePriceDynamic (ServiceId, Price, [DateStart], [DateFinish]) ";
            sql += " VALUES (" + det.ServiceId.ToString() + ", " + det.Price.ToString().Replace(",", ".") + ", '" + det.DateStart.ToString("yyyyMMdd") + "', '" + det.DateFinish.ToString("yyyyMMdd") + "')";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public void Update(ServicePriceDynamicDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE ServicePriceDynamic SET [ServiceId] = " + det.ServiceId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServicePriceDynamic SET [Price] = " + det.Price.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServicePriceDynamic SET [DateStart] = '" + det.DateStart.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ServicePriceDynamic SET [DateFinish] = '" + det.DateFinish.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM ServicePriceDynamic WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public ServicePriceDynamicDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM ServicePriceDynamic WHERE [Id] = " + id.ToString());

            ServicePriceDynamicDetails det = new ServicePriceDynamicDetails();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("ServiceId"))
                det.ServiceId = Convert.ToInt32(dr["ServiceId"]);

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
