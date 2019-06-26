using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class VisitsDetails : Details
    {
        public int ClientId = 0;
        public DateTime Date = DateTime.MinValue;
        public string Time = "";
        public int Type = 0;

        public int Number = 0;
        public int BoxId = 0;

        public string TimeOff = "";
        public bool WithoutKey = false;

        public int CoachId = 0;

        public bool IsSubstitution = false;
    }
}
