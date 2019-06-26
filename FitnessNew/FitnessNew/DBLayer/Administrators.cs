﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Library.Data;
using Library.Logic;

namespace FitnessProject.DBLayer
{
    public class Administrators :
        IInsertable<AdministratorsDetails>,
        IUpdatable<AdministratorsDetails>,
        IGettableDetailsById<AdministratorsDetails>,
        IDeletable
    {
        AdministratorsDetails det = new AdministratorsDetails();

        #region Get List

        public static ArrayList GetList()
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM Administrators");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Administrators.Details det = new DBLayer.Administrators.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.FIO = dr["FIO"].ToString();

                if (!dr.IsNull("IsTired"))
                    det.IsTired = Convert.ToBoolean(dr["IsTired"]);

                al.Add(det);
            }

            return al;


        }

        #endregion

        #region Get List

        public static ArrayList GetList(int id)
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM Administrators AS a INNER JOIN UserAdministrator AS ua ON a.[Id] = ua.[AdministratorId] WHERE ua.UserId = " + id.ToString());

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Administrators.Details det = new DBLayer.Administrators.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.FIO = dr["FIO"].ToString();

                if (!dr.IsNull("IsTired"))
                    det.IsTired = Convert.ToBoolean(dr["IsTired"]);

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

        public void Insert(AdministratorsDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("INSERT INTO Administrators ([FIO], IsTired) VALUES ('" + det.FIO + "', " + Convert.ToInt32(det.IsTired).ToString() + ")");
        }

        #endregion

        #region Update

        public void Update(AdministratorsDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Administrators SET [FIO] = '" + det.FIO + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Administrators SET [IsTired] = " + Convert.ToInt32(det.IsTired).ToString() + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Administrators WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public AdministratorsDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Administrators WHERE [Id] = " + id.ToString());

            AdministratorsDetails det = new AdministratorsDetails();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.FIO = dr["FIO"].ToString();

            det.IsTired = Convert.ToBoolean(dr["IsTired"]);

            return det;
        }

        #endregion
    }
}
