using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Users
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public string FIO = "";
            public string JobTitle = "";
            public string Login = "";
            public string Password = "";
            public string Barcode = "";
            public bool IsAdmin = false;

            #endregion
        }

        #endregion

        #region UserIncome_Details

        public class UserIncome_Details
        {
            #region Constructor

            public UserIncome_Details() { }

            #endregion

            #region Fields

            public string User = "";
            public int UserId = 0;
            public double Abonements = 0;
            public double Services = 0;
            public double Goods = 0;
            public double Fitness = 0;
            public double Massage = 0;

            public double Constant = 0;
            public double PercentForClients = 0;
            public double PercentForSales = 0;
            public double PercentForService = 0;

            public double ClientIncome = 0;
            public double GoodIncome = 0;
            public double ServiceIncome = 0;
            public double FitnessIncome = 0;
            public double MassageIncome = 0;

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList()
        {

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM Users");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Users.Details det = new DBLayer.Users.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.FIO = dr["FIO"].ToString();

                det.Barcode = dr["Barcode"].ToString();

                det.JobTitle = dr["JobTitle"].ToString();

                det.Login = dr["Login"].ToString();

                det.Password = dr["Password"].ToString();

                if (!dr.IsNull("IsAdmin"))
                    det.IsAdmin = Convert.ToBoolean(dr["IsAdmin"]);


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

        public static void Insert(DBLayer.Users.Details det)
        {
            string sql = "INSERT INTO Users (FIO, JobTitle, Login, Password, Barcode, IsAdmin) ";
            sql += " VALUES ('" + det.FIO + "', '" + det.JobTitle + "', '" + det.Login + "', '" + det.Password + "', '" + det.Barcode + "', " + Convert.ToInt32(det.IsAdmin).ToString() + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.Users.Details det)
        {

            ZFort.DB.Execute.ExecuteString_void("UPDATE Users SET [FIO] = '" + det.FIO + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Users SET [JobTitle] = '" + det.JobTitle + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Users SET [Login] = '" + det.Login + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Users SET [IsAdmin] = " + Convert.ToInt32(det.IsAdmin).ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Users SET [Password] = '" + det.Password + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Users SET [Barcode] = '" + det.Barcode + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Users WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.Users.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Users WHERE [Id] = " + id.ToString());

            DBLayer.Users.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            if (!dr.IsNull("IsAdmin"))
                det.IsAdmin = Convert.ToBoolean(dr["IsAdmin"]);

            det.FIO = dr["FIO"].ToString();

            det.JobTitle = dr["JobTitle"].ToString();

            det.Login = dr["Login"].ToString();

            det.Password = dr["Password"].ToString();

            det.Barcode = dr["Barcode"].ToString();

            return det;
        }

        #endregion

        #region Authorize

        public static DBLayer.Users.Details Authorize(string login, string pwd)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Users WHERE [login] = '" + login + "' AND [Password] = '" + pwd + "'");

            DBLayer.Users.Details det = new Details();

            if (dr != null)
            {

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                if (!dr.IsNull("IsAdmin"))
                    det.IsAdmin = Convert.ToBoolean(dr["IsAdmin"]);

                det.FIO = dr["FIO"].ToString();

                det.JobTitle = dr["JobTitle"].ToString();

                det.Login = dr["Login"].ToString();

                det.Password = dr["Password"].ToString();

                det.Barcode = dr["Barcode"].ToString();
            }

            return det;
        }

        #endregion

        #region GetUserIncome

        public static ArrayList GetUserIncome(DateTime date1, DateTime date2)
        {
            string sql = " SELECT DISTINCT a.[User], a.UserId, ISNULL((SELECT Sum(Summ) FROM Cash AS c INNER JOIN Abonements AS ab ON c.AbonementId = ab.[Id] WHERE ab.[AbonementGroup] = 0 AND Type = 0 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0) AS Abonements, ";
            sql += " ISNULL((SELECT Sum(Summ) FROM Cash AS c INNER JOIN Abonements AS ab ON c.AbonementId = ab.[Id] WHERE ab.[AbonementGroup] = 1 AND Type = 0 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0) AS AbonementFitness, ";
            sql += " ISNULL((SELECT Sum(Summ) FROM Cash AS c WHERE Type = 1 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0) AS Goods, ";
            sql += " ISNULL((SELECT Sum(Summ) FROM Cash AS c INNER JOIN Services AS s ON c.AbonementId = s.[Id] WHERE c.Type = 2 AND s.[Type] = 0 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0) AS Services, ";
            sql += " ISNULL((SELECT Sum(Summ) FROM Cash AS c INNER JOIN Services AS s ON c.AbonementId = s.[Id] WHERE c.Type = 2 AND s.[Type] = 1 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0) AS ServiceMassage, ";
            sql += " ur.Constant, ur.PercentForClients, ur.PercentForSales, ur.PercentForService, ur.PercentForFitness, ur.PercentForMassage, ";
            sql += " ((ur.PercentForClients * ISNULL((SELECT Sum(Summ) FROM Cash AS c INNER JOIN Abonements AS ab ON c.AbonementId = ab.[Id] WHERE ab.[AbonementGroup] = 0 AND Type = 0 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0))/100) AS ClientIncome, ";
            sql += " ((ur.PercentForClients * ISNULL((SELECT Sum(Summ) FROM Cash AS c INNER JOIN Abonements AS ab ON c.AbonementId = ab.[Id] WHERE ab.[AbonementGroup] = 1 AND Type = 0 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0))/100) AS ClientFitnessIncome, ";
            sql += " ((ur.PercentForSales * ISNULL((SELECT Sum(Summ) FROM Cash AS c WHERE Type = 1 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0))/100) AS GoodIncome, ";
            sql += " ((ur.PercentForService * ISNULL((SELECT Sum(Summ) FROM Cash AS c INNER JOIN Services AS s ON c.AbonementId = s.[Id] WHERE c.Type = 2 AND s.[Type] = 0 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0))/100) AS ServiceIncome, ";
            sql += " ((ur.PercentForMassage * ISNULL((SELECT Sum(Summ) FROM Cash AS c INNER JOIN Services AS s ON c.AbonementId = s.[Id] WHERE c.Type = 2 AND s.[Type] = 1 AND c.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND c.UserId = a.UserId GROUP BY [User]), 0))/100) AS ServiceMassageIncome ";
            sql += " FROM Cash AS a ";
            sql += " LEFT JOIN Users AS u ON u.[Id] = a.UserId ";
            sql += " LEFT JOIN UserRate AS ur ON ur.[UserId] = u.[Id] ";
            sql += " WHERE a.[Date] BETWEEN '" + date1.ToString("yyyyMMdd") + "' AND '" + date2.ToString("yyyyMMdd") + "' AND [User] <> '' ";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Users.UserIncome_Details det = new DBLayer.Users.UserIncome_Details();

                if (!dr.IsNull("Abonements"))
                    det.Abonements = Convert.ToDouble(dr["Abonements"]);

                if (!dr.IsNull("ClientIncome"))
                    det.ClientIncome = Convert.ToDouble(dr["ClientIncome"]);

                if (!dr.IsNull("Constant"))
                    det.Constant = Convert.ToDouble(dr["Constant"]);

                if (!dr.IsNull("GoodIncome"))
                    det.GoodIncome = Convert.ToDouble(dr["GoodIncome"]);

                if (!dr.IsNull("Goods"))
                    det.Goods = Convert.ToDouble(dr["Goods"]);

                if (!dr.IsNull("AbonementFitness"))
                    det.Fitness = Convert.ToDouble(dr["AbonementFitness"]);

                if (!dr.IsNull("ServiceMassage"))
                    det.Massage = Convert.ToDouble(dr["ServiceMassage"]);

                if (!dr.IsNull("PercentForClients"))
                    det.PercentForClients = Convert.ToDouble(dr["PercentForClients"]);

                if (!dr.IsNull("PercentForSales"))
                    det.PercentForSales = Convert.ToDouble(dr["PercentForSales"]);

                if (!dr.IsNull("PercentForService"))
                    det.PercentForService = Convert.ToDouble(dr["PercentForService"]);

                if (!dr.IsNull("ServiceIncome"))
                    det.ServiceIncome = Convert.ToDouble(dr["ServiceIncome"]);

                if (!dr.IsNull("ClientFitnessIncome"))
                    det.FitnessIncome = Convert.ToDouble(dr["ClientFitnessIncome"]);

                if (!dr.IsNull("ServiceMassageIncome"))
                    det.MassageIncome = Convert.ToDouble(dr["ServiceMassageIncome"]);

                if (!dr.IsNull("Services"))
                    det.Services = Convert.ToDouble(dr["Services"]);

                det.User = dr["User"].ToString();

                if (!dr.IsNull("UserId"))
                    det.UserId = Convert.ToInt32(dr["UserId"]);

                al.Add(det);
            }

            return al;
        }


        #endregion
    }
}
