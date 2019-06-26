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
    public class AdvertisingSource :
        IInsertable<DetailsWithName>,
        IUpdatable<DetailsWithName>,
        IGettableDetailsById<DetailsWithName>,
        IDeletable
    {
        DetailsWithName det = new DetailsWithName();

        #region Get List

        public static ArrayList GetList()
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM AdvertisingSource");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.AdvertisingSource.Details det = new DBLayer.AdvertisingSource.Details();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

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

        public void Insert(DetailsWithName det)
        {
            ZFort.DB.Execute.ExecuteString_void("INSERT INTO AdvertisingSource ([Name]) VALUES ('" + det.Name + "')");
        }

        #endregion

        #region Update

        public void Update(DetailsWithName det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE AdvertisingSource SET [Name] = '" + det.Name + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM AdvertisingSource WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public DetailsWithName GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM AdvertisingSource WHERE [Id] = " + id.ToString());

            DetailsWithName det = new DetailsWithName();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.Name = dr["Name"].ToString();

            return det;
        }

        #endregion
    }
}
