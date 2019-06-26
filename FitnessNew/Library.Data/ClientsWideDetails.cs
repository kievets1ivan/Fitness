using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class ClientsWideDetails : ClientsDetails
    {
        public string TypeName = "";
        public string DocumentName = "";
        public string SourceName = "";

        public string AbonementName = "";
        public DateTime FinishDate = DateTime.MinValue;

        public int VisitsCount = -1;
        public int AVisitsCount = -1;

        public string CoachName = string.Empty;
    }
}
