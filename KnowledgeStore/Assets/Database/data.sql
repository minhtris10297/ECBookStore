USE [KnowledgeStore]
GO
/****** Object:  Table [dbo].[GioiTinh]    Script Date: 11/15/2018 3:08:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioiTinh](
	[GioiTinhID] [int] IDENTITY(1,1) NOT NULL,
	[TenGioiTinh] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GioiTinhID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HinhAnh]    Script Date: 11/15/2018 3:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HinhAnh](
	[HinhAnhID] [int] IDENTITY(1,1) NOT NULL,
	[DuongDan] [varchar](500) NOT NULL,
 CONSTRAINT [PK_HinhAnh] PRIMARY KEY CLUSTERED 
(
	[HinhAnhID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoaiBia]    Script Date: 11/15/2018 3:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiBia](
	[LoaiBiaID] [int] IDENTITY(1,1) NOT NULL,
	[LoaiBia] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LoaiBiaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Merchant]    Script Date: 11/15/2018 3:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Merchant](
	[MerchantID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[HoTen] [nvarchar](100) NOT NULL,
	[DiaChi] [nvarchar](100) NOT NULL,
	[GioiTinh] [int] NOT NULL,
	[SoLuongKIPXu] [int] NOT NULL,
	[NgayTao] [datetime] NOT NULL,
 CONSTRAINT [PK__Merchant__04416563C82F31AF] PRIMARY KEY CLUSTERED 
(
	[MerchantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NangTin]    Script Date: 11/15/2018 3:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NangTin](
	[NangTinID] [int] IDENTITY(1,1) NOT NULL,
	[NgayNang] [datetime] NOT NULL,
 CONSTRAINT [PK__NangTin__36B5D6E180277628] PRIMARY KEY CLUSTERED 
(
	[NangTinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhaXuatBan]    Script Date: 11/15/2018 3:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaXuatBan](
	[NhaXuatBanID] [int] IDENTITY(1,1) NOT NULL,
	[TenNXB] [nvarchar](100) NOT NULL,
	[DiaChi] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK__NhaXuatB__E0587C6E81F12E05] PRIMARY KEY CLUSTERED 
(
	[NhaXuatBanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sach]    Script Date: 11/15/2018 3:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[SachID] [int] IDENTITY(1,1) NOT NULL,
	[TenSach] [nvarchar](50) NOT NULL,
	[TacGia] [nvarchar](50) NOT NULL,
	[NhaXuatBanID] [int] NOT NULL,
	[NgayXuatBan] [datetime] NOT NULL,
	[SoTrang] [int] NOT NULL,
	[LoaiBiaID] [int] NULL,
	[MerchantID] [int] NULL,
	[TrangThai] [bit] NULL,
	[GiaTien] [decimal](18, 0) NOT NULL,
	[GiaKhuyenMai] [decimal](18, 0) NULL,
	[HinhAnhID] [int] NULL,
	[MoTa] [ntext] NULL,
	[NangTinID] [nchar](10) NOT NULL,
	[SoLuong] [int] NULL,
	[TheLoaiID] [int] NOT NULL,
 CONSTRAINT [PK__Sach__F3005E3A42D94796] PRIMARY KEY CLUSTERED 
(
	[SachID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TheLoai]    Script Date: 11/15/2018 3:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheLoai](
	[TheLoaiID] [int] IDENTITY(1,1) NOT NULL,
	[TenTheLoai] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TheLoaiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Merchant] ADD  CONSTRAINT [DF_Merchant_KIPXu]  DEFAULT ((0)) FOR [SoLuongKIPXu]
GO
ALTER TABLE [dbo].[Sach] ADD  CONSTRAINT [DF_Sach_TrangThai]  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[Merchant]  WITH CHECK ADD  CONSTRAINT [FK__Merchant__GioiTi__440B1D61] FOREIGN KEY([GioiTinh])
REFERENCES [dbo].[GioiTinh] ([GioiTinhID])
GO
ALTER TABLE [dbo].[Merchant] CHECK CONSTRAINT [FK__Merchant__GioiTi__440B1D61]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK__Sach__LoaiBiaID__3E52440B] FOREIGN KEY([LoaiBiaID])
REFERENCES [dbo].[LoaiBia] ([LoaiBiaID])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK__Sach__LoaiBiaID__3E52440B]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK__Sach__NhaXuatBan__3D5E1FD2] FOREIGN KEY([NhaXuatBanID])
REFERENCES [dbo].[NhaXuatBan] ([NhaXuatBanID])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK__Sach__NhaXuatBan__3D5E1FD2]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK__Sach__TheLoaiID__3F466844] FOREIGN KEY([TheLoaiID])
REFERENCES [dbo].[TheLoai] ([TheLoaiID])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK__Sach__TheLoaiID__3F466844]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_HinhAnh] FOREIGN KEY([HinhAnhID])
REFERENCES [dbo].[HinhAnh] ([HinhAnhID])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_HinhAnh]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_Merchant] FOREIGN KEY([MerchantID])
REFERENCES [dbo].[Merchant] ([MerchantID])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_Merchant]
GO
