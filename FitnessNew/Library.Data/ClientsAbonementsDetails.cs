using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class ClientsAbonementsDetails : Details
    {
        public int ClientId = 0;
        public int AbonementId = 0;
        public DateTime DateStart = DateTime.MinValue;
        public DateTime DateFinish = DateTime.MinValue;

        public int VisitsCount = -1;
        public int AdditionalCount = -1;

        public int CoachId = 0;
        public string Weekdays = string.Empty;
        public string Time = string.Empty;
    }
}
