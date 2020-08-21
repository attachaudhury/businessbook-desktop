use master
go
drop database bbdb
go
CREATE DATABASE bbdb
GO
use bbdb 
GO

CREATE TABLE [user](
	id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	[address] varchar(200),
	[name] varchar(20),
	[password] varchar(20),
	username varchar(20),
	phone varchar(30),
	[role] varchar(20) default 'user'
)

CREATE TABLE tbl_Sitting(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name varchar(20),
)

CREATE TABLE tbl_Supplier(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name varchar(20),
	PhoneNo varchar(20),
	[Address] varchar(50),
	Balance float,
)
-- 0 id is for admin ,  1 for sale person, 2  for delivery
CREATE TABLE tbl_StaffCategory(
	Id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	[Name] varchar(20))


CREATE TABLE tbl_Customer(
	Id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	Name varchar(30),
	[Address] varchar(150),
	PhoneNo varchar(11))


CREATE TABLE tbl_FoodItemCategory(
	Id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	Name varchar(50))


CREATE TABLE tbl_KitchenInventoryCategory(
	Id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	Name varchar(50))


CREATE TABLE tbl_DepositHead(
	Id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	[Name] varchar(30))

	
CREATE TABLE tbl_ExpenceHead(
	Id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	Name varchar(50))


create table tbl_PaymentMode(
Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
Name varchar(50))

--KitchenInventory Quantity will be base unit eg 1ml, 1g, 1 egg, 1g carrot
CREATE TABLE tbl_KitchenInventory(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name varchar(50),
	Quantity float,
	MinimumQuantity float,
	PurchasePrice float,
	ExpiryDate DateTime,
	KitchenInventoryCategory_Id int foreign key references tbl_KitchenInventoryCategory(Id)	
)


CREATE TABLE tbl_DetuctInventory(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FoodItem_Id int,
	DeductedQuantity float,
	KitchenInventory_id int foreign key references tbl_KitchenInventory(Id))


CREATE TABLE tbl_ExpenceSubHead(
	Id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	Name varchar(50),
	ExpenseHead_Id int foreign key references tbl_ExpenceHead(Id)
)


CREATE TABLE tbl_Expence(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Amount int,
	Comment varchar(50),
	DatenTime DateTime,
	ExpenseHead_Id int foreign key references tbl_ExpenceHead(Id),
	ExpenceSubHead_Id int foreign key references tbl_ExpenceSubHead(Id),
)


CREATE TABLE tbl_FinanceChart(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Date] DateTime,
	OpeningBalance float,
	Sale float,
	Delivery float,
	TotalSale float,
	TotalCash float,
	Deposit float,
	Expence float,
	ClosingBalance float)

--0 id is for admin category
CREATE TABLE tbl_Staff(
	Id int IDENTITY(0,1) NOT NULL PRIMARY KEY,
	[Name] varchar(20),
	Designation varchar(20),
	UserName varchar(20),
	[Password] varchar(20),
	CNIC varchar(20),
	PhoneNo varchar(30),
	[Address] varchar(100),
	JoiningDate DateTime,
	LeavingDate DateTime,
	Salary float,
	DutyStart int,
	DutyEnd int,
	Comment varchar(50),
	StaffCategory_Id int foreign key references tbl_StaffCategory(Id))



CREATE TABLE tbl_PurchaseOrder(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Supplier_id int foreign key references tbl_Supplier(Id),
	DatenTime DateTime)



CREATE TABLE tbl_PurchaseOrderItem(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name varchar(20),
	Quantity float)



--SaleType (1 for takeaway, 2 for table, 3 for delivery)
CREATE TABLE tbl_Sale(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Date_Time DateTime,
	Amount float,
	SaleType int,
	Discount float,
	Customer_Id int foreign key references tbl_Customer(Id),
	Staff_id int foreign key references tbl_Staff(Id))



CREATE TABLE tbl_FoodItem(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name varchar(50),
	SalePrice float,
	ManageInventory bit,
	Recipe varchar(1000),
	Category_Id int foreign key references tbl_FoodItemCategory(Id))


	CREATE TABLE product(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	barcode nvarchar(100),
    carrycost float,
    discount float,
    [name] varchar(100),
    purchaseprice float,
    purchaseactive bit,
    quantity float,
    saleprice float,
    saleactive bit,
	--[type] nvarchar(100), -- eg product raw deal
)

	CREATE TABLE subproduct(
	id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	fk_product_product_subproduct int,
	constraint fk_product_product_subproduct foreign key (fk_product_product_subproduct) references product(id),
	fk_subproduct_product_subproduct int,
	constraint fk_subproduct_product_subproduct foreign key (fk_subproduct_product_subproduct) references product(id),
	quantity float,
)






CREATE TABLE tbl_Deal(
	Id int IDENTITY(20001,1) NOT NULL PRIMARY KEY,
	Name varchar(50),
	SalePrice float,
	ManageInventory bit,
	Recipe varchar(1000),
	Category_Id int foreign key references tbl_FoodItemCategory(Id)
)



