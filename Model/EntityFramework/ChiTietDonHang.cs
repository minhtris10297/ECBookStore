namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        public int ChiTietDonHangID { get; set; }

        public int DonHangID { get; set; }

        public int SachID { get; set; }

        public int SoLuong { get; set; }

        [Column(TypeName = "money")]
        public decimal ThanhTien { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
