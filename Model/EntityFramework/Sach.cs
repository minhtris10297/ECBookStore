namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietSachMerchants = new HashSet<ChiTietSachMerchant>();
        }

        public int SachID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSach { get; set; }

        [Required]
        [StringLength(50)]
        public string TacGia { get; set; }

        public int NhaXuatBanID { get; set; }

        public DateTime NgayXuatBan { get; set; }

        public int SoTrang { get; set; }

        public int? LoaiBiaID { get; set; }

        public int? MerchantID { get; set; }

        public bool? TrangThai { get; set; }

        public decimal GiaTien { get; set; }

        public decimal? GiaKhuyenMai { get; set; }

        public int? HinhAnhID { get; set; }

        [Column(TypeName = "text")]
        public string MoTa { get; set; }

        public int? SoLuong { get; set; }

        public int TheLoaiID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietSachMerchant> ChiTietSachMerchants { get; set; }

        public virtual HinhAnh HinhAnh { get; set; }

        public virtual LoaiBia LoaiBia { get; set; }

        public virtual Merchant Merchant { get; set; }

        public virtual NhaXuatBan NhaXuatBan { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
