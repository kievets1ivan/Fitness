using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class ProductsDetails : DetailsWithName
    {
        public int GroupId = 0;
        public int DimensionId = 0;
        public string Barcode = "";
        public string Description = "";

        public double Price = 0;
    }
}
