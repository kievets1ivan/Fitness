using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class InquiriesDetails : Details
    {
        public string Number = "";
        public int SupplierId = 0;
        public DateTime Date = DateTime.MinValue;
        public int State = 0;
    }
}
