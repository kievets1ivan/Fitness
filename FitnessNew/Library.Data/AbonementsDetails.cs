using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class AbonementsDetails : DetailsWithName
    {
        public double Length = 0;
        public double Cost = 0;

        public int AbonementGroup = 0;
        public int LessonsCount = 0;

        public bool IsSingle = false;

        public int AbonementType = 0;

        public bool IsUnlim = false;
        public bool IsSpecial = false;
        public int CoachId = 0;
        public string Time = string.Empty;

        public string Weekdays = string.Empty;

        public int AdditionalVisits = 0;
    }
}
