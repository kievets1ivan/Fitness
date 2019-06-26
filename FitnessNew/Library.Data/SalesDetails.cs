using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class SalesDetails : Details
    {
        public int ProductId = 0;
        public int ClientId = 0;
        public int UserId = 0;
        public DateTime Date = DateTime.MinValue;
        public string Time = "";
        public int Quantity = 0;

        public bool IsDeleted = false;
        public DateTime DeleteDate = DateTime.MinValue;
        public string DeleteReason = "";
    }
}
