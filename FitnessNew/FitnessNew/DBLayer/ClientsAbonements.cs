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
    public class ClientsAbonements :
        IInsertable<ClientsAbonementsWideDetails>,
        IUpdatable<ClientsAbonementsWideDetails>,
        IGettableDetailsById<ClientsAbonementsWideDetails>,
        IDeletable
    {
        ClientsAbonementsWideDetails det = new ClientsAbonementsWideDetails();

        #region Get List

        public static ArrayList GetList(int id)
        {
            string sql = "SELECT ca.[Id], ca.DateStart, ca.DateFinish, a.[Name], c.FIO, ca.ClientId, ca.AbonementId, ca.VisitsCount, co.Name AS CoachName, ca.CoachId, ca.Weekday, ca.Time ";
            sql += " FROM ClientsAbonements AS ca INNER JOIN Abonements AS a ON ca.[AbonementId] = a.[Id] ";
            sql += " LEFT JOIN Coaches AS co ON ca.CoachId = co.Id ";
            sql += " INNER JOIN Clients AS c ON c.[Id] = ca.ClientId WHERE ca.ClientId = " + id.ToString();
            sql += " ORDER BY DateFinish DESC";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.ClientsAbonements.ClientsAbonements_WideDetails det = new DBLayer.ClientsAbonements.ClientsAbonements_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                det.FIO = dr["FIO"].ToString();

                if (!dr.IsNull("ClientId"))
                    det.ClientId = Convert.ToInt32(dr["ClientId"]);

                if (!dr.IsNull("AbonementId"))
                    det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

                if (!dr.IsNull("VisitsCount"))
                    det.VisitsCount = Convert.ToInt32(dr["VisitsCount"]);

                if (!dr.IsNull("DateStart"))
                    det.DateStart = Convert.ToDateTime(dr["DateStart"]);

                if (!dr.IsNull("DateFinish"))
                    det.DateFinish = Convert.ToDateTime(dr["DateFinish"]);

                det.CoachName = dr["CoachName"].ToString();

                if (!dr.IsNull("CoachId"))
                {
                    det.CoachId = Convert.ToInt32(dr["CoachId"]);
                }

                det.Weekdays = dr["Weekday"].ToString();

                det.Time = dr["Time"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region SetToNull

        public static void SetToNull(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET IsActual = 0 WHERE ClientId = " + id.ToString());
        }

        #endregion

        #region Insert

        public int Insert(ClientsAbonementsWideDetails det)
        {
            var adet = DBLayer.Clients.GetCurrentAbonement(det.ClientId);
            string actual = "0";

            if (adet.Id == 0)
            {
                SetToNull(det.ClientId);
                actual = "1";
            }

            

            string sql = "INSERT INTO ClientsAbonements (ClientId, AbonementId, DateStart, DateFinish, IsActual, VisitsCount, AdditionalVisitsCount, CoachId, Weekday, [Time]) ";
            sql += " VALUES (" + det.ClientId.ToString() + ", " + det.AbonementId.ToString() + ", '" + det.DateStart.ToString("yyyyMMdd") + "', '" + det.DateFinish.ToString("yyyyMMdd") + "', " + actual + ", " + det.VisitsCount.ToString() + ", " + det.AdditionalCount.ToString() + ", " + det.CoachId.ToString() + ", '" + det.Weekdays + "', '" + det.Time + "')";

            ZFort.DB.Execute.ExecuteString_void(sql);

            sql = "SELECT Max(Id) FROM ClientsAbonements";

            int id = 0;

            id = (int)ZFort.DB.Execute.ExecuteString_Scalar(sql);

            return id;
        }

        #endregion

        #region Update

        public void Update(ClientsAbonementsWideDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [ClientId] = " + det.ClientId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [CoachId] = " + det.CoachId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [AbonementId] = " + det.AbonementId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [VisitsCount] = " + det.VisitsCount.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [AdditionalVisitsCount] = " + det.AdditionalCount.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [DateStart] = '" + det.DateStart.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [Weekday] = '" + det.Weekdays + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [Time] = '" + det.Time + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE ClientsAbonements SET [DateFinish] = '" + det.DateFinish.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region SetNewActual

        public static void SetNewActual(int cId)
        {
            DateTime date = GetMaxFinishDate(cId);

            if (date != DateTime.MinValue)
            {
                string sql = "SELECT [Id] FROM ClientsAbonements WHERE ClientId = " + cId.ToString() + " AND DateFinish = '" + date.ToString("yyyyMMdd") + "'";

                int id = (int)ZFort.DB.Execute.ExecuteString_Scalar(sql);

                sql = "UPDATE ClientsAbonements SET IsActual = 1 WHERE [Id] = " + id.ToString();

                ZFort.DB.Execute.ExecuteString_void(sql);
            }
        }

        #endregion

        #region GetMaxFinishDate

        public static DateTime GetMaxFinishDate(int cId)
        {
            string sql = "SELECT Max(DateFinish) FROM ClientsAbonements WHERE ClientId = " + cId.ToString();

            try
            {
                DateTime date = (DateTime)ZFort.DB.Execute.ExecuteString_Scalar(sql);

                return date;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            string user = ((DBLayer.Users.Details)AppDomain.CurrentDomain.GetData("User")).FIO;

            ClientsAbonementsWideDetails det = GetDetailsById(id);

            ClientsWideDetails cDet = DBLayer.Clients.GetDetailsById(det.ClientId);// ?

            AbonementsWideDetails aDet = DBLayer.Abonements.GetDetailsById(det.AbonementId);// ?

            DBLayer.DeletingLog.DeletingLog_Details dlDet = new DeletingLog.DeletingLog_Details();

            dlDet.Date = DateTime.Now;
            dlDet.Name = aDet.Name + "/" + cDet.FIO + "(" + aDet.Cost.ToString() + ")";
            dlDet.Type = 0;
            dlDet.User = user;

            DBLayer.DeletingLog.Insert(dlDet);

            ZFort.DB.Execute.ExecuteString_void("DELETE FROM ClientsAbonements WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public ClientsAbonementsWideDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM ClientsAbonements WHERE [Id] = " + id.ToString());

            ClientsAbonementsWideDetails det = new ClientsAbonementsWideDetails();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("DateStart"))
                det.DateStart = Convert.ToDateTime(dr["DateStart"]);

            if (!dr.IsNull("DateFinish"))
                det.DateFinish = Convert.ToDateTime(dr["DateFinish"]);

            if (!dr.IsNull("ClientId"))
                det.ClientId = Convert.ToInt32(dr["ClientId"]);

            if (!dr.IsNull("VisitsCount"))
                det.VisitsCount = Convert.ToInt32(dr["VisitsCount"]);

            if (!dr.IsNull("AdditionalVisitsCount"))
                det.AdditionalCount = Convert.ToInt32(dr["AdditionalVisitsCount"]);

            if (!dr.IsNull("AbonementId"))
                det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

            if (!dr.IsNull("CoachId"))
            {
                det.CoachId = Convert.ToInt32(dr["CoachId"]);
            }

            det.Weekdays = dr["Weekday"].ToString();

            det.Time = dr["Time"].ToString();

            return det;
        }

        #endregion        

        #region GetDetails by Id

        public static DBLayer.ClientsAbonements.Details GetDetails(int cId, int aId)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM ClientsAbonements WHERE [ClientId] = " + cId.ToString() + " AND AbonementId = " + aId.ToString() + " AND GETDATE() BETWEEN DateStart AND DATEADD(day, 1, DateFinish) ORDER BY Id DESC");

            DBLayer.ClientsAbonements.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("DateStart"))
                det.DateStart = Convert.ToDateTime(dr["DateStart"]);

            if (!dr.IsNull("DateFinish"))
                det.DateFinish = Convert.ToDateTime(dr["DateFinish"]);

            if (!dr.IsNull("ClientId"))
                det.ClientId = Convert.ToInt32(dr["ClientId"]);

            if (!dr.IsNull("VisitsCount"))
                det.VisitsCount = Convert.ToInt32(dr["VisitsCount"]);

            if (!dr.IsNull("AdditionalVisitsCount"))
                det.AdditionalCount = Convert.ToInt32(dr["AdditionalVisitsCount"]);

            if (!dr.IsNull("AbonementId"))
                det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

            if (!dr.IsNull("CoachId"))
            {
                det.CoachId = Convert.ToInt32(dr["CoachId"]);
            }

            det.Weekdays = dr["Weekday"].ToString();

            det.Time = dr["Time"].ToString();

            return det;
        }

        #endregion        

        #region GetFinishDate

        public static DateTime GetFinishDate(int cId, int aId)
        {
            string sql = "SELECT Max(DateFinish) AS FinishDate FROM ClientsAbonements WHERE ClientId = " + cId.ToString() + " AND AbonementId = " + aId.ToString();

            return (DateTime)ZFort.DB.Execute.ExecuteString_Scalar(sql);
        }

        #endregion
    }
}
