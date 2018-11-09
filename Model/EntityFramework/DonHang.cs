namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            DanhGiaCuaCustomers = new HashSet<DanhGiaCuaCustomer>();
            LichSuCus = new HashSet<LichSuCu>();
            LichSuMers = new HashSet<LichSuMer>();
        }

        public int DonHangID { get; set; }

        public int? CustomerID { get; set; }

        public int? MerchantID { get; set; }

        public DateTime? NgayXuat { get; set; }

        [Column(TypeName = "money")]
        public decimal TongTien { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        public int TinhTrangDonHangID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGiaCuaCustomer> DanhGiaCuaCustomers { get; set; }

        public virtual Merchant Merchant { get; set; }

        public virtual TinhTrangDonHang TinhTrangDonHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuCu> LichSuCus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuMer> LichSuMers { get; set; }
    }
}
