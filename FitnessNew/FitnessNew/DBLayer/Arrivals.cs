using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Arrivals
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public string Number = "";
            public int SupplierId = 0;
            public DateTime Date = DateTime.MinValue;
            public int InquiryId = 0;

            #endregion
        }

        #endregion

        #region WideDetails

        public class Arrivals_WideDetails
        {
            #region Constructor

            public Arrivals_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public string Number = "";
            public int SupplierId = 0;
            public string SupplierName = "";
            public DateTime Date = DateTime.MinValue;
            public int InquiryId = 0;
            public string InquiryNumber = "";

            public double Total = 0;

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList()
        {
            string sql = "SELECT a.*, i.Number AS InquiryNumber, s.[Name], (SELECT SUM((ad.Price * ad.Quantity)) FROM ArrivalDetails AS ad WHERE ad.ArrivalId = a.[Id]) AS Total FROM ";
            sql += " Arrival a LEFT JOIN Inquries AS i ON a.[InquiryId] = i.[Id] ";
            sql += " INNER JOIN Suppliers AS s ON s.[Id] = a.[SupplierId]";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Arrivals.Arrivals_WideDetails det = new DBLayer.Arrivals.Arrivals_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("Date"))
                    det.Date = Convert.ToDateTime(dr["Date"]);

                if (!dr.IsNull("InquiryId"))
                    det.InquiryId = Convert.ToInt32(dr["InquiryId"]);

                det.InquiryNumber = dr["InquiryNumber"].ToString();

                det.Number = dr["Number"].ToString();

                if (!dr.IsNull("SupplierId"))
                    det.SupplierId = Convert.ToInt32(dr["SupplierId"]);

                det.SupplierName = dr["Name"].ToString();

                if (!dr.IsNull("Total"))
                    det.Total = Convert.ToDouble(dr["Total"]);

                al.Add(det);
            }

            return al;
        }

        #endregion                    

        #region Insert

        public static void Insert(DBLayer.Arrivals.Details det)
        {
            string sql = "INSERT INTO Arrival (SupplierId, InquiryId, Date, Number) ";
            sql += " VALUES (" + det.SupplierId.ToString() + ", " + det.InquiryId.ToString() + ", '" + det.Date.ToString("yyyyMMdd") + "', '" + det.Number + "')";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.Arrivals.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Arrival SET [SupplierId] = " + det.SupplierId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Arrival SET [InquiryId] = " + det.InquiryId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Arrival SET [Date] = '" + det.Date.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Arrival SET [Number] = '" + det.Number + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Arrival  WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.Arrivals.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Arrival WHERE [Id] = " + id.ToString());

            DBLayer.Arrivals.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("SupplierId"))
                det.SupplierId = Convert.ToInt32(dr["SupplierId"]);

            if (!dr.IsNull("InquiryId"))
                det.InquiryId = Convert.ToInt32(dr["InquiryId"]);

            if (!dr.IsNull("Date"))
                det.Date = Convert.ToDateTime(dr["Date"]);

            if (!dr.IsNull("Number"))
                det.Number = Convert.ToString(dr["Number"]);

            return det;
        }

        #endregion        
    }
}
