using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Abonements
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public string Name = "";
            public double Length = 0;
            public double Cost = 0;

            public int AbonementGroup = 0;
            public int LessonsCount = 0;

            public bool IsSingle = false;

            public int AbonementType = 0;

            public bool IsUnlim = false;
            public bool IsSpecial = false;
            public int CoachId = 0;
            public string Time = string.Empty;

            public string Weekdays = string.Empty;

            public int AdditionalVisits = 0;

            #endregion
        }

        #endregion

        #region Details

        public class WideDetails
        {
            #region Constructor

            public WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public string Name = "";
            public double Length = 0;
            public double Cost = 0;

            public int AbonementGroup = 0;
            public int LessonsCount = 0;

            public bool IsSingle = false;

            public int AbonementType = 0;

            public bool IsUnlim = false;
            public bool IsSpecial = false;
            public int CoachId = 0;
            public string Time = string.Empty;

            public string CoachName = string.Empty;

            public string Weekdays = string.Empty;

            public int AdditionalVisits = 0;

            #endregion
        }

        #endregion

        #region Get List

        public static List<DBLayer.Abonements.WideDetails> GetList()
        {
            string sql = "SELECT a.*, c.[Name] AS CoachName FROM Abonements AS a LEFT JOIN Coaches AS c ON c.Id = a.CoachId WHERE IsDeleted = 0";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            var al = new List<DBLayer.Abonements.WideDetails>();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Abonements.WideDetails det = new DBLayer.Abonements.WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                if (!dr.IsNull("Cost"))
                    det.Cost = Convert.ToDouble(dr["Cost"]);

                if (!dr.IsNull("Length"))
                    det.Length = Convert.ToDouble(dr["Length"]);

                if (!dr.IsNull("AbonementGroup"))
                    det.AbonementGroup = Convert.ToInt32(dr["AbonementGroup"]);

                if (!dr.IsNull("LessonsCount"))
                    det.LessonsCount = Convert.ToInt32(dr["LessonsCount"]);

                if (!dr.IsNull("IsSingle"))
                    det.IsSingle = Convert.ToBoolean(dr["IsSingle"]);

                if (!dr.IsNull("CoachId"))
                    det.CoachId = Convert.ToInt32(dr["CoachId"]);

                if (!dr.IsNull("AdditionalVisits"))
                    det.AdditionalVisits = Convert.ToInt32(dr["AdditionalVisits"]);

                if (!dr.IsNull("IsUnlim"))
                    det.IsUnlim = Convert.ToBoolean(dr["IsUnlim"]);


                if (!dr.IsNull("IsSpecial"))
                    det.IsSpecial = Convert.ToBoolean(dr["IsSpecial"]);


                det.Time = dr["Time"].ToString();

                det.CoachName = dr["CoachName"].ToString();

                det.Weekdays = dr["Weekdays"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion

        /*#region Check

        public static bool Check(string name, int id)
        {
           
            Database.Service serv = new Management.Database.Service();

            return serv.AdvertisingSource_Check(name, id);
        }

        #endregion*/

        #region Insert

        public static int Insert(DBLayer.Abonements.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("INSERT INTO Abonements ([Name], [Length], [Cost], AbonementGroup, LessonsCount, IsSingle, IsUnlim, [Time], CoachId, Weekdays, AdditionalVisits, IsSpecial, IsDeleted) VALUES ('" + det.Name + "', " + det.Length.ToString().Replace(",", ".") + ", " + det.Cost.ToString().Replace(",", ".") + ", " + det.AbonementGroup.ToString() + ", " + det.LessonsCount.ToString() + ", " + Convert.ToInt32(det.IsSingle).ToString() + ", " + Convert.ToInt32(det.IsUnlim).ToString() + ", '" + det.Time + "', " + det.CoachId.ToString() + ", '" + det.Weekdays + "', " + det.AdditionalVisits.ToString() + ", " + Convert.ToInt32(det.IsSpecial).ToString() + ", 0)");

            return (int)ZFort.DB.Execute.ExecuteString_Scalar("SELECT Max(Id) FROM Abonements");
        }

        #endregion

        #region Update

        public static void Update(DBLayer.Abonements.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [Name] = '" + det.Name + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [Weekdays] = '" + det.Weekdays + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [Time] = '" + det.Time + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [Length] = " + det.Length.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [AbonementGroup] = " + det.AbonementGroup.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [CoachId] = " + det.CoachId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [AdditionalVisits] = " + det.AdditionalVisits.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [LessonsCount] = " + det.LessonsCount.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [Cost] = " + det.Cost.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [IsSingle] = " + Convert.ToInt32(det.IsSingle).ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [IsUnlim] = " + Convert.ToInt32(det.IsUnlim).ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET [IsSpecial] = " + Convert.ToInt32(det.IsSpecial).ToString() + " WHERE [Id] = " + det.Id.ToString());            
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET IsDeleted = 1 WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.Abonements.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Abonements WHERE [Id] = " + id.ToString());

            DBLayer.Abonements.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.Name = dr["Name"].ToString();

            if (!dr.IsNull("Length"))
                det.Length = Convert.ToDouble(dr["Length"]);

            if (!dr.IsNull("Cost"))
                det.Cost = Convert.ToDouble(dr["Cost"]);

            if (!dr.IsNull("AbonementGroup"))
                det.AbonementGroup = Convert.ToInt32(dr["AbonementGroup"]);

            if (!dr.IsNull("LessonsCount"))
                det.LessonsCount = Convert.ToInt32(dr["LessonsCount"]);

            if (!dr.IsNull("IsSingle"))
                det.IsSingle = Convert.ToBoolean(dr["IsSingle"]);

            if (!dr.IsNull("CoachId"))
                det.CoachId = Convert.ToInt32(dr["CoachId"]);

            if (!dr.IsNull("IsUnlim"))
                det.IsUnlim = Convert.ToBoolean(dr["IsUnlim"]);

            if (!dr.IsNull("IsSpecial"))
                det.IsSpecial = Convert.ToBoolean(dr["IsSpecial"]);

            det.Time = dr["Time"].ToString();

            det.Weekdays = dr["Weekdays"].ToString();

            if (!dr.IsNull("AdditionalVisits"))
                det.AdditionalVisits = Convert.ToInt32(dr["AdditionalVisits"]);

            return det;
        }

        #endregion

        #region GetClientLast by Id

        public static DBLayer.Abonements.Details GetClientLast(int id)
        {
            string sql = "SELECT a.* FROM Abonements AS a INNER JOIN ClientsAbonements AS ca ON ca.AbonementId = a.[Id] ";
            //sql += " INNER JOIN Clients AS c ON c.[Id] = ca.ClientId WHERE c.[Id] = " + id.ToString() + " AND GETDATE() BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish) AND DateFinish = (SELECT Max(DateFinish) FROm ClientsAbonements WHERE CLientId = " + id.ToString() + ")";
            sql += " INNER JOIN Clients AS c ON c.[Id] = ca.ClientId WHERE c.[Id] = " + id.ToString() + " AND GETDATE() BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish) ";

            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow(sql);

            DBLayer.Abonements.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.Name = dr["Name"].ToString();

            if (!dr.IsNull("Length"))
                det.Length = Convert.ToInt32(dr["Length"]);

            if (!dr.IsNull("Cost"))
                det.Cost = Convert.ToDouble(dr["Cost"]);

            if (!dr.IsNull("AbonementGroup"))
                det.AbonementGroup = Convert.ToInt32(dr["AbonementGroup"]);

            if (!dr.IsNull("LessonsCount"))
                det.LessonsCount = Convert.ToInt32(dr["LessonsCount"]);

            if (!dr.IsNull("IsSingle"))
                det.IsSingle = Convert.ToBoolean(dr["IsSingle"]);

            if (!dr.IsNull("CoachId"))
                det.CoachId = Convert.ToInt32(dr["CoachId"]);

            if (!dr.IsNull("IsUnlim"))
                det.IsUnlim = Convert.ToBoolean(dr["IsUnlim"]);

            if (!dr.IsNull("IsSpecial"))
                det.IsSpecial = Convert.ToBoolean(dr["IsSpecial"]);

            det.Time = dr["Time"].ToString();

            det.Weekdays = dr["Weekdays"].ToString();

            if (!dr.IsNull("AdditionalVisits"))
                det.AdditionalVisits = Convert.ToInt32(dr["AdditionalVisits"]);

            return det;
        }

        #endregion
    }
}
