using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class BookHomeVM
    {
        public int SachID { get; set; }
        
        public string TenSach { get; set; }

        public bool? TrangThai { get; set; }

        public decimal GiaTien { get; set; }

        public decimal? GiaKhuyenMai { get; set; }

        public string MoTa { get; set; }

        public int? SoLuong { get; set; }

        public string TenTheLoai { get; set; }

        public string TenCuaHang { get; set; }
    }
}
