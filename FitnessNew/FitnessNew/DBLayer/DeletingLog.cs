using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

namespace FitnessProject.DBLayer
{
    public class DeletingLog
    {
        #region Constructor

        public DeletingLog()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
        }

        #endregion

        #region Details

        public class DeletingLog_Details
        {
            #region Constructor

            public DeletingLog_Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public int Type = 0;
            public string Name = "";
            public DateTime Date = DateTime.MinValue;
            public string User = "";

            #endregion
        }

        #endregion

        #region Insert

        public static void Insert(DBLayer.DeletingLog.DeletingLog_Details det)
        {
            string sql = "INSERT INTO DeletingLog (Type, [Name], [Date], [User]) ";
            sql += " VALUES (" + det.Type.ToString() + ", '" + det.Name.ToString() + "', '" + det.Date.ToString("yyyyMMdd") + "', '" + det.User + "')";

            ZFort.DB.Execute.ExecuteString_void(sql);
        }

        #endregion
       
        #region GetList

        public static DataTable GetList()
        {
            string sql = "SELECT * FROM DeletingLog ORDER BY [Date]";
            
            return ZFort.DB.Execute.ExecuteString_DataTable(sql);
        }

        #endregion

    }
}
