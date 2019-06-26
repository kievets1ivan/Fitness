using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class UserIncomeDetails : Details
    {
        public string User = "";
        public int UserId = 0;
        public double Abonements = 0;
        public double Services = 0;
        public double Goods = 0;
        public double Fitness = 0;
        public double Massage = 0;

        public double Constant = 0;
        public double PercentForClients = 0;
        public double PercentForSales = 0;
        public double PercentForService = 0;

        public double ClientIncome = 0;
        public double GoodIncome = 0;
        public double ServiceIncome = 0;
        public double FitnessIncome = 0;
        public double MassageIncome = 0;
    }
}
