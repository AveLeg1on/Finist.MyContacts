using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finist.MyContacts.Model
{
    class DBContextGet : MyDBContext
    {
        private static DBContextGet _istance;

        public static DBContextGet GetContext()
        {
            if (_istance == null)
            {_istance=new DBContextGet();   

            }

            return _istance;

        }
    }
}
