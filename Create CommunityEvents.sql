Create Table CommunityEvents
(
Id int IDENTITY(1,1) PRIMARY KEY,
CommunityId int not null,
EventTypeId int not null,
UserId int not null,
Name varchar(300) not null,
Speaker varchar(200) not null,
Panelist varchar(200) not null,
EventDate Datetime,
Subject varchar(300) not null,
Content varchar(1000) not null,
Wants varchar(1000) not null,
Status int not null DEFAULT 0,
CreatedDate Datetime DEFAULT GETDATE() not null,
UpdatedDate Datetime null
)

/*
Status deðeri 0 olan ilk oluþturma
Danýþman hoca onaylarsa 1, onaylamazsa 2 olacak. Danýþman hoca sadece 0 olanlarý görebilecek. 
Admin onaylarsa 3, onaylamazsa 4 olacak. Admin 1 olanlarý görebilecek.


Topluluk Lideri etkinlik düzenleme vs yapabilecek. Kendi açtýðý etkinlikleri görebilecek

Topluluk Danýþmaný,Danýþmaný olduðu topluluklarýn etkinliklerini görebilecek. Sadece onaylama veya red etme iþlemi yapabilecek

Admin Topluluk Danýþmanýnýn onayladýðý etkinliklerini görebilecek. Sadece onaylama veya red etme iþlemi yapabilecek

*/