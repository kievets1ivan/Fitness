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
    public class Coaches :
        IInsertable<CoachesWideDetails>,
        IUpdatable<CoachesWideDetails>,
        IGettableDetailsById<CoachesWideDetails>,
        IDeletable
    {
        CoachesWideDetails det = new CoachesWideDetails();

        #region Get List

        public static ArrayList GetList()
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM Coaches ORDER BY Name");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Coaches.Details det = new DBLayer.Coaches.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("HireDate"))
                {
                    det.HireDate = Convert.ToDateTime(dr["HireDate"]);
                }

                if (!dr.IsNull("FireDate"))
                {
                    det.FireDate = Convert.ToDateTime(dr["FireDate"]);
                }

                if (!dr.IsNull("BirthDate"))
                {
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                }

                if (!dr.IsNull("Sex"))
                {
                    det.Sex = Convert.ToInt32(dr["Sex"]);
                }

                if (!dr.IsNull("Type"))
                {
                    det.Type = Convert.ToInt32(dr["Type"]);
                }

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region Get List

        public static ArrayList GetList(int type)
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM Coaches WHERE Type IS NULL OR Type = " + type.ToString() + " ORDER BY Name");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Coaches.Details det = new DBLayer.Coaches.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("HireDate"))
                {
                    det.HireDate = Convert.ToDateTime(dr["HireDate"]);
                }

                if (!dr.IsNull("FireDate"))
                {
                    det.FireDate = Convert.ToDateTime(dr["FireDate"]);
                }

                if (!dr.IsNull("BirthDate"))
                {
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                }

                if (!dr.IsNull("Sex"))
                {
                    det.Sex = Convert.ToInt32(dr["Sex"]);
                }

                if (!dr.IsNull("Type"))
                {
                    det.Type = Convert.ToInt32(dr["Type"]);
                }

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

        public int Insert(CoachesWideDetails det)
        {
            if (det.FireDate != DateTime.MinValue)
            {
                ZFort.DB.Execute.ExecuteString_void("INSERT INTO Coaches ([Name], Phone, HireDate, FireDate, BirthDate, Sex, [Type]) VALUES ('" + det.Name + "', '" + det.Phone + "', '" + det.HireDate.ToString("yyyyMMdd") + "', '" + det.FireDate.ToString("yyyyMMdd") + "', '" + det.BirthDate.ToString("yyyyMMdd") + "', " + det.Sex.ToString() + ", " + det.Type.ToString() + ")");
            }
            else
            {
                ZFort.DB.Execute.ExecuteString_void("INSERT INTO Coaches ([Name], Phone, HireDate, BirthDate, Sex, [Type]) VALUES ('" + det.Name + "', '" + det.Phone + "', '" + det.HireDate.ToString("yyyyMMdd") +"', '" + det.BirthDate.ToString("yyyyMMdd") + "', " + det.Sex.ToString() + ", " + det.Type.ToString() + ")");
            }

            return (int)ZFort.DB.Execute.ExecuteString_Scalar("SELECT Max(Id) FROM Coaches");
        }

        #endregion

        #region Update

        public void Update(CoachesWideDetails det)
        {

            ZFort.DB.Execute.ExecuteString_void("UPDATE Coaches SET [Name] = '" + det.Name + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Coaches SET [Phone] = '" + det.Phone + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Coaches SET [Sex] = " + det.Sex.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Coaches SET [Type] = " + det.Type.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Coaches SET [HireDate] = '" + det.HireDate.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Coaches SET [BirthDate] = '" + det.BirthDate.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            if (det.FireDate != DateTime.MinValue)
            {
                ZFort.DB.Execute.ExecuteString_void("UPDATE Coaches SET [FireDate] = '" + det.FireDate.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());
            }
            else
            {
                ZFort.DB.Execute.ExecuteString_void("UPDATE Coaches SET [FireDate] = NULL WHERE [Id] = " + det.Id.ToString());
            }
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Coaches WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region DeleteByAbonements

        public static void DeleteByAbonements(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM CoachesAbonements WHERE [CoachId] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public CoachesWideDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Coaches WHERE [Id] = " + id.ToString());

            CoachesWideDetails det = new CoachesWideDetails();
            if ((dr != null))
            {
                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("HireDate"))
                {
                    det.HireDate = Convert.ToDateTime(dr["HireDate"]);
                }

                if (!dr.IsNull("FireDate"))
                {
                    det.FireDate = Convert.ToDateTime(dr["FireDate"]);
                }

                if (!dr.IsNull("BirthDate"))
                {
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                }

                if (!dr.IsNull("Sex"))
                {
                    det.Sex = Convert.ToInt32(dr["Sex"]);
                }

                if (!dr.IsNull("Type"))
                {
                    det.Type = Convert.ToInt32(dr["Type"]);
                }
            }

            return det;
        }

        #endregion

        #region UploadPhoto

        public static void UploadPhoto(byte[] plan, int Id)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionString);

            conn.Open();

            string sql = "UPDATE Coaches SET [Photo] = @par WHERE [Id]=" + Id.ToString();

            SqlCommand comm = new SqlCommand(sql, conn);

            comm.Parameters.Add("@par", SqlDbType.Image);

            comm.Parameters["@par"].Value = plan;

            comm.ExecuteNonQuery();

            conn.Close();
        }

        #endregion

        #region RemovePhoto

        public static void RemovePhoto(int Id)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionString);

            conn.Open();

            string sql = "UPDATE Coaches SET [Photo] = '' WHERE [Id]=" + Id.ToString();

            SqlCommand comm = new SqlCommand(sql, conn);

            comm.ExecuteNonQuery();

            conn.Close();
        }

        #endregion

        #region DownloadPhoto

        public static byte[] DownloadPhoto(int Id)
        {
            byte[] result = null;

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionString);

            conn.Open();

            string sql = "SELECT [Photo] FROM Coaches WHERE [Id]=" + Id.ToString();

            SqlCommand comm = new SqlCommand(sql, conn);

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(comm);

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (dr["Photo"].ToString() != "")
                    result = (byte[])dr["Photo"];
            }

            conn.Close();

            return result;
        }

        #endregion

        #region VisitsReport

        public static ArrayList VisitsReport(int month, int year)
        {
            string sql = "SELECT cs.Name, ";
            sql += " (SELECT Count(*) FROM  ";
            sql += " (SELECT DISTINCT v.*, b.[Number], c.FIO, a.[Name] AS Abonement,  ";
            sql += " b.Sex, b.[Type] AS BoxType, co.[Name] AS CoachName ";
            sql += " FROM Visits AS v  INNER JOIN Boxes As b on b.[Id] = v.BoxId  ";
            sql += " INNER JOIN Clients As c ON c.[Id] = v.ClientID  ";
            sql += " INNER JOIN ClientsAbonements As ca ON c.[Id] = ca.ClientId ";
            sql += " AND v.[Date] BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish) ";
            sql += " INNER JOIN Abonements AS a ON ca.AbonementId = a.[Id]  ";
            sql += " LEFT JOIN Coaches AS co ON ca.CoachId = co.[Id]   ";
            sql += " WHERE v.TimeOff <> '' AND ca.CoachId = cs.Id AND ((IsSubstitution IS NULL) OR (IsSubstitution = 0)) AND Month(v.[Date]) = " + month.ToString() + " AND YEAR(v.Date) = " + year.ToString() + ") AS a) AS Abonements, ";
            sql += " (SELECT Count(ca.Id) FROM ClientsAbonements AS ca ";
            sql += " INNER JOIN AbonementIncome AS ai ON ai.ClientAbonementId = ca.Id INNER JOIN Abonements AS a ON a.Id = ca.AbonementId ";
            sql += " WHERE MONTH(ai.Date) = " + month.ToString() + " AND YEAR(ai.Date) = " + year.ToString() + " AND a.IsSingle = 1 AND ca.CoachId = cs.Id) AS Single, ";
            sql += " (SELECT Count(*) FROM Visits AS v WHERE v.IsSubstitution = 1 AND MONTH(v.Date) = " + month.ToString();
            sql += " AND YEAR(v.Date) = " + year.ToString() + " AND v.CoachId = cs.Id ) AS Substitutions ";
            sql += " FROM Coaches AS cs WHERE FireDate IS NULL ";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Coaches.VisitsReportDetails det = new VisitsReportDetails();

                det.Name = dr["Name"].ToString();

                if (!dr.IsNull("Abonements"))
                {
                    det.Abonements = Convert.ToInt32(dr["Abonements"]);
                }

                if (!dr.IsNull("Single"))
                {
                    det.Single = Convert.ToInt32(dr["Single"]);
                }

                if (!dr.IsNull("Substitutions"))
                {
                    det.Substitutions = Convert.ToInt32(dr["Substitutions"]);
                }

                al.Add(det);
            }
                        
            return al;
        }

        #endregion
    }
}
