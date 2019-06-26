using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class CoachesDetails : DetailsWithName
    {
        public string Phone = string.Empty;
        public DateTime HireDate = DateTime.MinValue;
        public DateTime FireDate = DateTime.MinValue;

        public int Sex = -1;
        public int Type = -1;
        public DateTime BirthDate = DateTime.MinValue;
    }
}
