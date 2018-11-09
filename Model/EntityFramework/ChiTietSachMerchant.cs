namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietSachMerchant")]
    public partial class ChiTietSachMerchant
    {
        [Key]
        public int KhoSachMerchantID { get; set; }

        public int? SachID { get; set; }

        public int? MerchantID { get; set; }

        [Column(TypeName = "money")]
        public decimal DonGia { get; set; }

        [Column(TypeName = "money")]
        public decimal? GiaKhuyenMai { get; set; }

        public int SoLuong { get; set; }

        public DateTime NgayTao { get; set; }

        public bool TrangThai { get; set; }

        public virtual Merchant Merchant { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
