using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class UserRateDetails : Details
    {
        public int UserId = 0;
        public double Constant = 0;
        public double PercentForClients = 0;
        public double PercentForSales = 0;
        public double PercentForService = 0;
        public double PercentForFitness = 0;
        public double PercentForMassage = 0;
    }
}
