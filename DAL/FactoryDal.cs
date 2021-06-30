using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        static IDal idal = null;
        public static IDal GetDAL()
        {
            if (idal == null)
                idal = new Dal_XML_imp();
            return idal;
        }
    }
}
