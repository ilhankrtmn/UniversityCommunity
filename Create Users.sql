/*
Create Table Users 
(
Id int IDENTITY(1,1) PRIMARY KEY,
UserTypeId int not null,
Name varchar(50) not null,
Surname varchar(50) not null,
Email varchar(80) not null,
Password varchar(100) not null,
Status bit not null default 0,
CreatedDate Datetime  not null default getdate(),
UpdatedDate Datetime null
)
*/