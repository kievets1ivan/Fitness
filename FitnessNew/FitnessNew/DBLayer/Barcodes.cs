using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Barcodes
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public string Name = "";

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList()
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT * FROM Barcodes");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Barcodes.Details det = new DBLayer.Barcodes.Details();

                det.Name = dr["Barcode"].ToString();

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

        public static void Insert(DBLayer.Barcodes.Details det)
        {
            ZFort.DB.Execute.ExecuteString_void("INSERT INTO Barcodes (Barcode) VALUES ('" + det.Name + "')");
        }

        #endregion

        #region Check

        public static bool Check(string code)
        {
            string sql = "SELECT * FROM Barcodes WHERE Barcode = '" + code + "'";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            return (dt.Rows.Count > 0);
        }

        #endregion

        #region RemoveAll

        public static void RemoveAll()
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Barcodes");
        }

        #endregion

    }
}
