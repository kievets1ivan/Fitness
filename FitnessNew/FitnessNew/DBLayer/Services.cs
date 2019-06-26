using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Services
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int DimensionId = 0;
            public string Name = "";
            public double CostPerUnit = 0;
            public int Type = 0;

            #endregion
        }

        #endregion

        #region Details

        public class Services_WideDetails
        {
            #region Constructor

            public Services_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int DimensionId = 0;
            public string Name = "";
            public double CostPerUnit = 0;
            public string DimensionName = "";
            public int Type = 0;

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList()
        {

            string sql = "SELECT s.[Name], s.[Id], s.DimensionId, d.[Name] AS Dimension, s.CostPerUnit, s.[Type] ";
            sql += " FROM Services AS s  ";
            sql += " INNER JOIN Dimensions AS d ON d.[Id] = s.[DimensionId] ";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.Services.Services_WideDetails det = new DBLayer.Services.Services_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.Name = dr["Name"].ToString();

                if (!dr.IsNull("CostPerUnit"))
                    det.CostPerUnit = Convert.ToDouble(dr["CostPerUnit"]);

                if (!dr.IsNull("DimensionId"))
                    det.DimensionId = Convert.ToInt32(dr["DimensionId"]);

                det.DimensionName = dr["Dimension"].ToString();

                if (!dr.IsNull("Type"))
                    det.Type = Convert.ToInt32(dr["Type"]);

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region Insert

        public static int Insert(DBLayer.Services.Details det)
        {
            string sql = "INSERT INTO Services (DimensionId, [Name], CostPerUnit, [Type]) ";
            sql += " VALUES (" + det.DimensionId.ToString() + ", '" + det.Name + "', " + det.CostPerUnit.ToString().Replace(",", ".") + ", " + det.Type.ToString() + ")";

            ZFort.DB.Execute.ExecuteString_void(sql);

            return (int)ZFort.DB.Execute.ExecuteString_Scalar("SELECT Max(ID) FROM Services");
        }

        #endregion

        #region Update

        public static void Update(DBLayer.Services.Details det)
        {

            ZFort.DB.Execute.ExecuteString_void("UPDATE Services SET [DimensionId] = " + det.DimensionId.ToString() + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Services SET [CostPerUnit] = " + det.CostPerUnit.ToString().Replace(",", ".") + " WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Services SET [Name] = '" + det.Name + "' WHERE [Id] = " + det.Id.ToString());

            ZFort.DB.Execute.ExecuteString_void("UPDATE Services SET [Type] = " + det.Type.ToString() + " WHERE [Id] = " + det.Id.ToString());
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM Services WHERE [Id] = " + id.ToString());
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.Services.Details GetDetails(int id)
        {
            DataRow dr = ZFort.DB.Execute.ExecuteString_DataRow("SELECT * FROM Services WHERE [Id] = " + id.ToString());

            DBLayer.Services.Details det = new Details();

            if (!dr.IsNull("Id"))
                det.Id = Convert.ToInt32(dr["Id"]);


            det.Name = dr["Name"].ToString();


            if (!dr.IsNull("CostPerUnit"))
                det.CostPerUnit = Convert.ToDouble(dr["CostPerUnit"]);

            if (!dr.IsNull("DimensionId"))
                det.DimensionId = Convert.ToInt32(dr["DimensionId"]);

            if (!dr.IsNull("Type"))
                det.Type = Convert.ToInt32(dr["Type"]);

            return det;
        }

        #endregion        
    }
}
