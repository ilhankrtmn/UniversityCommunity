Create Table EventTypes
(
Id int IDENTITY(1,1) PRIMARY KEY,
Name varchar(30) not null
)

Insert Into EventTypes 
Values 
('Konferans'),
('Seminer'),
('Panel'),
('Gezi'),
('Diðer')