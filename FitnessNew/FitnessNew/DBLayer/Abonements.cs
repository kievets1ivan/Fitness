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
    public class Abonements :
        IInsertable<AbonementsDetails>,
        IUpdatable<AbonementsDetails>,
        IGettableDetailsById<AbonementsDetails>,
        IDeletable
    {
        AbonementsDetails det = new AbonementsDetails();

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

        public int Insert(AbonementsDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("INSERT INTO Abonements ([Name], [Length], [Cost], AbonementGroup, LessonsCount, IsSingle, IsUnlim, [Time], CoachId, Weekdays, AdditionalVisits, IsSpecial, IsDeleted) VALUES ('" + det.Name + "', " + det.Length.ToString().Replace(",", ".") + ", " + det.Cost.ToString().Replace(",", ".") + ", " + det.AbonementGroup.ToString() + ", " + det.LessonsCount.ToString() + ", " + Convert.ToInt32(det.IsSingle).ToString() + ", " + Convert.ToInt32(det.IsUnlim).ToString() + ", '" + det.Time + "', " + det.CoachId.ToString() + ", '" + det.Weekdays + "', " + det.AdditionalVisits.ToString() + ", " + Convert.ToInt32(det.IsSpecial).ToString() + ", 0)");

            return (int)ZFort.DB.Execute.ExecuteString_Scalar("SELECT Max(Id) FROM Abonements");
        }

        #endregion

        #region Update

        public void Update(AbonementsDetails det)
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

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Abonements SET IsDeleted = 1 WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public AbonementsDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Abonements WHERE [Id] = " + id.ToString());

            AbonementsDetails det = new AbonementsDetails();

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

        public static AbonementsDetails GetClientLast(int id)
        {
            string sql = "SELECT a.* FROM Abonements AS a INNER JOIN ClientsAbonements AS ca ON ca.AbonementId = a.[Id] ";
            //sql += " INNER JOIN Clients AS c ON c.[Id] = ca.ClientId WHERE c.[Id] = " + id.ToString() + " AND GETDATE() BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish) AND DateFinish = (SELECT Max(DateFinish) FROm ClientsAbonements WHERE CLientId = " + id.ToString() + ")";
            sql += " INNER JOIN Clients AS c ON c.[Id] = ca.ClientId WHERE c.[Id] = " + id.ToString() + " AND GETDATE() BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish) ";

            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow(sql);

            AbonementsDetails det = new AbonementsDetails();

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
