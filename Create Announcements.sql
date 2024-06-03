/*
Create Table Announcements
(
Id int IDENTITY(1,1) PRIMARY KEY,
Image varchar(150) not null,
Title varchar(250) not null,
Description varchar(2000) not null,
Status bit not null,
CreatedDate Datetime Default GETDATE() not null,
UpdatedDate Datetime null
)
*/