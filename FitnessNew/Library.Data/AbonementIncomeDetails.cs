using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class AbonementIncomeDetails : Details
    {
        public int ClientId = 0;
        public int AbonementId = 0;
        public int UserId = 0;
        public DateTime Date = DateTime.MinValue;
        public double Summ = 0;

        public bool IsDeleted = false;
        public DateTime DeleteDate = DateTime.MinValue;
        public string DeleteReason = "";

        public int ClientAbonementId = 0;
    }
}
