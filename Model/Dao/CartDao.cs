using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CartDao
    {
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
        public bool CheckAvailableBook(int idBook,int quantity)
        {
            if (db.Saches.Where(m => m.SachID == idBook & quantity >= m.SoLuong) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
