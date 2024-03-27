
use master
drop database TrungTamTA
create database TrungTamTA
use TrungTamTA

CREATE TABLE [Admin] (
  [IDTaiKhoan] INT PRIMARY KEY IDENTITY ,
  [TaiKhoan] VARCHAR(50) ,
  [MatKhau] VARCHAR(50) NOT NULL,
);

CREATE TABLE [TaiKhoan] (
  [IDTaiKhoan] INT PRIMARY KEY IDENTITY ,
  [TaiKhoan] VARCHAR(50) ,
  [MatKhau] VARCHAR(50) NOT NULL,
);

CREATE TABLE [ChuongTrinhHoc] (
  IDChuongTrinh INT PRIMARY KEY IDENTITY ,
  TenChuongTrinh NVARCHAR(50),
  SoBuoiHoc NVARCHAR(50),
  ThoiLuong NVARCHAR(50),
  GiaTien FLOAT,
  MoTa NVARCHAR(MAX)
);

CREATE TABLE [TrangThaiHV] (
	IDTrangThai INT PRIMARY KEY,
	TenTrangThai  NVARCHAR(50),
);

CREATE TABLE [GiangVien] (
  IDGiangvien INT PRIMARY KEY IDENTITY ,
  TenGiangVien NVARCHAR(50),
  DiaChi  NVARCHAR(MAX),
  SoDienThoai VARCHAR(20),
  Email VARCHAR(50) UNIQUE,
  Hinh VARCHAR(MAX),
  BangCap  NVARCHAR(MAX),
  Luong  FLOAT,
);

CREATE TABLE [LopHoc] (
  IDLophoc INT PRIMARY KEY IDENTITY ,
  TenLopHoc NVARCHAR(50),
  IDGiangVien INT,
  IDChuongTrinh INT,
  SoLuong INT,
  FOREIGN KEY (IDGiangVien) REFERENCES GiangVien(IDGiangvien),
  FOREIGN KEY (IDChuongTrinh) REFERENCES ChuongTrinhHoc(IDChuongTrinh),
);

CREATE TABLE [HocVien] (
  IDHocvien INT PRIMARY KEY IDENTITY ,
  TenHocVien NVARCHAR(50),
  NgaySinh DATETIME,
  DiaChi NVARCHAR(100),
  SoDienThoai NVARCHAR(50),
  Email NVARCHAR(50) UNIQUE,
  Hinh NVARCHAR(MAX),
  IDTrangThai INT,
  IDTaiKhoan INT,
  NgayDangKy DATETIME,
   FOREIGN KEY (IDTrangThai) REFERENCES [TrangThaiHV](IDTrangThai),
   FOREIGN KEY ([IDTaiKhoan]) REFERENCES [TaiKhoan]([IDTaiKhoan])
);

CREATE TABLE ChiTietLopHoc (
    IDLophoc INT ,
    IDHocVien INT,
	DiemNghe FLOAT,
	DiemNoi FLOAT,
	DiemViet FLOAT,
	DiemDoc FLOAT,
	DiemTB AS CEILING((DiemNghe + DiemNoi + DiemViet + DiemDoc) / 4.0 * 2) / 2.0,
	[DaThanhToan] BIT,
	[NgayNopTien] DATETIME,
	PRIMARY KEY (IDLophoc, IDHocVien),
    FOREIGN KEY (IDHocVien) REFERENCES HocVien(IDHocVien),
    FOREIGN KEY (IDLophoc) REFERENCES LopHoc(IDLophoc)
);
CREATE TABLE [CacNgayTrongTuan] (
	IDNgay INT PRIMARY KEY,
	TenNgay NVARCHAR(50),
);
 
CREATE TABLE [LichHoc] (
	IDLichhoc INT PRIMARY KEY IDENTITY ,
	TGBatDau TIME,
    TGKetThuc TIME,
	IDNgay INT,
	Ngay DATETIME,
	FOREIGN KEY (IDNgay) REFERENCES [CacNgayTrongTuan](IDNgay),
);



CREATE TABLE [ChiTietLichHoc] (
	IDLophoc INT,
	IDLichhoc INT,
	 PRIMARY KEY (IDLophoc, IDLichhoc),
	FOREIGN KEY (IDLichhoc) REFERENCES [LichHoc](IDLichhoc),
    FOREIGN KEY (IDLophoc) REFERENCES LopHoc(IDLophoc),
);


-----------------------CHỌN BẢNG-------------------------
select * from [TrangThaiHV]
select * from [CacNgayTrongTuan]

	select * from [TaiKhoan]
	select * from [HocVien]
	select * from [GiangVien]
	select * from [ChuongTrinhHoc]
	select * from [LichHoc]
	select * from [LopHoc]
	select * from [ChiTietLopHoc]
	select * from [ChiTietLichHoc]

