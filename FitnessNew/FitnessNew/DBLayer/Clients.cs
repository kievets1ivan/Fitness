using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Clients
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;

            public int TypeId = 0;
            public int DocumentId = 0;
            public int SourceId = 0;
            public string FIO = "";
            public string Barcode = "";
            public DateTime RegisterDate = DateTime.MinValue;

            public string DocumentNumber = "";
            public DateTime BirthDate = DateTime.MinValue;

            public string Phone = "";

            public int Sex = 0;

            #endregion
        }

        #endregion

        #region Details

        public class Clients_WideDetails
        {
            #region Constructor

            public Clients_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int TypeId = 0;
            public int DocumentId = 0;
            public int SourceId = 0;
            public string FIO = "";
            public string Barcode = "";
            public DateTime RegisterDate = DateTime.MinValue;

            public string DocumentNumber = "";
            public DateTime BirthDate = DateTime.MinValue;

            public string TypeName = "";
            public string DocumentName = "";
            public string SourceName = "";

            public string Phone = "";

            public string AbonementName = "";
            public DateTime FinishDate = DateTime.MinValue;

            public int VisitsCount = -1;
            public int AVisitsCount = -1;

            public int Sex = 0;

            public string CoachName = string.Empty;

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList()
        {
            string sql = "SELECT c.FIO, c.[Id], c.Barcode, co.[Name] AS CoachName, dt.[Name] AS Document, s.[Name] AS Source, ct.[Name] AS ClientType, c.DocumentNumber, c.BirthDate, c.DocumentId, c.RegisterDate, c.SourceId, c.TypeId, c.Phone, c.Sex, ca.DateFinish, a.[Name] AS AbonementName, ca.VisitsCount, a.LessonsCount AS AVC ";
            sql += " FROM Clients AS c INNER JOIN ClientTypes AS ct ON c.[TypeId] = ct.[Id] ";
            sql += " INNER JOIN DocumentType AS dt ON dt.[Id] = c.[DocumentId] ";
            sql += " INNER JOIN AdvertisingSource AS s ON s.[Id] = c.[SourceId] ";
            sql += " LEFT JOIN ClientsAbonements AS ca ON ca.CLientId = c.[Id] AND ca.IsActual = 1 ";
            sql += " LEFT JOIN Coaches AS co ON co.Id = ca.[CoachId] ";
            sql += " LEFT JOIN Abonements AS a ON a.[Id] = ca.[AbonementId] ";
            //sql += " WHERE getdate() BETWEEN ca.DateStart AND ca.DateFinish";
            sql += " WHERE a.IsSingle IS NULL OR a.IsSingle = 0";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Clients.Clients_WideDetails det = new DBLayer.Clients.Clients_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Barcode = dr["Barcode"].ToString();

                det.DocumentNumber = dr["DocumentNumber"].ToString();

                if (!dr.IsNull("DocumentId"))
                    det.DocumentId = Convert.ToInt32(dr["DocumentId"]);

                det.FIO = dr["FIO"].ToString();

                if (!dr.IsNull("RegisterDate"))
                    det.RegisterDate = Convert.ToDateTime(dr["RegisterDate"]);

                if (!dr.IsNull("BirthDate"))
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

                if (!dr.IsNull("SourceId"))
                    det.SourceId = Convert.ToInt32(dr["SourceId"]);

                if (!dr.IsNull("TypeId"))
                    det.TypeId = Convert.ToInt32(dr["TypeId"]);

                if (!dr.IsNull("AVC"))
                    det.AVisitsCount = Convert.ToInt32(dr["AVC"]);

                det.DocumentName = dr["Document"].ToString();
                det.TypeName = dr["ClientType"].ToString();
                det.SourceName = dr["Source"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("Sex"))
                    det.Sex = Convert.ToInt32(dr["Sex"]);

                if (!dr.IsNull("VisitsCount"))
                    det.VisitsCount = Convert.ToInt32(dr["VisitsCount"]);

                det.AbonementName = dr["AbonementName"].ToString();

                if (!dr.IsNull("DateFinish"))
                    det.FinishDate = Convert.ToDateTime(dr["DateFinish"]);

                det.CoachName = dr["CoachName"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region Get List

        public static ArrayList GetOnlineList()
        {

            string sql = "SELECT c.Barcode, c.Id FROM Clients AS c INNER JOIN Visits AS v ON v.[ClientId] = c.[Id] ";
            sql += " INNER JOIN ClientsAbonements AS ca ON ca.ClientId = c.[Id] AND GETDATE() BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish)  ";
            sql += " WHERE v.TimeOff = '' AND GETDATE() BETWEEN ca.DateStart AND DATEADD(day, 1, ca.DateFinish) ";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Clients.Clients_WideDetails det = new DBLayer.Clients.Clients_WideDetails();

                det.Barcode = dr["Barcode"].ToString();
                det.Id = Convert.ToInt32(dr["Id"]);

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region Get List By Name

        public static ArrayList GetListByName(string name)
        {
            string sql = "SELECT c.FIO, c.[Id], c.Barcode, co.Name AS CoachName, dt.[Name] AS Document, s.[Name] AS Source, ct.[Name] AS ClientType, c.DocumentNumber, c.BirthDate, c.DocumentId, c.RegisterDate, c.SourceId, c.TypeId, c.Phone, c.Sex, ca.DateFinish, a.[Name] AS AbonementName, ca.VisitsCount, a.LessonsCount AS AVC ";
            sql += " FROM Clients AS c INNER JOIN ClientTypes AS ct ON c.[TypeId] = ct.[Id] ";
            sql += " INNER JOIN DocumentType AS dt ON dt.[Id] = c.[DocumentId] ";
            sql += " INNER JOIN AdvertisingSource AS s ON s.[Id] = c.[SourceId] ";
            sql += " LEFT JOIN ClientsAbonements AS ca ON ca.CLientId = c.[Id] AND ca.IsActual = 1 ";
            sql += " LEFT JOIN Coaches AS co ON co.Id = ca.[CoachId] ";
            sql += " INNER JOIN Abonements AS a ON a.[Id] = ca.[AbonementId] ";
            sql += " WHERE FIO LIKE '%" + name + "%'";


            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Clients.Clients_WideDetails det = new DBLayer.Clients.Clients_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Barcode = dr["Barcode"].ToString();

                det.DocumentNumber = dr["DocumentNumber"].ToString();

                if (!dr.IsNull("DocumentId"))
                    det.DocumentId = Convert.ToInt32(dr["DocumentId"]);

                det.FIO = dr["FIO"].ToString();

                if (!dr.IsNull("RegisterDate"))
                    det.RegisterDate = Convert.ToDateTime(dr["RegisterDate"]);

                if (!dr.IsNull("BirthDate"))
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

                if (!dr.IsNull("SourceId"))
                    det.SourceId = Convert.ToInt32(dr["SourceId"]);

                if (!dr.IsNull("TypeId"))
                    det.TypeId = Convert.ToInt32(dr["TypeId"]);

                det.DocumentName = dr["Document"].ToString();
                det.TypeName = dr["ClientType"].ToString();
                det.SourceName = dr["Source"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("Sex"))
                    det.Sex = Convert.ToInt32(dr["Sex"]);

                det.AbonementName = dr["AbonementName"].ToString();

                if (!dr.IsNull("AVC"))
                    det.AVisitsCount = Convert.ToInt32(dr["AVC"]);

                if (!dr.IsNull("VisitsCount"))
                    det.VisitsCount = Convert.ToInt32(dr["VisitsCount"]);

                if (!dr.IsNull("DateFinish"))
                    det.FinishDate = Convert.ToDateTime(dr["DateFinish"]);

                det.CoachName = dr["CoachName"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region Get List By Barcode

        public static ArrayList GetListByBarcode(string name)
        {
            string sql = "SELECT c.FIO, c.[Id], c.Barcode, co.Name AS CoachName, dt.[Name] AS Document, s.[Name] AS Source, ct.[Name] AS ClientType, c.DocumentNumber, c.BirthDate, c.DocumentId, c.RegisterDate, c.SourceId, c.TypeId, c.Phone, c.Sex, ca.DateFinish, a.[Name] AS AbonementName, ca.VisitsCount, a.LessonsCount AS AVC ";
            sql += " FROM Clients AS c INNER JOIN ClientTypes AS ct ON c.[TypeId] = ct.[Id] ";
            sql += " INNER JOIN DocumentType AS dt ON dt.[Id] = c.[DocumentId] ";
            sql += " INNER JOIN AdvertisingSource AS s ON s.[Id] = c.[SourceId] ";
            sql += " LEFT JOIN ClientsAbonements AS ca ON ca.CLientId = c.[Id] AND ca.IsActual = 1 ";
            sql += " LEFT JOIN Coaches AS co ON co.Id = ca.[CoachId] ";
            sql += " INNER JOIN Abonements AS a ON a.[Id] = ca.[AbonementId] ";
            sql += " WHERE Barcode = '" + name + "'";


            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Clients.Clients_WideDetails det = new DBLayer.Clients.Clients_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Barcode = dr["Barcode"].ToString();

                det.DocumentNumber = dr["DocumentNumber"].ToString();

                if (!dr.IsNull("DocumentId"))
                    det.DocumentId = Convert.ToInt32(dr["DocumentId"]);

                det.FIO = dr["FIO"].ToString();

                if (!dr.IsNull("RegisterDate"))
                    det.RegisterDate = Convert.ToDateTime(dr["RegisterDate"]);

                if (!dr.IsNull("BirthDate"))
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

                if (!dr.IsNull("SourceId"))
                    det.SourceId = Convert.ToInt32(dr["SourceId"]);

                if (!dr.IsNull("TypeId"))
                    det.TypeId = Convert.ToInt32(dr["TypeId"]);

                det.DocumentName = dr["Document"].ToString();
                det.TypeName = dr["ClientType"].ToString();
                det.SourceName = dr["Source"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("Sex"))
                    det.Sex = Convert.ToInt32(dr["Sex"]);

                if (!dr.IsNull("VisitsCount"))
                    det.VisitsCount = Convert.ToInt32(dr["VisitsCount"]);

                if (!dr.IsNull("AVC"))
                    det.AVisitsCount = Convert.ToInt32(dr["AVC"]);

                det.AbonementName = dr["AbonementName"].ToString();

                if (!dr.IsNull("DateFinish"))
                    det.FinishDate = Convert.ToDateTime(dr["DateFinish"]);

                det.CoachName = dr["CoachName"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region GetListForInput

        public static ArrayList GetListForInput()
        {
            string sql = "SELECT c.FIO, c.[Id], c.Barcode, dt.[Name] AS Document, s.[Name] AS Source, ct.[Name] AS ClientType, c.DocumentNumber, c.BirthDate, c.DocumentId, c.RegisterDate, c.SourceId, c.TypeId, c.Phone, c.Sex ";
            sql += " FROM Clients AS c INNER JOIN ClientTypes AS ct ON c.[TypeId] = ct.[Id] ";
            sql += " INNER JOIN DocumentType AS dt ON dt.[Id] = c.[DocumentId] ";
            sql += " INNER JOIN AdvertisingSource AS s ON s.[Id] = c.[SourceId] WHERE c.[Id] NOT IN (SELECT ClientId FROM Visits WHERE TimeOff = '')";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Clients.Clients_WideDetails det = new DBLayer.Clients.Clients_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Barcode = dr["Barcode"].ToString();

                det.DocumentNumber = dr["DocumentNumber"].ToString();

                if (!dr.IsNull("DocumentId"))
                    det.DocumentId = Convert.ToInt32(dr["DocumentId"]);

                det.FIO = dr["FIO"].ToString();

                if (!dr.IsNull("RegisterDate"))
                    det.RegisterDate = Convert.ToDateTime(dr["RegisterDate"]);

                if (!dr.IsNull("BirthDate"))
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

                if (!dr.IsNull("SourceId"))
                    det.SourceId = Convert.ToInt32(dr["SourceId"]);

                if (!dr.IsNull("TypeId"))
                    det.TypeId = Convert.ToInt32(dr["TypeId"]);

                det.DocumentName = dr["Document"].ToString();
                det.TypeName = dr["ClientType"].ToString();
                det.SourceName = dr["Source"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("Sex"))
                    det.Sex = Convert.ToInt32(dr["Sex"]);

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region GetListForInput

        public static ArrayList GetListForInputByName(string name)
        {

            string sql = "SELECT c.FIO, c.[Id], c.Barcode, dt.[Name] AS Document, s.[Name] AS Source, ct.[Name] AS ClientType, c.DocumentNumber, c.BirthDate, c.DocumentId, c.RegisterDate, c.SourceId, c.TypeId, c.Phone, c.Sex ";
            sql += " FROM Clients AS c INNER JOIN ClientTypes AS ct ON c.[TypeId] = ct.[Id] ";
            sql += " INNER JOIN DocumentType AS dt ON dt.[Id] = c.[DocumentId] ";
            sql += " INNER JOIN AdvertisingSource AS s ON s.[Id] = c.[SourceId] WHERE FIO LIKE '%" + name + "%' AND c.[Id] NOT IN (SELECT ClientId FROM Visits WHERE TimeOff = '')";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Clients.Clients_WideDetails det = new DBLayer.Clients.Clients_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Barcode = dr["Barcode"].ToString();

                det.DocumentNumber = dr["DocumentNumber"].ToString();

                if (!dr.IsNull("DocumentId"))
                    det.DocumentId = Convert.ToInt32(dr["DocumentId"]);

                det.FIO = dr["FIO"].ToString();

                if (!dr.IsNull("RegisterDate"))
                    det.RegisterDate = Convert.ToDateTime(dr["RegisterDate"]);

                if (!dr.IsNull("BirthDate"))
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

                if (!dr.IsNull("SourceId"))
                    det.SourceId = Convert.ToInt32(dr["SourceId"]);

                if (!dr.IsNull("TypeId"))
                    det.TypeId = Convert.ToInt32(dr["TypeId"]);

                det.DocumentName = dr["Document"].ToString();
                det.TypeName = dr["ClientType"].ToString();
                det.SourceName = dr["Source"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("Sex"))
                    det.Sex = Convert.ToInt32(dr["Sex"]);

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region UploadPhoto

        public static void UploadPhoto(byte[] plan, int Id)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionString);

            conn.Open();

            string sql = "UPDATE Clients SET [Photo] = @par WHERE [Id]=" + Id.ToString();

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

            string sql = "UPDATE Clients SET [Photo] = '' WHERE [Id]=" + Id.ToString();

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

            string sql = "SELECT [Photo] FROM Clients WHERE [Id]=" + Id.ToString();

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
               

        #region Insert

        public static int Insert(DBLayer.Clients.Details det)
        {
            string sql = "INSERT INTO Clients (DocumentId, TypeId, SourceId, FIO, Barcode, RegisterDate, BirthDate, DocumentNumber, Phone, Sex) ";
            sql += " VALUES (" + det.DocumentId.ToString() + ", " + det.TypeId.ToString() + ", " + det.SourceId.ToString() + ", '" + det.FIO + "', '" + det.Barcode + "', '" + det.RegisterDate.ToString("yyyyMMdd") + "', '" + det.BirthDate.ToString("yyyyMMdd") + "', '" + det.DocumentNumber + "', '" + det.Phone + "', " + det.Sex.ToString() + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);

            sql = "SELECT Max([Id]) AS MaxId FROM Clients";

            return (int)ZFort.DB.Execute.ExecuteString_Scalar(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.Clients.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [DocumentId] = " + det.DocumentId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [SourceId] = " + det.SourceId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [TypeId] = " + det.TypeId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [FIO] = '" + det.FIO + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [Barcode] = '" + det.Barcode + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [DocumentNumber] = '" + det.DocumentNumber + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [Phone] = '" + det.Phone + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [RegisterDate] = '" + det.RegisterDate.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [BirthDate] = '" + det.BirthDate.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Clients SET [Sex] = " + det.Sex.ToString() + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Clients WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.Clients.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Clients WHERE [Id] = " + id.ToString());

            DBLayer.Clients.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.FIO = dr["FIO"].ToString();

            det.Barcode = dr["Barcode"].ToString();

            det.DocumentNumber = dr["DocumentNumber"].ToString();

            if (!dr.IsNull("RegisterDate"))
                det.RegisterDate = Convert.ToDateTime(dr["RegisterDate"]);

            if (!dr.IsNull("BirthDate"))
                det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

            if (!dr.IsNull("TypeId"))
                det.TypeId = Convert.ToInt32(dr["TypeId"]);

            if (!dr.IsNull("SourceId"))
                det.SourceId = Convert.ToInt32(dr["SourceId"]);

            if (!dr.IsNull("DocumentId"))
                det.DocumentId = Convert.ToInt32(dr["DocumentId"]);

            if (!dr.IsNull("Sex"))
                det.Sex = Convert.ToInt32(dr["Sex"]);

            det.Phone = dr["Phone"].ToString();

            return det;
        }

        #endregion        

        #region GetDetails by Code

        public static DBLayer.Clients.Details GetDetails(string code)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Clients WHERE [Barcode] = '" + code + "'");

            DBLayer.Clients.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.FIO = dr["FIO"].ToString();

            det.Barcode = dr["Barcode"].ToString();

            det.DocumentNumber = dr["DocumentNumber"].ToString();

            if (!dr.IsNull("RegisterDate"))
                det.RegisterDate = Convert.ToDateTime(dr["RegisterDate"]);

            if (!dr.IsNull("BirthDate"))
                det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

            if (!dr.IsNull("TypeId"))
                det.TypeId = Convert.ToInt32(dr["TypeId"]);

            if (!dr.IsNull("SourceId"))
                det.SourceId = Convert.ToInt32(dr["SourceId"]);

            if (!dr.IsNull("DocumentId"))
                det.DocumentId = Convert.ToInt32(dr["DocumentId"]);

            if (!dr.IsNull("Sex"))
                det.Sex = Convert.ToInt32(dr["Sex"]);

            det.Phone = dr["Phone"].ToString();

            return det;
        }

        #endregion  
      
        #region GetDetails by Id

        public static DBLayer.Abonements.Details GetCurrentAbonement(int id)
        {
            string sql = "SELECT a.* FROM Abonements AS a INNER JOIN ClientsAbonements AS ca ON a.[Id] = ca.AbonementId ";
            sql += " INNER JOIN Clients AS c ON c.[Id] = ca.ClientId WHERE getdate() BETWEEN ca.DateStart AND ca.DateFinish AND c.[Id] = " + id.ToString();

            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow(sql);

            DBLayer.Abonements.Details det = new DBLayer.Abonements.Details();

            if (dr != null)
            {
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

                if (!dr.IsNull("AdditionalVisits"))
                    det.AdditionalVisits = Convert.ToInt32(dr["AdditionalVisits"]);
            }
            return det;
        }

        #endregion

        #region GetBirthDates

        public static ArrayList GetBirthDates(int month)
        {
            string sql = "SELECT * FROM (SELECT DISTINCT FIO, BirthDate, Phone FROM Clients ";
            sql += " INNER JOIN ClientsAbonements AS ca ON ca.ClientId = Clients.Id ";
            sql += " INNER JOIN Abonements AS a ON a.Id = ca.AbonementId ";
            sql += " WHERE a.Id > 0 ";
            sql += " AND a.IsSIngle <> 1 ";

            if (month != 0)
            {
                sql += " AND MONTH(BirthDate) =  " + month.ToString();
            }

            sql += " ) as a ORDER BY Day(a.BirthDate)";
            //sql += " ORDER BY DAY(BirthDate), MONTH(BirthDate) ";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Clients.Details det = new Details();

                det.FIO = dr["FIO"].ToString();

                det.Phone = dr["Phone"].ToString();

                if (!dr.IsNull("BirthDate"))
                    det.BirthDate = Convert.ToDateTime(dr["BirthDate"]);

                al.Add(det);
            }

            return al;
        }

        #endregion
    }
}

