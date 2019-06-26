using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class ChargesDetails : DetailsWithName
    {
        public int GroupId = 0;
        public double Summ = 0;
        public DateTime Date = DateTime.MinValue;
        public int AdminstratorId = 0;
    }
}
