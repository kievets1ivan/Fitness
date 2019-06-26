using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FitnessProject.DBLayer
{
    public class Inquiries
    {
        #region Details

        public class Details
        {
            #region Constructor

            public Details() { }

            #endregion

            #region Fields

            public int Id = 0;
            public string Number = "";
            public int SupplierId = 0;
            public DateTime Date = DateTime.MinValue;
            public int State = 0;

            #endregion
        }

        #endregion

        #region Details

        public class Inquiries_WideDetails
        {
            #region Constructor

            public Inquiries_WideDetails() { }

            #endregion

            #region Fields

            public int Id = 0;
            public string Number = "";
            public int SupplierId = 0;
            public DateTime Date = DateTime.MinValue;
            public int State = 0;

            public string Supplier = "";

            #endregion
        }

        #endregion

        #region Get List

        public static ArrayList GetList()
        {

            Database.Service1 serv = new FitnessProject.Database.Service1();

            object[] list = serv.Clients_GetList();

            ArrayList al = new ArrayList();

            //System.Windows.Forms.MessageBox.Show("Length: " + list.Length.ToString());

            for (int i = 0; i < list.Length; i++)
            {
                Database.Inquiries_WideDetails det1 = (Database.Inquiries_WideDetails)list[i];

                //System.Windows.Forms.MessageBox.Show(det1.Source);

                DBLayer.Inquiries.Inquiries_WideDetails det = new Inquiries_WideDetails();

                det.Date = det1.Date;

                det.Id = det1.Id;

                det.Number = det1.Number;

                det.State = det1.State;

                det.Supplier = det1.Supplier;

                det.SupplierId = det1.SupplierId;

                al.Add(det);
            }

            return al;
        }

        #endregion               

        #region Insert

        public static void Insert(DBLayer.Inquiries.Details det1)
        {
            Database.Service1 serv = new FitnessProject.Database.Service1();

            Database.Inquiries_Details det = new FitnessProject.Database.Inquiries_Details();

            det.Date = det1.Date;

            det.Id = det1.Id;

            det.Number = det1.Number;

            det.State = det1.State;           

            det.SupplierId = det1.SupplierId;

            serv.Inquiries_Insert(det);
        }

        #endregion

        #region Update

        public static void Update(DBLayer.Inquiries.Details det1)
        {

            Database.Service1 serv = new FitnessProject.Database.Service1();

            Database.Inquiries_Details det = new FitnessProject.Database.Inquiries_Details();

            det.Date = det1.Date;

            det.Id = det1.Id;

            det.Number = det1.Number;

            det.State = det1.State;            

            det.SupplierId = det1.SupplierId;

            serv.Inquiries_Update(det);
        }

        #endregion

        #region Delete

        public static void Delete(int id)
        {
            Database.Service1 serv = new FitnessProject.Database.Service1();

            serv.Inquiries_Delete(id);
        }

        #endregion

        #region GetDetails by Id

        public static DBLayer.Inquiries.Details GetDetails(int id)
        {            
            Database.Service1 serv = new FitnessProject.Database.Service1();

            Database.Inquiries_Details det1 = serv.Inquiries_GetDetails(id);

            DBLayer.Inquiries.Details det = new Details();

            det.Date = det1.Date;

            det.Id = det1.Id;

            det.Number = det1.Number;

            det.State = det1.State;          

            det.SupplierId = det1.SupplierId;

            return det;
        }

        #endregion        
    }
}
