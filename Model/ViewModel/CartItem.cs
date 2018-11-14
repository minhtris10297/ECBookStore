using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    [Serializable]
    public class CartItem
    {
        public Sach Sach { set; get; }
        public int Quantity { set; get; }
    }
}
