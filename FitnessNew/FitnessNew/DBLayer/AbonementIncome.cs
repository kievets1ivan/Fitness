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
    public class AbonementIncome : 
        IInsertable<AbonementIncomeWideDetails>, 
        IUpdatable<AbonementIncomeWideDetails>, 
        IGettableDetailsById<AbonementIncomeWideDetails>,
        IDeletable
    {

        AbonementIncomeWideDetails det = new AbonementIncomeWideDetails();

        #region ReportDetails

        public class Money_ReportDetails
        {
            #region Constructor

            public Money_ReportDetails() { }

            #endregion

            #region Fields

            public int Type = 0;
            public DateTime Date = DateTime.MinValue;
            public string Time = "";
            public double Summ = 0;
            public string UserName = "";
            public string Name = "";
            public int Quantity = 0;
            public double Price = 0;
            public string DimensionName = "";

            public string GroupName = "";

            public bool IsDeleted = false;
            public DateTime DeleteDate = DateTime.MinValue;
            public string DeleteReason = "";

            public string AbonementName = "";

            public string ChargeGroupName = "";

            public int AbonementGroup = 0;

            #endregion
        }

        #endregion

        #region FinancialDetails

        public class FinincialDetails
        {
            public DateTime Date { get; set; }
            public double Value { get; set; }
        }

        #endregion

        #region UserIncome_ReportDetails

        public class UserIncome_ReportDetails
        {
            #region Constructor

            public UserIncome_ReportDetails() { }

            #endregion

            #region Fields

            public int Type = 0;
            public DateTime Date = DateTime.MinValue;
            public string Time = "";
            public double Summ = 0;
            public string UserName = "";
            public string Name = "";
            public int Quantity = 0;
            public double Price = 0;
            public string DimensionName = "";

            public string GroupName = "";

            public bool IsDeleted = false;
            public DateTime DeleteDate = DateTime.MinValue;
            public string DeleteReason = "";

            public double ClientsSumm = 0;
            public double ServiceSumm = 0;
            public double SalesSumm = 0;

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList(int id, DateTime date1, DateTime date2)
        {

            string sql = "SELECT DISTINCT a.AbonementGroup, ai.[Date], ai.[Summ], ai.[Id], c.FIO, a.[Name] AS AbonementName, u.FIO AS [User], ai.ClientId, ai.AbonementId, ai.UserId, ai.IsDeleted, ai.DeleteDate, ai.DeleteReason, co.Name AS CoachName, ca.Time, ca.Weekday ";
            sql += " FROM AbonementIncome AS ai INNER JOIN Clients AS c ON c.[Id] = ai.CLientId ";
            sql += " INNER JOIN Abonements AS a ON a.[Id] = ai.AbonementId ";
            sql += " LEFT JOIN ClientsAbonements AS ca ON ai.ClientAbonementId = ca.Id ";
            sql += " LEFT JOIN Coaches AS co ON co.[Id] = ca.[CoachId] ";
            sql += " INNER JOIN Users AS u ON u.[Id] = ai.[UserId] ";
            sql += " WHERE ai.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "'";
            sql += " ORDER BY ai.[Date]";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.AbonementIncome.AbonementIncome_WideDetails det = new DBLayer.AbonementIncome.AbonementIncome_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.AbonementName = dr["AbonementName"].ToString();

                det.FIO = dr["FIO"].ToString();

                det.User = dr["User"].ToString();

                if (!dr.IsNull("ClientId"))
                    det.ClientId = Convert.ToInt32(dr["ClientId"]);

                if (!dr.IsNull("AbonementId"))
                    det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

                if (!dr.IsNull("UserId"))
                    det.UserId = Convert.ToInt32(dr["UserId"]);

                if (!dr.IsNull("Date"))
                    det.Date = Convert.ToDateTime(dr["Date"]);

                if (!dr.IsNull("Summ"))
                    det.Summ = Convert.ToDouble(dr["Summ"]);

                if (!dr.IsNull("IsDeleted"))
                    det.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

                if (!dr.IsNull("DeleteDate"))
                    det.DeleteDate = Convert.ToDateTime(dr["DeleteDate"]);

                det.DeleteReason = dr["DeleteReason"].ToString();

                if (!dr.IsNull("AbonementGroup"))
                    det.AbonementGroup = Convert.ToInt32(dr["AbonementGroup"]);

                det.CoachName = dr["CoachName"].ToString();

                det.Weekday = dr["Weekday"].ToString();

                det.Time = dr["Time"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region Get Fitness List

        public static ArrayList GetFitnessList(int id, DateTime date1, DateTime date2)
        {

            string sql = "SELECT DISTINCT a.AbonementGroup, ai.[Date], ai.[Summ], ai.[Id], c.FIO, a.[Name] AS AbonementName, u.FIO AS [User], ai.ClientId, ai.AbonementId, ai.UserId, ai.IsDeleted, ai.DeleteDate, ai.DeleteReason, co.Name AS CoachName, ca.Time, ca.Weekday ";
            sql += " FROM AbonementIncome AS ai INNER JOIN Clients AS c ON c.[Id] = ai.CLientId ";
            sql += " INNER JOIN Abonements AS a ON a.[Id] = ai.AbonementId ";
            sql += " LEFT JOIN ClientsAbonements AS ca ON ai.ClientAbonementId = ca.Id ";
            sql += " LEFT JOIN Coaches AS co ON co.[Id] = ca.[CoachId] ";
            sql += " INNER JOIN Users AS u ON u.[Id] = ai.[UserId] ";
            sql += " WHERE ai.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND a.AbonementGroup = 0 ";
            sql += " ORDER BY ai.[Date]";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.AbonementIncome.AbonementIncome_WideDetails det = new DBLayer.AbonementIncome.AbonementIncome_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.AbonementName = dr["AbonementName"].ToString();

                det.FIO = dr["FIO"].ToString();

                det.User = dr["User"].ToString();

                if (!dr.IsNull("ClientId"))
                    det.ClientId = Convert.ToInt32(dr["ClientId"]);

                if (!dr.IsNull("AbonementId"))
                    det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

                if (!dr.IsNull("UserId"))
                    det.UserId = Convert.ToInt32(dr["UserId"]);

                if (!dr.IsNull("Date"))
                    det.Date = Convert.ToDateTime(dr["Date"]);

                if (!dr.IsNull("Summ"))
                    det.Summ = Convert.ToDouble(dr["Summ"]);

                if (!dr.IsNull("IsDeleted"))
                    det.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

                if (!dr.IsNull("DeleteDate"))
                    det.DeleteDate = Convert.ToDateTime(dr["DeleteDate"]);

                det.DeleteReason = dr["DeleteReason"].ToString();

                if (!dr.IsNull("AbonementGroup"))
                    det.AbonementGroup = Convert.ToInt32(dr["AbonementGroup"]);

                det.CoachName = dr["CoachName"].ToString();

                det.Weekday = dr["Weekday"].ToString();

                det.Time = dr["Time"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region Insert

        public void Insert(AbonementIncomeWideDetails det)
        {
            string sql = "INSERT INTO AbonementIncome (ClientId, AbonementId, UserId, [Date], Summ, IsDeleted, ClientAbonementId) ";
            sql += " VALUES (" + det.ClientId.ToString() + ", " + det.AbonementId.ToString() + ", " + det.UserId.ToString() + ", '" + det.Date.ToString("yyyyMMdd") + "', " + det.Summ.ToString().Replace(",", ".") + ", 0, " + det.ClientAbonementId.ToString() + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public void Update(AbonementIncomeWideDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET [ClientId] = " + det.ClientId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET [AbonementId] = " + det.AbonementId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET [ClientAbonementId] = " + det.ClientAbonementId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET [UserId] = " + det.UserId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET [Date] = '" + det.Date.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET [Summ] = " + det.Summ.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM AbonementIncome WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region DeleteByClientAbonement

        public static void DeleteByClientAbonement(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM AbonementIncome WHERE [ClientAbonementId] = " + id.ToString());
        }

        #endregion

        #region SetDelete

        public static void SetDelete(DBLayer.AbonementIncome.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET IsDeleted = 1 WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET DeleteDate = '" + det.DeleteDate.ToString("yyyyMMdd") + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE AbonementIncome SET DeleteReason = '" + det.DeleteReason + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public AbonementIncomeWideDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM AbonementIncome WHERE [Id] = " + id.ToString());

            DBLayer.AbonementIncome.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("Date"))
                det.Date = Convert.ToDateTime(dr["Date"]);

            if (!dr.IsNull("Summ"))
                det.Summ = Convert.ToDouble(dr["Summ"]);

            if (!dr.IsNull("ClientId"))
                det.ClientId = Convert.ToInt32(dr["ClientId"]);

            if (!dr.IsNull("ClientAbonementId"))
                det.ClientAbonementId = Convert.ToInt32(dr["ClientAbonementId"]);

            if (!dr.IsNull("AbonementId"))
                det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

            if (!dr.IsNull("UserId"))
                det.UserId = Convert.ToInt32(dr["UserId"]);

            if (!dr.IsNull("IsDeleted"))
                det.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

            if (!dr.IsNull("DeleteDate"))
                det.DeleteDate = Convert.ToDateTime(dr["DeleteDate"]);

            det.DeleteReason = dr["DeleteReason"].ToString();

            return det;
        }

        #endregion        

        #region MoneyReport

        public static ArrayList MoneyReport(DateTime date1, DateTime date2)
        {
            string sql = "SELECT * FROM Cash as a ";
            sql += " WHERE a.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "'";
            sql += " ORDER BY a.[Date]";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.AbonementIncome.Money_ReportDetails det = new DBLayer.AbonementIncome.Money_ReportDetails();

                det.Date = Convert.ToDateTime(dr["Date"]).Date;

                det.DimensionName = dr["DimensionName"].ToString();

                det.Name = dr["Name"].ToString();

                det.Price = Convert.ToDouble(dr["Price"]);

                det.Quantity = Convert.ToInt32(dr["Quantity"]);

                det.Summ = Convert.ToDouble(dr["Summ"]);

                det.Time = dr["Time"].ToString();

                det.UserName = dr["User"].ToString();

                det.Type = Convert.ToInt32(dr["Type"]);

                det.GroupName = dr["Group"].ToString();

                det.AbonementName = dr["AName"].ToString();

                det.ChargeGroupName = dr["GroupName"].ToString();

                //if (!dr.IsNull("AbonementGroup"))
                //    det.AbonementGroup = Convert.ToInt32(dr["AbonementGroup"]);

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region MoneyReportByDayCharges

        public static List<FinincialDetails> MoneyReportByDayCharges(int month, int year)
        {
            string sql = "SELECT SUM(Summ) AS Charges, Date FROM Cash ";
            sql += " WHERE Month(Date) = " + month.ToString() + " AND Year(Date) = " + year.ToString() + " AND Summ < 0 ";
            sql += " GROUP BY Date ";
            sql += " ORDER BY Date";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            List<FinincialDetails> list = new List<FinincialDetails>();

            foreach (DataRow dr in dt.Rows)
            {
                FinincialDetails det = new FinincialDetails();

                if (!dr.IsNull("Date"))
                    det.Date = Convert.ToDateTime(dr["Date"]);

                if (!dr.IsNull("Charges"))
                    det.Value = Convert.ToDouble(dr["Charges"]);
                else
                    det.Value = 0;

                list.Add(det);
            }

            return list;
        }

        #endregion

        #region MoneyReportByDayIncomes

        public static List<FinincialDetails> MoneyReportByDayIncomes(int month, int year)
        {
            string sql = "SELECT SUM(Summ) AS Incomes, Date FROM Cash ";
            sql += " WHERE Month(Date) = " + month.ToString() + " AND Year(Date) = " + year.ToString() + " AND Summ >= 0 ";
            sql += " GROUP BY Date ";
            sql += " ORDER BY Date";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            List<FinincialDetails> list = new List<FinincialDetails>();

            foreach (DataRow dr in dt.Rows)
            {
                FinincialDetails det = new FinincialDetails();

                if (!dr.IsNull("Date"))
                    det.Date = Convert.ToDateTime(dr["Date"]);

                if (!dr.IsNull("Incomes"))
                    det.Value = Convert.ToDouble(dr["Incomes"]);
                else
                    det.Value = 0;

                list.Add(det);
            }

            return list;
        }

        #endregion

        #region Decline

        public static ArrayList Decline(DateTime date1, DateTime date2)
        {
            string sql = " SELECT * FROM Declines AS a ";
            sql += " WHERE a.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "'";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.AbonementIncome.Money_ReportDetails det = new DBLayer.AbonementIncome.Money_ReportDetails();

                det.Date = Convert.ToDateTime(dr["Date"]).Date;

                det.DimensionName = dr["DimensionName"].ToString();

                det.Name = dr["Name"].ToString();

                det.Price = Convert.ToDouble(dr["Price"]);

                det.Quantity = Convert.ToInt32(dr["Quantity"]);

                det.Summ = Convert.ToDouble(dr["Summ"]);

                det.Time = dr["Time"].ToString();

                det.UserName = dr["User"].ToString();

                det.Type = Convert.ToInt32(dr["Type"]);

                det.GroupName = dr["Group"].ToString();

                if (!dr.IsNull("IsDeleted"))
                    det.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);

                if (!dr.IsNull("DeleteDate"))
                    det.DeleteDate = Convert.ToDateTime(dr["DeleteDate"]);

                det.DeleteReason = dr["DeleteReason"].ToString();

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region UserIncome

        public static ArrayList UserIncome(DateTime date1, DateTime date2)
        {
            string sql = " SELECT * FROM UserIncome AS ui ";
            sql += " WHERE ui.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "'";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.AbonementIncome.UserIncome_ReportDetails det = new DBLayer.AbonementIncome.UserIncome_ReportDetails();

                det.Date = Convert.ToDateTime(dr["Date"]).Date;

                det.DimensionName = dr["DimensionName"].ToString();

                det.Name = dr["Name"].ToString();

                det.Price = Convert.ToDouble(dr["Price"]);

                det.Quantity = Convert.ToInt32(dr["Quantity"]);

                det.Summ = Convert.ToDouble(dr["Summ"]);

                det.Time = dr["Time"].ToString();

                det.UserName = dr["User"].ToString();

                det.Type = Convert.ToInt32(dr["Type"]);

                det.GroupName = dr["Group"].ToString();

                if (!dr.IsNull("ClientSumm"))
                    det.ClientsSumm = Convert.ToDouble(dr["ClientSumm"]);

                if (!dr.IsNull("SalesSumm"))
                    det.SalesSumm = Convert.ToDouble(dr["SalesSumm"]);

                if (!dr.IsNull("ServiceSumm"))
                    det.ServiceSumm = Convert.ToDouble(dr["ServiceSumm"]);

                al.Add(det);
            }

            return al;
        }

        #endregion
    }
}
