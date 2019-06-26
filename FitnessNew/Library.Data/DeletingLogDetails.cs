using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class DeletingLogDetails : DetailsWithName
    {
        public int Type = 0;
        public DateTime Date = DateTime.MinValue;
        public string User = "";
    }
}