CREATE TABLE tbl_DealFoodItem(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Deal_Id int foreign key references tbl_Deal(Id),
	FoodItem_Id int foreign key references tbl_FoodItem(Id))



--both deal and food item will be stored here. dealId will be greater then 20000
CREATE TABLE tbl_SaleItem(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Quantity float,	
	Sale_id int foreign key references tbl_Sale(Id),
	Item_id int)


CREATE TABLE tbl_DeliveryQueue(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Delivered Bit,
	DatenTime DateTime,
	Sale_Id int foreign key references tbl_Sale(Id),
	Customer_Id int foreign key references tbl_Customer(Id),
	DeliveryBoyId int foreign key references tbl_Staff(Id))


CREATE TABLE tbl_Deposit(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Ammount float,
	Comment varchar(30),
	DatenTime DateTime, 
	DepositHead_Id int foreign key references tbl_DepositHead(Id))



create table financeaccount(
id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
name varchar(30),
type varchar(50),
fk_parent_financeaccount int,
constraint fk_parent_financeaccount foreign key (fk_parent_financeaccount) references financeaccount(id),
);


create table financetransaction(
id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
groupid int,
name varchar(50),
amount float,
status varchar(50),
date datetime,
childof int,
paymentmethod varchar(30),
referencenumber varchar(50),
bank varchar(50),
branch varchar(100),
chequedate datetime,
details varchar(200),
fk_createdbyuser_user_financetransaction int,
constraint fk_createdbyuser_user_financetransaction foreign key (fk_createdbyuser_user_financetransaction) references [user](id),
fk_targettouser_user_financetransaction int,
constraint fk_targettouser_user_financetransaction foreign key (fk_targettouser_user_financetransaction) references [user](id),
fk_financeaccount_financeaccount_financetransaction int,
constraint fk_financeaccount_financeaccount_financetransaction foreign key (fk_financeaccount_financeaccount_financetransaction) references financeaccount(id),
);

create table salepurchaseproduct(
id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
price float,
quantity float,
total float,
fk_product_salepurchaseproduct_product int,
constraint fk_product_salepurchaseproduct_product foreign key (fk_product_salepurchaseproduct_product) references product(id),
fk_financetransaction_salepurchaseproduct_financetransaction int,
constraint fk_financetransaction_salepurchaseproduct_financetransaction foreign key (fk_financetransaction_salepurchaseproduct_financetransaction) references financetransaction(id),
);

create table closing(
id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
[date] datetime,
expence float,
income float,
closingbalance float,
comment varchar(200),
fk_user_closing_user int,
constraint fk_user_closing_user foreign key (fk_user_closing_user) references [user](id),
);


SET IDENTITY_INSERT financeaccount ON
/* asset Accounts */
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(101,'bank','asset',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(1011,'meezan bank','asset',101);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(1012,'faisal bank','asset',101);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(102,'cash','asset',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(103,'petty cash','asset',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(104,'undeposited fund','asset',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(105,'account receivable','asset',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(106,'fixed','asset',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(107,'current','asset',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(108,'other','asset',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(109,'inventory','asset',null);
/*liabitity Accounts */
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(201,'notes payable','liabitity',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(202,'account payable','liabitity',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(203,'tax payable','liabitity',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(204,'salary payable','liabitity',null);
/*equity Accounts */
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(301,'owner equity','equity',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(302,'share capital','equity',null);
/*income Accounts */
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(401,'pos sale','income',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(402,'service sale','income',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(403,'other','income',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(405,'inventory gain','income',null);
/*expence Accounts */
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(501,'operating','expence',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(502,'salary','expence',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(503,'paid tax','expence',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(504,'cgs','expence',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(509,'discount','expence',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(510,'other','expence',null);
insert into financeaccount(id,name,type,fk_parent_financeaccount) values(511,'inventory loss','expence',null);

SET IDENTITY_INSERT financeaccount OFF



--Must Have Data
INSERT INTO [user](name,phone,username,password,role) VALUES ('admin','00000000000','admin','admin@123','admin');
INSERT INTO tbl_Customer(Name,PhoneNo)
VALUES ('anonymous','00000000000');
INSERT INTO tbl_KitchenInventoryCategory (Name)
VALUES ('Other');
INSERT INTO tbl_StaffCategory(Name)
VALUES ('Administration');
INSERT INTO tbl_StaffCategory(Name)
VALUES ('Sale');
INSERT INTO tbl_StaffCategory(Name)
VALUES ('Delivery');
INSERT INTO tbl_DepositHead(Name)
VALUES ('Default');
INSERT INTO tbl_ExpenceHead(Name)
VALUES ('Default');
INSERT INTO tbl_PaymentMode(Name)
VALUES ('Cash');
INSERT INTO tbl_PaymentMode(Name)
VALUES ('CreditCard');
INSERT INTO tbl_ExpenceSubHead(Name,ExpenseHead_Id)
VALUES ('Default',0);
INSERT INTO tbl_Staff(Name,StaffCategory_Id,UserName,[Password])
VALUES ('Admin',0,'admin','123');
INSERT INTO tbl_FoodItemCategory(Name)
VALUES ('Default');

go
use master
go