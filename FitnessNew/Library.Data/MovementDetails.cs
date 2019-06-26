using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class MovementDetails : Details
    {
        public int Arrived = 0;
        public int Dispatched = 0;
        public DateTime Date = DateTime.MinValue;

    }
}
