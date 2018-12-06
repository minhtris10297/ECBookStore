namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Merchant")]
    public partial class Merchant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Merchant()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietSachMerchants = new HashSet<ChiTietSachMerchant>();
            DanhGiaCuaMerchants = new HashSet<DanhGiaCuaMerchant>();
            LichSuGiaoDichXuCuaMerchants = new HashSet<LichSuGiaoDichXuCuaMerchant>();
            LichSuMerchants = new HashSet<LichSuMerchant>();
            Saches = new HashSet<Sach>();
        }

        public int MerchantID { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(100)]
        public string DiaChi { get; set; }

        public int GioiTinhID { get; set; }

        [Required]
        [StringLength(120)]
        public string TenCuaHang { get; set; }

        public int SoLuongKIPXu { get; set; }

        public DateTime NgayTao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietSachMerchant> ChiTietSachMerchants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGiaCuaMerchant> DanhGiaCuaMerchants { get; set; }

        public virtual GioiTinh GioiTinh { get; set; }

        public virtual GioiTinh GioiTinh1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuGiaoDichXuCuaMerchant> LichSuGiaoDichXuCuaMerchants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuMerchant> LichSuMerchants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
