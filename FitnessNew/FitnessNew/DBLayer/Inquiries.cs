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
    public class Inquiries :
        IInsertable<InquiriesWideDetails>,
        IUpdatable<InquiriesWideDetails>,
        IGettableDetailsById<InquiriesWideDetails>,
        IDeletable
    {
        InquiriesWideDetails det = new InquiriesWideDetails();

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

        public void Insert(InquiriesWideDetails det1)
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

        public void Update(InquiriesWideDetails det1)
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

        public void Delete(int id)
        {
            Database.Service1 serv = new FitnessProject.Database.Service1();

            serv.Inquiries_Delete(id);
        }

        #endregion

        #region GetDetails by Id

        public InquiriesWideDetails GetDetailsById(int id)
        {            
            Database.Service1 serv = new FitnessProject.Database.Service1();

            Database.Inquiries_Details det1 = serv.Inquiries_GetDetails(id);

            InquiriesWideDetails det = new InquiriesWideDetails();

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
