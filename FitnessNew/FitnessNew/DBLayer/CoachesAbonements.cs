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
    public class CoachesAbonements :
        IInsertable<CoachesAbonementsWideDetails>,
        IDeletable
    {
        CoachesAbonementsWideDetails det = new CoachesAbonementsWideDetails();

        #region Get List

        public static ArrayList GetList()
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT ca.[Id], a.[Id] AS AbonementId, c.[Id] AS CoachId, a.[Name] AS Abonement, c.[Name] AS Coach FROM Coaches AS c INNER JOIN CoachesAbonements AS ca ON c.[Id] = ca.CoachId INNER JOIN Abonements AS a ON a.[Id] = ca.AbonementId");

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.CoachesAbonements.CoachesAbonements_WideDetails det = new DBLayer.CoachesAbonements.CoachesAbonements_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.AbonementName = dr["Abonement"].ToString();

                det.CoachName = dr["Coach"].ToString();

                if (!dr.IsNull("AbonementId"))
                    det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

                if (!dr.IsNull("CoachId"))
                    det.CoachId = Convert.ToInt32(dr["CoachId"]);

                al.Add(det);
            }

            return al;
        }

        #endregion

        #region GetListByCoach

        public static ArrayList GetListByCoach(int id)
        {
            DataTable dt = ZFort.DB.Execute.ExecuteString_DataTable("SELECT ca.[Id], a.[Id] AS AbonementId, c.[Id] AS CoachId, a.[Name] AS Abonement, c.[Name] AS Coach FROM Coaches AS c INNER JOIN CoachesAbonements AS ca ON c.[Id] = ca.CoachId INNER JOIN Abonements AS a ON a.[Id] = ca.AbonementId WHERE c.[Id] = " + id.ToString());

            ArrayList al = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                DBLayer.CoachesAbonements.CoachesAbonements_WideDetails det = new DBLayer.CoachesAbonements.CoachesAbonements_WideDetails();

                if (!dr.IsNull("Id"))
                    det.Id = Convert.ToInt32(dr["Id"]);

                det.AbonementName = dr["Abonement"].ToString();

                det.CoachName = dr["Coach"].ToString();

                if (!dr.IsNull("AbonementId"))
                    det.AbonementId = Convert.ToInt32(dr["AbonementId"]);

                if (!dr.IsNull("CoachId"))
                    det.CoachId = Convert.ToInt32(dr["CoachId"]);

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

        public void Insert(CoachesAbonementsWideDetails det)
        {
            ZFort.DB.Execute.ExecuteString_void("INSERT INTO CoachesAbonements (AbonementId, CoachId) VALUES (" + det.AbonementId.ToString() + ", " + det.CoachId.ToString() + ")");
        }

        #endregion        

        #region Delete

        public void Delete(int id)
        {
            ZFort.DB.Execute.ExecuteString_void("DELETE FROM CoachesAbonements WHERE [Id] = " + id.ToString());
        }

        #endregion       
    }
}
