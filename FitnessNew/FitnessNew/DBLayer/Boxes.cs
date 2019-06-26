using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Boxes
    {
       
        #region Get List

        public static int GetList(int type, int sex)
        {
            return (int)ZFort.DB.Execute.ExecuteString_Scalar("SELECT Count(*) As Cnt FROM Boxes WHERE [Type] = " + type.ToString() + " AND [Sex] = " + sex.ToString());
        }

        #endregion

        #region Get Id

        public static int GetId(int type, int sex, int number)
        {
            string sql = "SELECT [Id] As Cnt FROM Boxes WHERE [Sex] = " + sex.ToString() + " AND [Number] = " + number.ToString();

            return (int)ZFort.DB.Execute.ExecuteString_Scalar(sql);
        }

        #endregion

        #region GetReserved

        public static ArrayList GetReserved(int type, int sex)
        {
            string sql = "SELECT [Number] FROM Boxes WHERE [Sex] = " + sex.ToString() + " AND [ID] IN (SELECT BoxId FROM Visits WHERE TimeOff = '')";

            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable(sql);

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                al.Add(Convert.ToInt32(dr["Number"]));
            }

            return al;
        }

        #endregion

        #region GetNumber

        public static int GetNumber(int id)
        {
            string sql = "SELECT [Number] FROM Boxes WHERE [Id] = " + id.ToString();

            int number = (int) ZFort.DB.Execute.ExecuteString_Scalar(sql);

            return number;
        }

        #endregion

    }
}
