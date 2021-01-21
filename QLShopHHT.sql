use master
GO
CREATE DATABASE QUANLY_SHOP_WINFORM_1801
GO 
USE QUANLY_SHOP_WINFORM_1801
go

Create table [Customer]
(
	[CustomerID]  int IDENTITY(1,1) NOT NULL,
	[FullName] Nvarchar(100) NULL,
	[SDT] Varchar(15) NOT NULL Unique,
	[BuyTotal] Decimal(18,0) NULL,
Primary Key ([CustomerID])
) 
go

Create table [Order]
(
	[OrderID]  int IDENTITY(1,1) NOT NULL,
	[CreateDate] Datetime NULL,
	[CustomerID] int NOT NULL,
	[ToTalPrice] Decimal(18,0),
	[AdminID] int  NOT NULL,
Primary Key ([OrderID])
) 
go

Create table [ProductSize]
(
	[SizeCode] Varchar(10) NOT NULL,
	[Desciption] Nvarchar(250) NULL,
Primary Key ([SizeCode])
) 
Create table [ProductDetail]
(
	[STT] int IDENTITY(1,1) NOT NULL,
	[SizeCode] Varchar(10) NOT NULL,
	[ProductID] varchar(50) NOT NULL,
	[Quantity] Integer NULL,
	
Primary Key ([STT])
) 
Create table [Product]
(
	[ProductID] varchar(50) NOT NULL,
	[Name] Nvarchar(250) NULL,
	[Image] Image NULL,
	[DateCreate] datetime Null,
	[BuyPrice] Decimal(18,0) NULL,
	[SellPrice] Decimal(18,0) NULL,
Primary Key ([ProductID])
) 
go

CREATE VIEW productall
AS SELECT a.ProductID, a.Name, a.Image, a.BuyPrice, a.SellPrice, b.SizeCode, c.Desciption, b.Quantity, a.DateCreate
FROM Product a, ProductDetail b, ProductSize c
WHERE a.ProductID=b.ProductID
AND b.SizeCode=c.SizeCode;
go

SELECT * FROM productall;
Create table [OrderHaunt]
(
	[OrderHauntID] int IDENTITY(1,1) NOT NULL,
	[CreateDate] Datetime NULL,
	[ToTalPrice] Decimal(18,0),
	[AdminID] int NOT NULL,
Primary Key ([OrderHauntID])
) 
go

Create table [Administrator]
(
	[AdminID]  int IDENTITY(1,1) NOT NULL,
	[AccountName] varchar (20) not null unique,
	[Password] varchar(20) not null,
	[FullName] Nvarchar(250) NULL,
	[CMND] Varchar(15) NOt NULL unique,
	[Address] Nvarchar(250) NULL,
	[Position] nvarchar(50) null,
Primary Key ([AdminID])
) 
go

Create table [OrderDetail]
(
	[OrderID] int NOT NULL,
	[ProductID] varchar(50) NOT NULL,
	[OrderDetailID] Integer NOT NULL,
	[Name] Nvarchar(250) NULL,
	[Quantity] Integer NULL,
	[Discount] int null,
	[SizeCode] Varchar(10) NOT NULL,
	[Price] Decimal(18,0) NULL,
	[TotalPrice] Decimal(18,0) NULL,
Primary Key ([OrderID],[ProductID],[OrderDetailID])
) 
go

Create table [HauntDetail]
(
	[OrderHauntID] Integer NOT NULL,
	[ProductID] varchar(50) NOT NULL,
	[HauntDetailID] int NOT NULL,
	[Name] Nvarchar(250) NULL,
	[Quantity] Integer NULL,
	[Discount] int null,
	[SizeCode] Varchar(10) NOT NULL,
	[Price] Decimal(18,0) NULL,
	[TotalPrice] Decimal(18,0) NULL,
Primary Key ([OrderHauntID],[ProductID],[HauntDetailID])
) 
go



Alter table [ProductDetail] add  foreign key([SizeCode]) references [ProductSize] ([SizeCode])  on update no action on delete no action 
go
Alter table [ProductDetail] add  foreign key([ProductID]) references [Product] ([ProductID])  on update no action on delete no action 
go
Alter table [Order] add  foreign key([CustomerID]) references [Customer] ([CustomerID])  on update no action on delete no action 
go
Alter table [OrderDetail] add  foreign key([OrderID]) references [Order] ([OrderID])  on update no action on delete no action 
go
Alter table [OrderDetail] add  foreign key([ProductID]) references [Product] ([ProductID])  on update no action on delete no action 
go
Alter table [HauntDetail] add  foreign key([ProductID]) references [Product] ([ProductID])  on update no action on delete no action 
go
Alter table [HauntDetail] add  foreign key([OrderHauntID]) references [OrderHaunt] ([OrderHauntID])  on update no action on delete no action 
go
Alter table [Order] add  foreign key([AdminID]) references [Admin] ([AdminID])  on update no action on delete no action 
go
Alter table [OrderHaunt] add  foreign key([AdminID]) references [Admin] ([AdminID])  on update no action on delete no action 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


