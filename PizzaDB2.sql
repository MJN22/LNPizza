create Database pizzaDB
go
create schema pizzaDB
go


--drop table UserTBL
CREATE TABLE UserTBL
(
  UserID int identity Primary key NOT NULL,
  FirstName NVARCHAR(108) NOT NULL
,
  LastName NVARCHAR(108) NOT NULL
  ,
  --User
);
go
--drop table UserAddress 
CREATE TABLE UserAddress
(
  UserAddressID int identity Primary key NOT NULL,
  Address1 VARCHAR(120)NOT NULL,
  Address2 VARCHAR(120),
  City VARCHAR(100) NOT NULL,
  ProvidenceState NVARCHAR(108) NOT NULL,--SPELLING ERROR
  PostalCode VARCHAR(16) NOT NULL,
  CountryAbrev NVARCHAR(5) NOT NULL,


);
GO
CREATE TABLE Orders
(
  UserOrderAddressID int not null,
  StoreAddressID int not null,
  ID int NOT NULL,
  --make this foreign key to orderdetail

);
GO

CREATE TABLE Store
(
  StoreAddress varchar(1000),
  StoreAddressID int identity Primary key NOT NULL,

  Address1 VARCHAR
(120)NOT NULL,
  Address2 VARCHAR
(120),
  City VARCHAR
(100) NOT NULL,
  ProvidenceState NVARCHAR
(108) NOT NULL,
  PostalCode VARCHAR
(16) NOT NULL,
  CountryAbrev NVARCHAR
(5) NOT NULL,

);

GO
--drop table StoreIngredients
CREATE TABLE StoreIngredients
(
  StoreIngredientsAddressID int Not Null,
  ID INT PRIMARY KEY IDENTITY NOT NULL,
  --IngredientsName NVARCHAR(300) NOT NULL,
  IngredientStock INT,
  Quantity int,
  --uncommented this for compiling issues need to fix db
);
GO
--drop Table OrderDetails
CREATE TABLE OrderDetails
(
  id INT not null,
  OrderID int Primary Key Identity(1,1) not null,
  PizzaID int not null,

)
go

CREATE TABLE Pizza
(
  ID int PRIMARY KEY IDENTITY,
  PizzaName NVARCHAR(500) NOT NULL,
  Costs MONEY not null,
  Quantity int,
);
GO

CREATE TABLE PizzaIngredients
(
  ID INT Primary Key IDENTITY(1,1),--add primary key on to identity field see if this helps scaffold
  PizzaID INT NOT NULL,
  IngredientName NVARCHAR(108) NOT NULL,
  IngredientCost INT NOT NULL,

); 
GO

------------ADDDING CONSTRAINTSSSSS -------------

Alter table PizzaIngredients
add constraint Fk_PizzaID
foreign key (PizzaID)
References Pizza(ID)

alter table OrderDetails
Add constraint FK_OrderID
Foreign key (OrderID)
References Orders(ID)

-- ALTER TABLE PizzaIngredients
-- ADD CONSTRAINT FK_PizzaID
-- FOREIGN KEY (PizzaID) 
-- REFERENCES Pizza(ID);
-- GO

ALTER TABLE UserAddress
ADD CONSTRAINT FK_UserAddressID
FOREIGN KEY (UserAddressID) 
REFERENCES UserTBL(UserID);
GO
---- going to re do  relationship between orders
--orderdetails and possibly store
ALTER TABLE Orders
ADD CONSTRAINT FK_UserOrderAddressID
FOREIGN KEY (UserOrderAddressID) 
REFERENCES UserAddress(UserAddressID);
GO

Alter table Orders
ADD CONSTRAINT FK_StoreAddressID
FOREIGN KEY ( StoreAddressID )
REFERENCES Store(StoreAddressID);
GO

ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderID
FOREIGN KEY ( OrderID )
REFERENCES Orders(ID);
GO

ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderPizzaID
FOREIGN KEY ( PizzaID )
REFERENCES Pizza(ID);
GO

