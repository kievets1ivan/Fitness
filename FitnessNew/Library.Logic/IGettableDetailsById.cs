using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;

namespace Library.Logic
{
    public interface IGettableDetailsById<T> where T : Details
    {
        T GetDetailsById(int det);
    }
}
