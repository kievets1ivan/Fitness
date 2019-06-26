using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Visits
    {
        #region Details

        public class Visits_Details
        {
            #region Constructor

            public Visits_Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int ClientId = 0;
            public DateTime Date = DateTime.MinValue;
            public string Time = "";
            public int Type = 0;

            public int Number = 0;
            public int BoxId = 0;

            public string TimeOff = "";
            public bool WithoutKey = false;

            public int CoachId = 0;

            public bool IsSubstitution = false;

            #endregion
        }

        #endregion

        #region Visits_WideDetails

        public class Visits_WideDetails
        {
            #region Constructor

            public Visits_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int ClientId = 0;
            public string ClientName = "";
            public DateTime DateVisit = DateTime.MinValue;

            public DateTime TimeOnVisit = DateTime.MinValue;
            public DateTime TimeOffVisit = DateTime.MinValue;

            public DateTime Date = DateTime.MinValue;
            public string Time = "";
            public int Type = 0;

            public int Number = 0;
            public int BoxId = 0;

            public string TimeOff = "";
            public bool WithoutKey = false;

            public string AbonementName = "";
            public string BoxType = "";

            public int CoachId = 0;
            public string CoachName = "";

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList(DateTime date1, DateTime date2)
        {
            string sql = "SELECT DISTINCT v.*, b.[Number], c.FIO, a.[Name] AS Abonement, b.Sex, b.[Type] AS BoxType, co.[Name] AS CoachName FROM Visits AS v ";
            sql += " INNER JOIN Boxes As b on b.[Id] = v.BoxId INNER JOIN Clients As c ON c.[Id] = v.ClientID ";
            sql += " INNER JOIN ClientsAbonements As ca ON c.[Id] = ca.ClientId AND v.[Date] BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish)";
            sql += " INNER JOIN Abonements AS a ON ca.AbonementId = a.[Id]";
            sql += " LEFT JOIN Coaches AS co ON v.CoachId = co.[Id] ";
            sql += " WHERE v.TimeOff <> '' ";
            sql += " AND v.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "'";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Visits.Visits_WideDetails det = new DBLayer.Visits.Visits_WideDetails();

                det.BoxId = Convert.ToInt32(dr["BoxId"]);

                det.ClientId = Convert.ToInt32(dr["ClientID"]);

                det.ClientName = dr["FIO"].ToString();

                det.Date = Convert.ToDateTime(dr["Date"]);

                det.DateVisit = Convert.ToDateTime(dr["Date"]);

                det.Id = Convert.ToInt32(dr["Id"]);

                det.Number = Convert.ToInt32(dr["Number"]);

                det.Time = dr["Time"].ToString();

                det.TimeOff = dr["TimeOff"].ToString();

                det.Type = Convert.ToInt32(dr["Type"]);

                det.WithoutKey = Convert.ToBoolean(dr["WithoutKey"]);

                det.CoachName = dr["CoachName"].ToString();

                string sex = "";

                if (Convert.ToInt32(dr["Sex"]) == 0)
                    sex = "Ж";
                else
                    sex = "М";


                det.BoxType = sex;

                det.AbonementName = dr["Abonement"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region GetListOnline

        public static ArrayList GetListOnline()
        {
            string sql = "SELECT v.*, b.[Number], c.FIO, a.[Name] AS Abonement, b.Sex, b.[Type] AS BoxType, co.[Name] AS CoachName FROM Visits AS v ";
            sql += " INNER JOIN Boxes As b on b.[Id] = v.BoxId INNER JOIN Clients As c ON c.[Id] = v.ClientID ";
            sql += " INNER JOIN ClientsAbonements As ca ON c.[Id] = ca.ClientId ";
            sql += " INNER JOIN Abonements AS a ON ca.AbonementId = a.[Id]";
            sql += " LEFT JOIN Coaches AS co ON v.CoachId = co.[Id] ";
            sql += " WHERE v.TimeOff = '' AND GETDATE() BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish) AND ca.Id = (SELECT Max(Id) FROM ClientsAbonements WHERE ClientId = ca.ClientId AND AbonementId = ca.AbonementId)";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Visits.Visits_WideDetails det = new DBLayer.Visits.Visits_WideDetails();

                det.BoxId = Convert.ToInt32(dr["BoxId"]);

                det.ClientId = Convert.ToInt32(dr["ClientID"]);

                det.ClientName = dr["FIO"].ToString();

                det.Date = Convert.ToDateTime(dr["Date"]);

                det.DateVisit = Convert.ToDateTime(dr["Date"]);

                det.Id = Convert.ToInt32(dr["Id"]);

                det.Number = Convert.ToInt32(dr["Number"]);

                det.Time = dr["Time"].ToString();

                det.Type = Convert.ToInt32(dr["Type"]);

                det.WithoutKey = Convert.ToBoolean(dr["WithoutKey"]);

                det.CoachName = dr["CoachName"].ToString();

                string sex = "";

                if (Convert.ToInt32(dr["Sex"]) == 0)
                    sex = "Ж";
                else
                    sex = "М";


                det.BoxType = sex;

                det.AbonementName = dr["Abonement"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region GetListOnline

        public static ArrayList GetListOnline(string name)
        {

            string sql = "SELECT v.*, b.[Number], c.FIO, a.[Name] AS Abonement, b.Sex, b.[Type] AS BoxType, co.[Name] AS CoachName FROM Visits AS v ";
            sql += " INNER JOIN Boxes As b on b.[Id] = v.BoxId INNER JOIN Clients As c ON c.[Id] = v.ClientID ";
            sql += " INNER JOIN ClientsAbonements As ca ON c.[Id] = ca.ClientId ";
            sql += " INNER JOIN Abonements AS a ON ca.AbonementId = a.[Id]";
            sql += " LEFT JOIN Coaches AS co ON v.CoachId = co.[Id] ";
            sql += " WHERE v.TimeOff = '' AND c.FIO LIKE '%" + name + "%'";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Visits.Visits_WideDetails det = new DBLayer.Visits.Visits_WideDetails();

                det.BoxId = Convert.ToInt32(dr["BoxId"]);

                det.ClientId = Convert.ToInt32(dr["ClientID"]);

                det.ClientName = dr["FIO"].ToString();

                det.Date = Convert.ToDateTime(dr["Date"]);

                det.DateVisit = Convert.ToDateTime(dr["Date"]);

                det.Id = Convert.ToInt32(dr["Id"]);

                det.Number = Convert.ToInt32(dr["Number"]);

                det.Time = dr["Time"].ToString();

                det.Type = Convert.ToInt32(dr["Type"]);

                det.WithoutKey = Convert.ToBoolean(dr["WithoutKey"]);

                det.CoachName = dr["CoachName"].ToString();

                string sex = "";

                if (Convert.ToInt32(dr["Sex"]) == 0)
                    sex = "Ж";
                else
                    sex = "М";


                det.BoxType = sex + " " + dr["BoxType"].ToString();

                det.AbonementName = dr["Abonement"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion
       
        #region Insert

        public static void Insert(DBLayer.Visits.Visits_Details det)
        {
            string sql = "INSERT INTO Visits (ClientId, [Date], [Time], [Type], BoxId, WithoutKey, TimeOff, CoachId, IsSubstitution) ";
            sql += " VALUES (" + det.ClientId.ToString() + ", '" + det.Date.Date.ToString("yyyyMMdd") + "', '" + det.Time + "', " + det.Type.ToString() + ", " + det.BoxId.ToString() + ", " + Convert.ToInt32(det.WithoutKey).ToString() + ", '', " + det.CoachId.ToString() + ", " + Convert.ToInt32(det.IsSubstitution) + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.Visits.Visits_Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Visits SET [ClientId] = " + det.ClientId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Visits SET [Date] = '" + det.Date.Date.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Visits SET [Time] = '" + det.Time + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Visits SET [Type] = " + det.Type.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Visits SET [BoxId] = " + det.BoxId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Visits SET [CoachId] = " + det.CoachId.ToString() + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Visits WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.Visits.Visits_Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Visits WHERE [Id] = " + id.ToString());

            DBLayer.Visits.Visits_Details det = new Visits_Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.ClientId = Convert.ToInt32(dr["ClientId"]);

            det.Date = Convert.ToDateTime(dr["Date"]).Date;

            det.Time = dr["Time"].ToString();

            det.Type = Convert.ToInt32(dr["Type"]);

            det.BoxId = Convert.ToInt32(dr["BoxId"]);

            if (!dr.IsNull("CoachId"))
                det.CoachId = Convert.ToInt32(dr["CoachId"]);

            return det;
        }

        #endregion   

        #region GetLastVisit

        public static int GetLastVisit(int id)
        {
            string sql = "SELECT [Id] FROM Visits WHERE ClientId = " + id.ToString() + " AND [TimeOff] = '' AND [Date] IN (SELECT Max([Date]) FROM Visits WHERE ClientId = " + id.ToString() + ")";

            int ind = (int)ZFort.DB.Execute.ExecuteString_Scalar(sql);

            return ind;
        }

        #endregion

        #region SetTimeOff

        public static void SetTimeOff(int id, string time)
        {
            string sql = "UPDATE Visits SET TimeOff = '" + time + "' WHERE [Id] = " + id.ToString();

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion
    }
}
