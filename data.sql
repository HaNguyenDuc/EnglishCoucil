----------------------------------THÊM DỮ LIỆU--------------------------
------------THÊM ACCOUNT ADMIN----------------
INSERT INTO [Admin]
VALUES (N'admin',N'admin');
INSERT INTO [TaiKhoan]
VALUES (N'kientran',N'e10adc3949ba59abbe56e057f20f883e');

------------THÊM TRẠNG THÁI HỌC VIÊN----------------
INSERT INTO [TrangThaiHV]
VALUES (1, N'Đăng ký học');
INSERT INTO [TrangThaiHV]
VALUES (2, N'Đang học');
INSERT INTO [TrangThaiHV]
VALUES (3, N'Bỏ học');
INSERT INTO [TrangThaiHV]
VALUES (4, N'Nợ học phí');
INSERT INTO [TrangThaiHV]
VALUES (5, N'Bảo lưu');
--UPDATE [TrangThaiHV] SET TenTrangThai = N'Đang học' WHERE IDTrangThai=1 ;
----------------------------------------------------
------------THÊM THỨ---------------
INSERT INTO [CacNgayTrongTuan]
VALUES (1, N'Thứ 2');
INSERT INTO [CacNgayTrongTuan]
VALUES (2, N'Thứ 3');
INSERT INTO [CacNgayTrongTuan]
VALUES (3, N'Thứ 4');
INSERT INTO [CacNgayTrongTuan]
VALUES (4, N'Thứ 5');
INSERT INTO [CacNgayTrongTuan]
VALUES (5, N'Thứ 6');
INSERT INTO [CacNgayTrongTuan]
VALUES (6, N'Thứ 7');
INSERT INTO [CacNgayTrongTuan]
VALUES (7, N'Chủ Nhật');
--------------------------------------------------------
------------THÊM GIẢNG VIÊN----------------
INSERT INTO [GiangVien]
VALUES ( N'Kiên',N'Châu thành','0976391970',N'kt78139@gmail.com',null,null,100000);
---------------------------------------------
------------THÊM HỌC VIÊN----------------
--UPDATE [HocVien] SET IDTaiKhoan = null WHERE IDTaiKhoan= 1  ;
--drop table [HocVien];
INSERT INTO [HocVien]
VALUES ( N'Kiên Trần',28/05/2004,N'Châu thành Tây Ninh','0976391970',N'kt78139@gmail.com',null,1,1,28/05/2023);
INSERT INTO [HocVien]
VALUES ( N'Kiên Trần1',16/10/2002,N'Châu thành Tây Ninh1','0976391970',N'kt781390@gmail.com',null,1,null,null);
------------THÊM CHƯƠNG TRÌNH HỌC----------------
INSERT INTO [ChuongTrinhHoc]
VALUES ( N'Ielts 2.5',N'2',N'3',10000,N'Dành cho các bạn IELTS 2.5');
INSERT INTO [ChuongTrinhHoc]
VALUES ( N'Ielts 3.0',N'2',N'3',10000,N'Dành cho các bạn IELTS 3.0');
-------------------------------------------------
------------THÊM LỊCH HỌC----------------
INSERT INTO [LichHoc]
VALUES ( N'07:00',N'09:00',1,30/05/2023);
--------------------------------------
------------THÊM LỚP HỌC----------------
INSERT INTO [LopHoc]
VALUES ( N'Ielts 1',1,1,30);
INSERT INTO [LopHoc]
VALUES ( N'Ielts 2',1,2,30);
------------CHI TIẾT LỚP HỌC----------------
INSERT INTO [ChiTietLopHoc]
VALUES (1,1,6.5,6.5,5.0,7.5,0,null);
--UPDATE [ChiTietLopHoc] SET DaThanhToan = 0 WHERE IDLophoc = 2  ;
INSERT INTO [ChiTietLopHoc]
VALUES (2,1,null,null,null,null,0,null);
----------------------------------
------------THÊM CHI TIẾT LỊCH HỌC----------------
INSERT INTO [ChiTietLichHoc]
VALUES (1,1);
INSERT INTO [ChiTietLichHoc]
VALUES (2,1);
----------------------------------
-----------------------CHỌN BẢNG-------------------------
select * from [TrangThaiHV]
select * from [CacNgayTrongTuan]
SET DATEFORMAT dmy;
UPDATE [HocVien] SET NgayDangKy = '13/06/2023' WHERE IDHocvien= 7  ;
UPDATE [HocVien] SET NgayDangKy = '13/06/2023' WHERE IDHocvien= 2  ;
UPDATE [HocVien] SET NgayDangKy = '13/06/2023' WHERE IDHocvien= 3  ;
UPDATE [HocVien] SET NgayDangKy = '13/06/2023' WHERE IDHocvien= 4  ;
UPDATE [HocVien] SET NgayDangKy = '06/11/2023' WHERE IDHocvien= 5  ;
UPDATE [HocVien] SET NgayDangKy = '06/11/2023' WHERE IDHocvien= 6  ;


	INSERT INTO [TaiKhoan]
VALUES (N'viettrung',N'e10adc3949ba59abbe56e057f20f883e');
	INSERT INTO [TaiKhoan]
VALUES (N'khoithieu',N'e10adc3949ba59abbe56e057f20f883e');

	INSERT INTO [TaiKhoan]
VALUES (N'anhtuan',N'e10adc3949ba59abbe56e057f20f883e');

	INSERT INTO [TaiKhoan]
VALUES (N'luutuan',N'e10adc3949ba59abbe56e057f20f883e');

	INSERT INTO [TaiKhoan]
VALUES (N'manhcuong',N'e10adc3949ba59abbe56e057f20f883e');
	INSERT INTO [TaiKhoan]
VALUES (N'lamhung',N'e10adc3949ba59abbe56e057f20f883e');

select * from [TaiKhoan]
	select * from [HocVien]
	select * from [GiangVien]
	select * from [ChuongTrinhHoc]
	select * from [LichHoc]
	select * from [LopHoc]
	select * from [ChiTietLopHoc]
	select * from [ChiTietLichHoc]