using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class ClientsDetails : Details
    {
        public int TypeId = 0;
        public int DocumentId = 0;
        public int SourceId = 0;
        public string FIO = "";
        public string Barcode = "";
        public DateTime RegisterDate = DateTime.MinValue;

        public string DocumentNumber = "";
        public DateTime BirthDate = DateTime.MinValue;

        public string Phone = "";

        public int Sex = 0;
    }
}
