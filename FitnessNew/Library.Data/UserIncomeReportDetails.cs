using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class UserIncomeReportDetails : DetailsWithName
    {
        public int Type = 0;
        public DateTime Date = DateTime.MinValue;
        public string Time = "";
        public double Summ = 0;
        public string UserName = "";
        public int Quantity = 0;
        public double Price = 0;
        public string DimensionName = "";

        public string GroupName = "";

        public bool IsDeleted = false;
        public DateTime DeleteDate = DateTime.MinValue;
        public string DeleteReason = "";

        public double ClientsSumm = 0;
        public double ServiceSumm = 0;
        public double SalesSumm = 0;
    }
}
