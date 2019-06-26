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
    public class UserRate :
        IInsertable<UserRateDetails>,
        IUpdatable<UserRateDetails>,
        IGettableDetailsById<UserRateDetails>,
        IDeletable
    {
        UserRateDetails det = new UserRateDetails();

        #region Get List

        public static ArrayList GetList()
        {

            string sql = "SELECT ur.*, u.FIO FROM UserRate As ur INNER JOIN Users As u ON ur.UserId = u.[Id]";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.UserRate.Details det = new DBLayer.UserRate.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("Constant"))
                    det.Constant = Convert.ToDouble(dr["Constant"]);

                if (!dr.IsNull("PercentForClients"))
                    det.PercentForClients = Convert.ToDouble(dr["PercentForClients"]);

                if (!dr.IsNull("PercentForSales"))
                    det.PercentForSales = Convert.ToDouble(dr["PercentForSales"]);

                if (!dr.IsNull("UserId"))
                    det.UserId = Convert.ToInt32(dr["UserId"]);

                al.Add(det);
            }

            return al;
        }

        #endregion     
        
        #region Insert

        public void Insert(UserRateDetails det)
        {
            string sql = "INSERT INTO UserRate (UserId, Constant, PercentForClients, PercentForSales, PercentForService, PercentForFitness, PercentForMassage) ";
            sql += " VALUES (" + det.UserId.ToString() + ", " + det.Constant.ToString() + ", " + det.PercentForClients.ToString().Replace(",", ".") + ", " + det.PercentForSales.ToString().Replace(",", ".") + ", " + det.PercentForService.ToString().Replace(",", ".") + ", " + det.PercentForFitness.ToString().Replace(",", ".") + ", " + det.PercentForMassage.ToString().Replace(",", ".") + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public void Update(UserRateDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE UserRate SET [UserId] = " + det.UserId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserRate SET [Constant] = " + det.Constant.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserRate SET [PercentForClients] = " + det.PercentForClients.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserRate SET [PercentForSales] = " + det.PercentForSales.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserRate SET [PercentForService] = " + det.PercentForService.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserRate SET [PercentForFitness] = " + det.PercentForFitness.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE UserRate SET [PercentForMassage] = " + det.PercentForMassage.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM UserRate WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public UserRateDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM UserRate WHERE [UserId] = " + id.ToString());

            UserRateDetails det = new UserRateDetails();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("UserId"))
                det.UserId = Convert.ToInt32(dr["UserId"]);

            if (!dr.IsNull("Constant"))
                det.Constant = Convert.ToDouble(dr["Constant"]);

            if (!dr.IsNull("PercentForSales"))
                det.PercentForSales = Convert.ToDouble(dr["PercentForSales"]);

            if (!dr.IsNull("PercentForClients"))
                det.PercentForClients = Convert.ToDouble(dr["PercentForClients"]);

            if (!dr.IsNull("PercentForService"))
                det.PercentForService = Convert.ToDouble(dr["PercentForService"]);

            if (!dr.IsNull("PercentForFitness"))
                det.PercentForFitness = Convert.ToDouble(dr["PercentForFitness"]);

            if (!dr.IsNull("PercentForMassage"))
                det.PercentForMassage = Convert.ToDouble(dr["PercentForMassage"]);

            return det;
        }

        #endregion        
    }
}
