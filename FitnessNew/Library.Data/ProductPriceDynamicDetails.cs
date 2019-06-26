using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class ProductPriceDynamicDetails : Details
    {
        public int ProductId = 0;

        public double Price = 0;
        public DateTime DateStart = DateTime.MinValue;
        public DateTime DateFinish = DateTime.MinValue;
    }
}
