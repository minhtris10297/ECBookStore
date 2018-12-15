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
        KnowledgeStoreEntities db = new KnowledgeStoreEntities();
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
        public int LoginMerchant(string email, string passWord)
        {
            var result = db.Merchants.SingleOrDefault(x => x.Email == email);
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
        public int LoginAdmin(string email, string passWord)
        {
            var result = db.Admins.SingleOrDefault(x => x.TenDangNhap == email);
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
        public int InsertUserFb(Customer Customer)
        {
            var modelMb = db.Customers.SingleOrDefault(m => m.Email == Customer.Email || m.IDFacebook == Customer.IDFacebook);
            if (modelMb == null)
            {
                Customer.NgayTao = System.DateTime.Now;
                db.Customers.Add(Customer);
                db.SaveChanges();
                return Customer.CustomerID;
            }
            else
            {
                if (modelMb.IDFacebook == null)
                {
                    modelMb.IDFacebook = Customer.IDFacebook;
                    db.SaveChanges();
                    return modelMb.CustomerID;
                }
                else if (modelMb.Email == null)
                {
                    modelMb.Email = Customer.Email;
                    db.SaveChanges();
                    return modelMb.CustomerID ;
                }
                return 0;
            }
        }

        public int InsertUserGg(Customer Customer)
        {
            var modelMb = db.Customers.Where(m => m.Email == Customer.Email || m.IDGoogle == Customer.IDGoogle).SingleOrDefault();
            if (modelMb == null)
            {
                Customer.NgayTao = System.DateTime.Now;
                db.Customers.Add(Customer);

                db.SaveChanges();
                return Customer.CustomerID;
            }
            else
            {
                if (modelMb.IDGoogle == null)
                {
                    modelMb.IDGoogle = Customer.IDGoogle;
                    db.SaveChanges();
                    return modelMb.CustomerID;
                }
                else if (modelMb.Email == null)
                {
                    modelMb.Email = Customer.Email;
                    db.SaveChanges();
                    return modelMb.CustomerID;
                }
                return modelMb.CustomerID;
            }
        }
    }
}
