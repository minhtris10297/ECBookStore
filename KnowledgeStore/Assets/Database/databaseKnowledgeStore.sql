USE master
IF EXISTS(SELECT * FROM sys.databases WHERE name='KnowledgeStore')
DROP DATABASE KnowledgeStore
CREATE DATABASE KnowledgeStore
GO

USE KnowledgeStore
GO

--Thể loại--
CREATE TABLE TheLoai(
	TheLoaiID int identity (1,1) not null primary key,
	TenTheLoai nvarchar(100) not null
)
GO

--Loại Bìa--
CREATE TABLE LoaiBia(
	LoaiBiaID int identity (1,1) not null primary key,
	TenLoaiBia nvarchar(100) not null
)

--  Nhà Xuất Bản --
CREATE TABLE NhaXuatBan(
	NhaXuatBanID int identity (1,1) not null primary key,
	TenNXB nvarchar(100) not null,
	DiaChi nvarchar(50) not null,
)
GO
--  Sách --
CREATE TABLE Sach(
	SachID int identity (1,1) not null primary key,
	MaSach varchar(100) not null,
	TenSach nvarchar(50) not null,
	TacGia nvarchar(50) not null,
	NhaXuatBanID int foreign key references NhaXuatBan(NhaXuatBanID) not null,
	NgayXuatBan datetime not null,
	SoTrang int not null,
	LoaiBiaID int foreign key references LoaiBia(LoaiBiaID),
	TheLoaiID int foreign key references TheLoai(TheLoaiID) not null
)
GO
-- GioiTinh --
CREATE TABLE GioiTinh(
	GioiTinhID int identity (1,1) not null primary key,
	TenGioiTinh nvarchar(50) not null
)
--  Merchant --
CREATE TABLE Merchant(
	MerchantID int identity (1,1) not null primary key,
	Email nvarchar(100) not null,
	HoTen nvarchar(100) not null,
	DiaChi nvarchar(100) not null,
	GioiTinhID int foreign key references GioiTinh(GioiTinhID) not null,
	DiemTichLuy int not null,
	NgayTao DateTime not null,
)
GO

-- chi tiet sach merchant--
CREATE TABLE ChiTietSachMerchant(
	KhoSachMerchantID int identity (1,1) not null primary key,
	SachID int foreign key references Sach(SachID),
	MerchantID int foreign key references Merchant(MerchantID),
	DonGia money not null,
	GiaKhuyenMai money null,
	SoLuong int not null,
	NgayTao DateTime not null,
	TrangThai bit Default 1 not null, -- 1 là hoạt động, deactive nó thì sẽ thành 0
)
GO

--  Customer --
CREATE TABLE Customer(
	CustomerID int identity (1,1) not null primary key,
	Email nvarchar(100),
	HoTen nvarchar(100) not null,
	DiaChi nvarchar(100),
	SoDienThoai varchar(20),
	MatKhauMaHoa varchar(256),
	IDFacebook varchar(256),
	IDGoogle varchar(256),
	NgayDangKy datetime not null,
	GioiTinhID int foreign key references GioiTinh(GioiTinhID),
	DanhGia int,
	TrangThai bit Default 1 not null, -- 1 là hoạt động, deactive nó thì sẽ thành 0
)
GO

--Tinh Trang Don Hang--
CREATE TABLE TinhTrangDonHang(
	TinhTrangDonHangID int identity (1,1) not null primary key,
	TenTinhTrangDonHang nvarchar(100)
)

-- Don Hang-- 
CREATE TABLE DonHang(
	DonHangID int identity (1,1) not null primary key,
	CustomerID int foreign key references Customer(CustomerID),
	MerchantID int foreign key references Merchant(MerchantID),
	NgayXuat Datetime,
	TongTien money not null,
	DiaChi nvarchar(100),
	TinhTrangDonHangID int foreign key references TinhTrangDonHang(TinhTrangDonHangID) not null
)
GO

--  Chi Tiet Don hang --
CREATE TABLE ChiTietDonHang(
	ChiTietDonHangID int identity (1,1) not null primary key,
	DonHangID int foreign key references DonHang(DonHangID) not null,
	SachID int  foreign key references Sach(SachID) not null,
	SoLuong int not null,
	ThanhTien money not null,
	GhiChu nvarchar(100)
)
GO

--  Danh Gia Cus --
CREATE TABLE DanhGiaCuaCustomer(
	DanhGiaCusID int identity (1,1) not null primary key,
	CustomerID int foreign key references Customer(CustomerID) not null,
	DonHangID int foreign key references DonHang(DonHangID) not null,
	SoSao float not null,
)
GO

-- 13 Danh Gia Mer
CREATE TABLE DanhGiaCuaMerchant(
	DanhGiaMerID int identity (1,1) not null primary key,
	MerchantID int foreign key references Merchant(MerchantID) not null,
	CustomerID int foreign key references Customer(CustomerID) not null,
	SoSao float not null,
)
GO


--10 Lich Su Customer
CREATE TABLE LichSuCus(
	LichSuCusID int identity (1,1) not null primary key,
	CustomerID int foreign key references Customer(CustomerID) not null,
	DonHangID int foreign key references DonHang(DonHangID) not null,
	TongTien money,
)
GO

-- 13 Lich Su Merchant
CREATE TABLE LichSuMer(
	LichSuMerID int identity (1,1) not null primary key,
	MerchantID int foreign key references Merchant(MerchantID) not null,
	DonHangID int foreign key references DonHang(DonHangID) not null,
	TongTien money,
)
GO

-- 14 Bai Dang
CREATE TABLE BaiDang(
	MaBaiDang int identity (1,1) not null primary key,
	MaMer int,
	ViTri int,
)
GO

-- 15 ChiTiet BaiDang
CREATE TABLE CTBaiDang(
	MaCTBD int identity (1,1) not null primary key,
	MaBaiDang int,
	TieuDe nvarchar(50),
	HinhAnh varchar(100), -- cái này lưu đường dẫn thôi, ko phải lưu hình ảnh
	NoiDung nvarchar(500)
)
GO

-- 16 Nâng Tin
CREATE TABLE NangTin(
	MaLuotNang int identity (1,1) not null primary key,
	MaBaiDang int not null,
	MaMer int not null,
)

-- 17 Admin
CREATE TABLE Admin(
	MaTaiKhoan int identity (1,1) not null primary key,
	TenDangNhap varchar(50) not null,
	MatKhau varchar(50) not null,
	TenHienThi nvarchar(50) not null,
)

