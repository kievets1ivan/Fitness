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
    public class Suppliers :
        IInsertable<SuppliersDetails>,
        IUpdatable<SuppliersDetails>,
        IGettableDetailsById<SuppliersDetails>,
        IDeletable
    {
        SuppliersDetails det = new SuppliersDetails();

        #region Get List

        public static ArrayList GetList()
        {

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM Suppliers");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Suppliers.Details det = new DBLayer.Suppliers.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                det.Director = dr["Director"].ToString();

                det.Phone = dr["Phone"].ToString();

                det.Fax = dr["Fax"].ToString();

                if (!dr.IsNull("Type"))
                    det.Type = Convert.ToInt32(dr["Type"]);

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

        public void Insert(SuppliersDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("INSERT INTO Suppliers ([Type], [Name], Director, Phone, Fax) VALUES (" + det.Type.ToString() + ", '" + det.Name + "', '" + det.Director + "', '" + det.Phone + "', '" + det.Fax + "')");
        }

        #endregion

        #region Update

        public void Update(SuppliersDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE Suppliers SET [Name] = '" + det.Name + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Suppliers SET [Director] = '" + det.Director + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Suppliers SET [Phone] = '" + det.Phone + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Suppliers SET [Fax] = '" + det.Fax + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Suppliers SET [Type] = " + det.Type.ToString() + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Suppliers WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public SuppliersDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Suppliers WHERE [Id] = " + id.ToString());

            SuppliersDetails det = new SuppliersDetails();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.Name = dr["Name"].ToString();

            det.Director = dr["Director"].ToString();

            det.Fax = dr["Fax"].ToString();

            det.Phone = dr["Phone"].ToString();

            if (!dr.IsNull("Type"))
                det.Type = Convert.ToInt32(dr["Type"]);

            return det;
        }

        #endregion
    }
}
