USE master
IF EXISTS(SELECT * FROM sys.databases WHERE name='ProjectBookStore')
DROP DATABASE ProjectBookStore
CREATE DATABASE ProjectBookStore
GO

USE ProjectBookStore
GO

--Thể loại--
CREATE TABLE TheLoai(
	TheLoaiID int identity (1,1) not null primary key,
	TenTheLoai nvarchar(100)
)
GO

CREATE TABLE LoaiBia(
	LoaiBiaID int identity (1,1) not null primary key,
	LoaiBia nvarchar(100) not null
)

-- 6 Nha XB
CREATE TABLE NhaXuatBan(
	NhaXuatBanID int identity (1,1) not null primary key,
	TenNXB nvarchar(100) not null,
	DiaChi nvarchar(50) not null,
)
GO
-- 1 Sách
CREATE TABLE Sach(
	SachID int identity (1,1) not null primary key,
	MaSach varchar(100) not null,
	TenSach nvarchar(50) not null,
	TacGia nvarchar(50) not null,
	NhaXuatBan int foreign key references NhaXuatBan(NhaXuatBanID) not null,
	NgayXuatBan datetime not null,
	LoaiBiaID int foreign key references LoaiBia(LoaiBiaID),
	TheLoaiID int foreign key references TheLoai(TheLoaiID) not null
)
GO
-- GioiTinh --
CREATE TABLE GioiTinh(
	GioiTinhID int identity (1,1) not null primary key,
	TenGioiTinh nvarchar(50)
)
-- 12 Merchant
CREATE TABLE Merchant(
	MerchantID int identity (1,1) not null primary key,
	Email nvarchar(100) not null,
	HoTen nvarchar(100) not null,
	DiaChi nvarchar(100) not null,
	GioiTinh int foreign key references GioiTinh(GioiTinhID) not null,
	DiemTichLuy int not null,
	NgayTao DateTime not null,
)
GO

-- 2 
CREATE TABLE KhoSachMerchant(
	KhoSachMerchantID int identity (1,1) not null primary key,
	SachID int foreign key references Sach(SachID),
	MerchantID int foreign key references Merchant(MerchantID),
	DonGia money not null,
	SoLuong int not null,
	NgayTao DateTime not null,
	TrangThai bit Default 1, -- 1 là hoạt động, deactive nó thì sẽ thành 0
)
GO

-- 9 Customer
CREATE TABLE Customer(
	CustomerID int identity (1,1) not null primary key,
	Email nvarchar(100) not null,
	HoTen nvarchar(100) not null,
	DiaChi nvarchar(100) not null,
	GioiTinhID int foreign key references GioiTinh(GioiTinhID) not null,
	DanhGia int,
	TrangThai bit Default 1, -- 1 là hoạt động, deactive nó thì sẽ thành 0
)
GO

CREATE TABLE TinhTrangDonHang(
	TinhTrangDonHangID int identity (1,1) not null primary key,
	TenTinhTrangDonHang nvarchar(100)
)

-- 3 Đơn Hàng 
CREATE TABLE DonHang(
	DonHangID int identity (1,1) not null primary key,
	CustomerID int foreign key references Customer(CustomerID),
	MerchantID int foreign key references Merchant(MerchantID),
	NgayXuat Datetime,
	TinhTrangDonHangID int foreign key references TinhTrangDonHang(TinhTrangDonHangID) not null,
	DiaChi nvarchar(100)
)
GO

-- 4 Chi Tiet Don hang
CREATE TABLE ChiTietDonHang(
	ChiTietDonHangID int identity (1,1) not null primary key,
	DonHangID int foreign key references DonHang(DonHangID) not null,
	SachID int  foreign key references Sach(SachID) not null,
	SoLuong int not null,
	ThanhTien money not null,
	GhiChu nvarchar(100)
)
GO

-- 11 Danh Gia Cus
CREATE TABLE DanhGiaCus(
	DanhGiaCusID int identity (1,1) not null primary key,
	CustomerID int foreign key references Customer(CustomerID) not null,
	DonHangID int foreign key references DonHang(DonHangID) not null,
	SoSao float,
)
GO

-- 13 Danh Gia Mer
CREATE TABLE DanhGiaMer(
	DanhGiaMerID int identity (1,1) not null primary key,
	MerchantID int foreign key references Merchant(MerchantID),
	MaCus int,
	SoSao int,
)
GO


--10 Lich Su Customer
CREATE TABLE LSCus(
	MaLSCus int identity (1,1) not null primary key,
	MaCus int not null,
	MaDH int not null,
	TongTien money,
)
GO

-- 13 Lich Su Merchant
CREATE TABLE LSMer(
	MaLSMer int identity (1,1) not null primary key,
	MaMer int not null,
	MaDH int not null,
	TongTien money,
)
GO

-- 13 Danh Gia Mer
CREATE TABLE DGMer(
	MaDGM int identity (1,1) not null primary key,
	MaMer int,
	MaCus int,
	SoSao int,
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