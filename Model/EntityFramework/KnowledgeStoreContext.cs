namespace Model.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KnowledgeStoreContext : DbContext
    {
        public KnowledgeStoreContext()
            : base("name=KnowledgeStoreContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BaiDang> BaiDangs { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietSachMerchant> ChiTietSachMerchants { get; set; }
        public virtual DbSet<CTBaiDang> CTBaiDangs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DanhGiaCuaCustomer> DanhGiaCuaCustomers { get; set; }
        public virtual DbSet<DanhGiaCuaMerchant> DanhGiaCuaMerchants { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GioiTinh> GioiTinhs { get; set; }
        public virtual DbSet<LichSuCu> LichSuCus { get; set; }
        public virtual DbSet<LichSuMer> LichSuMers { get; set; }
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
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTietSachMerchant>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTietSachMerchant>()
                .Property(e => e.GiaKhuyenMai)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CTBaiDang>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.MatKhauMaHoa)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.SoDienThoai)
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
                .HasMany(e => e.LichSuCus)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.DanhGiaCuaCustomers)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.LichSuCus)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.LichSuMers)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GioiTinh>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.GioiTinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GioiTinh>()
                .HasMany(e => e.Merchants)
                .WithRequired(e => e.GioiTinh1)
                .HasForeignKey(e => e.GioiTinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LichSuCu>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LichSuMer>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Merchant>()
                .HasMany(e => e.DanhGiaCuaMerchants)
                .WithRequired(e => e.Merchant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Merchant>()
                .HasMany(e => e.LichSuMers)
                .WithRequired(e => e.Merchant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaXuatBan>()
                .HasMany(e => e.Saches)
                .WithRequired(e => e.NhaXuatBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sach>()
                .Property(e => e.MaSach)
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
