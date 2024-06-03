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
Status de�eri 0 olan ilk olu�turma
Dan��man hoca onaylarsa 1, onaylamazsa 2 olacak. Dan��man hoca sadece 0 olanlar� g�rebilecek. 
Admin onaylarsa 3, onaylamazsa 4 olacak. Admin 1 olanlar� g�rebilecek.


Topluluk Lideri etkinlik d�zenleme vs yapabilecek. Kendi a�t��� etkinlikleri g�rebilecek

Topluluk Dan��man�,Dan��man� oldu�u topluluklar�n etkinliklerini g�rebilecek. Sadece onaylama veya red etme i�lemi yapabilecek

Admin Topluluk Dan��man�n�n onaylad��� etkinliklerini g�rebilecek. Sadece onaylama veya red etme i�lemi yapabilecek

*/