using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class MoneyReportDetails : Details
    {
        public int Type = 0;
        public DateTime Date = DateTime.MinValue;
        public string Time = "";
        public double Summ = 0;
        public string UserName = "";
        public string Name = "";
        public int Quantity = 0;
        public double Price = 0;
        public string DimensionName = "";

        public string GroupName = "";

        public bool IsDeleted = false;
        public DateTime DeleteDate = DateTime.MinValue;
        public string DeleteReason = "";

        public string AbonementName = "";

        public string ChargeGroupName = "";

        public int AbonementGroup = 0;
    }
}
