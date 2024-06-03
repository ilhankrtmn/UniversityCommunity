Create Table CommunityMembers 
(
Id int IDENTITY(1,1) PRIMARY KEY,
CommunityId int not null,
Name varchar(50) not null,
Surname varchar(50) not null,
Email varchar(80) not null,
StudentNumber varchar(50) not null,
Department varchar(50) not null,
Message varchar(1000) not null,
CreatedDate Datetime  not null default getdate(),
)