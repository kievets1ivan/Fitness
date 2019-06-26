using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class UserVisitsDetails : Details
    {
        public int UserId = 0;
        public DateTime Date = DateTime.MinValue;
        public string TimeOn = "";
        public string TimeOff = "";
    }
}
