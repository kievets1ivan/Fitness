using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;

namespace Library.Logic
{
    public interface IDeletable
    {
        void Delete(int id);
    }
}
