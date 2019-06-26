using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class ArrivalElementDetails :Details
    {
        public int ArrivalId = 0;
        public int ProductId = 0;
        public double Price = 0;
        public double Quantity = 0;

        public DateTime Date = DateTime.MinValue;
        public int SupplierId = 0;

        public int ChargeId = 0;
    }
}