ALTER TABLE StoreIngredients
ADD CONSTRAINT FK_StoreIngredientsAddressID
FOREIGN KEY ( StoreIngredientsAddressID )
REFERENCES Store(StoreAddressID);
GO

------create info/insert --------------
INSERT INTO UserTBL
  (FirstName, LastName)
Values
  ('Tony', 'Stark'),
  ('Elon', 'Musk'),
  ('Greg', 'Savage');
--select * from UserTBL;

INSERT INTO UserAddress
  (Address1,Address2, City, ProvidenceState, PostalCode,CountryAbrev)
VALUES((SELECT UserID
    FROM UserTBL
    WHERE UserID =1), '181 11th street' , 'Charlotte', 'North Carolina', '28269', 'USA')
--select * from UserAddress;
INSERT INTO UserAddress
  (Address1,Address2, City, ProvidenceState, PostalCode,CountryAbrev)
VALUES((SELECT UserID
    FROM UserTBL
    WHERE UserID =2), '11th S 12th Street' , 'Pheonix', 'Arizona', '85001', 'USA')
INSERT INTO UserAddress
  (Address1,Address2, City, ProvidenceState, PostalCode,CountryAbrev)
VALUES((SELECT UserID
    FROM UserTBL
    WHERE UserID =3), '2121 Alexander Heigths' , 'Concord', 'North Carolina', '282027', 'USA')

INSERT INTO Store
  (StoreAddress, Address1, Address2, City, ProvidenceState, PostalCode, CountryAbrev)
VALUES( '11th Main street, Charlotte, North Carolina 28269 USA' , '11th Main street', 'Null', 'Charlotte', 'North Carolina', '28269', 'USA')
Select *
from Store;

--INSERT Pizza-----
--drop table pizza
INSERT INTO Pizza
  ( PizzaName, Costs)
VALUES
  ('Cheese', 8.00)
INSERT INTO Pizza
  ( PizzaName, Costs)
VALUES
  ('ExtraCheesy', 12.00)
INSERT INTO Pizza
  ( PizzaName, Costs)
VALUES
  ('Meats', 15.00)

SELECT *
FROM Pizza
---into pizzaIngredients
INSERT INTO PizzaIngredients
  (PizzaID, IngredientName, IngredientCost)
VALUES
  ( (select ID
    FROM Pizza
    Where ID =1), 'Cheese', 1)

INSERT INTO PizzaIngredients
  (PizzaID, IngredientName, IngredientCost)
VALUES
  ( (select ID
    FROM Pizza
    Where ID =2), (select PizzaName
    FROM Pizza
    Where ID =2) , 2)

INSERT INTO PizzaIngredients
  (PizzaID, IngredientName, IngredientCost)
VALUES
  ( (select ID
    FROM Pizza
    Where ID =3), (select PizzaName
    FROM Pizza
    Where ID =3) , 3)



--SELECT *FROM PizzaIngredients 
--SELECT * FROM UserAddress
--select * From Pizza
--SELECT * From Store
--select * from StoreIngredients
--select * from Orders
--select * from OrderDetails
------insert into store ignreds---
INSERT INTO StoreIngredients
  ( StoreIngredientsAddressID, IngredientStock)
Values((Select StoreAddressID
    from Store), 250)
GO
--insert into order and order  pizzaa--------
INSERT INTO Orders
  (UserOrderAddressID, StoreAddressID)
VALUES
  (1, 1),
  (2, 1),
  (3, 1)
--isseus storing into order pizza may have key wrong, also reference fields wrong 
--will get back to this if i can

--select * from Orders
--select * from OrderDetails

Alter table Orders
add constraint FK_ID
foreign key (ID)
References OrderDetails(OrderID)

Alter table OrderDetails
add constraint FK_PizzaIDOrderDetails
foreign key (ID)
References Pizza(ID)

alter table Orders
add _orderId int Primary Key identity(1,1);

ALTER TABLE OrderDetails
Add Quantity int CHECK (Quantity >= 12)

select *
from OrderDetails

alter table OrderDetails

Alter table OrderDetails
add constraint FK_Quantity
foreign key
(Quantity)
References Pizza
(ID)