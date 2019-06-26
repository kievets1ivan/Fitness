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
    public class ChargeGroup :
        IInsertable<ChargeGroupDetails>,
        IUpdatable<ChargeGroupDetails>,
        IGettableDetailsById<ChargeGroupDetails>,
        IDeletable
    {
        ChargeGroupDetails det = new ChargeGroupDetails();

        #region Get List

        public static ArrayList GetList()
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM ChargeGroup WHERE IsPrimary = 0");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.ChargeGroup.Details det = new DBLayer.ChargeGroup.Details();

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

        public void Insert(ChargeGroupDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("INSERT INTO ChargeGroup ([Name], IsPrimary) VALUES ('" + det.Name + "', 0)");
        }

        #endregion

        #region Update

        public void Update(ChargeGroupDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("UPDATE ChargeGroup SET [Name] = '" + det.Name + "' WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM ChargeGroup WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public ChargeGroupDetails GetDetailsById(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM ChargeGroup WHERE [Id] = " + id.ToString());

            ChargeGroupDetails det = new ChargeGroupDetails();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);

            det.Name = dr["Name"].ToString();

            return det;
        }

        #endregion
    }
}
