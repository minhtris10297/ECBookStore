using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeStore.Common;
using Model.EntityFramework;

namespace Model.Dao
{
    public class UserDao
    {
        KnowledgeStoreContext db = new KnowledgeStoreContext();
        public int Login(string email, string passWord)
        {
            var result = db.Customers.SingleOrDefault(x => x.Email == email);
            if (result == null)
            {
                return -1;
            }
            else
            {
                if (!result.TrangThai)
                {
                    return 0;
                }
                else
                {
                    if (result.MatKhauMaHoa == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
    }
}
