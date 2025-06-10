--drop table Rates
--create table Rates
--(
--Id int identity primary key(Id),
--CurrencyName nvarchar(255),
--CurrencyValue money
--)

create database Registration
use Registration;
create Table Users
(
Id int identity primary key(Id),
FirstName nvarchar(50),
LastName nvarchar(50),
Age tinyint,
UserName nvarchar(50),
Email nvarchar(255),
[Password] nvarchar(13),
[PasswordCheck] nvarchar(13),
PhoneNumber nvarchar(20),
Promocode nvarchar(5),
RoleId int,
foreign key (RoleId) references Roles(Id)
)
create table Roles
(
Id int identity primary key(Id),
[Role] nvarchar(10) 
)
create table Promocodes
(
Id int identity Primary key(Id),
Promocode nvarchar(5)
)
select *from Users
insert into Promocodes(Promocode)values('12abc'),('135bd'),('1234')
drop table Users