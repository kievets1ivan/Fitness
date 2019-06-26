using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class UsersDetails : Details
    {
        public string FIO = "";
        public string JobTitle = "";
        public string Login = "";
        public string Password = "";
        public string Barcode = "";
        public bool IsAdmin = false;
    }
}
