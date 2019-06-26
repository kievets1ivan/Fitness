using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class ServiceSalesWideDetails : ServiceSalesDetails
    {
        public string ServiceName = "";

        public string UserName = "";

        public int DimensionId = 0;
        public string DimensionName = "";

        public double Cost = 0;

        public int Type = 0;
    }
}
