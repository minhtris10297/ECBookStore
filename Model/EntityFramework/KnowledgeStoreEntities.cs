namespace Model.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KnowledgeStoreEntities : DbContext
    {
        public KnowledgeStoreEntities()
            : base("name=KnowledgeStoreEntities")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietSachMerchant> ChiTietSachMerchants { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DanhGiaCuaCustomer> DanhGiaCuaCustomers { get; set; }
        public virtual DbSet<DanhGiaCuaMerchant> DanhGiaCuaMerchants { get; set; }
        public virtual DbSet<DoanhThu> DoanhThus { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GiaTriKIPXu> GiaTriKIPXus { get; set; }
        public virtual DbSet<GioiTinh> GioiTinhs { get; set; }
        public virtual DbSet<HinhAnh> HinhAnhs { get; set; }
        public virtual DbSet<HoaHong> HoaHongs { get; set; }
        public virtual DbSet<LichSuCustomer> LichSuCustomers { get; set; }
        public virtual DbSet<LichSuGiaoDichXuCuaMerchant> LichSuGiaoDichXuCuaMerchants { get; set; }
        public virtual DbSet<LichSuMerchant> LichSuMerchants { get; set; }
        public virtual DbSet<LoaiBia> LoaiBias { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<NangTin> NangTins { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }
        public virtual DbSet<TinhTrangDonHang> TinhTrangDonHangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.MatKhauMaHoa)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChiTietSachMerchant>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTietSachMerchant>()
                .Property(e => e.GiaKhuyenMai)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.IDGoogle)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.IDFacebook)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.MatKhauMaHoa)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.DanhGiaCuaCustomers)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.DanhGiaCuaMerchants)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.LichSuCustomers)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DanhGiaCuaCustomer>()
                .Property(e => e.NoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<DanhGiaCuaMerchant>()
                .Property(e => e.NgayDanhGia)
                .IsFixedLength();

            modelBuilder.Entity<DoanhThu>()
                .Property(e => e.SoTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.DanhGiaCuaCustomers)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.DoanhThus)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.LichSuCustomers)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.LichSuMerchants)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GiaTriKIPXu>()
                .Property(e => e.GiaTriXu)
                .HasPrecision(18, 0);

            modelBuilder.Entity<GioiTinh>()
                .HasMany(e => e.Merchants)
                .WithRequired(e => e.GioiTinh1)
                .HasForeignKey(e => e.GioiTinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HinhAnh>()
                .Property(e => e.DuongDan)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuCustomer>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<LichSuMerchant>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .HasMany(e => e.DanhGiaCuaMerchants)
                .WithRequired(e => e.Merchant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Merchant>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.Merchant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Merchant>()
                .HasMany(e => e.LichSuGiaoDichXuCuaMerchants)
                .WithRequired(e => e.Merchant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Merchant>()
                .HasMany(e => e.LichSuMerchants)
                .WithRequired(e => e.Merchant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaXuatBan>()
                .HasMany(e => e.Saches)
                .WithRequired(e => e.NhaXuatBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.GiaTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sach>()
                .Property(e => e.GiaKhuyenMai)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Sach>()
                .Property(e => e.MoTa)
                .IsUnicode(false);

            modelBuilder.Entity<Sach>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.Sach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TheLoai>()
                .HasMany(e => e.Saches)
                .WithRequired(e => e.TheLoai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TinhTrangDonHang>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.TinhTrangDonHang)
                .WillCascadeOnDelete(false);
        }
    }
}
