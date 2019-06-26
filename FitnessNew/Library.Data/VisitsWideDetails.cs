using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class VisitsWideDetails : VisitsDetails
    {
        public string ClientName = "";
        public DateTime DateVisit = DateTime.MinValue;

        public DateTime TimeOnVisit = DateTime.MinValue;
        public DateTime TimeOffVisit = DateTime.MinValue;


        public string AbonementName = "";
        public string BoxType = "";

        public string CoachName = "";
    }
}
