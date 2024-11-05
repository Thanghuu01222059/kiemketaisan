create database KiemKe
go
use KiemKe
go
create table TaiSan(
Id int identity primary key,
TenTS nvarchar(100) null,
NoiSuDung nvarchar(max) null,
NamDVSD int null,
SoKiemKeTT int null,
SoTheoKeToan int null,
NguyenNhan nvarchar(max) null,
Gia float null,
TinhTrang int null,
GhiChu nvarchar(max),
)