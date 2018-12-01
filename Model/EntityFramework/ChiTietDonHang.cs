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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChiTietDonHang()
        {
            DanhGiaCuaCustomers = new HashSet<DanhGiaCuaCustomer>();
        }

        public int ChiTietDonHangID { get; set; }

        public int DonHangID { get; set; }

        public int SachID { get; set; }

        public int MerchantID { get; set; }

        public DateTime? NgayXuat { get; set; }

        public int SoLuong { get; set; }

        public decimal ThanhTien { get; set; }

        public bool? TrangThaiDanhGia { get; set; }

        public bool? TrangThai { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual Sach Sach { get; set; }

        public virtual Merchant Merchant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGiaCuaCustomer> DanhGiaCuaCustomers { get; set; }
    }
}
