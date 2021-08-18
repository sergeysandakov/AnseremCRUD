CREATE DATABASE SaleDB
GO

USE SaleDB;

CREATE TABLE Cities (
	CityId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CityName NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Contacts  (
	ContactId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  
	ContactPerson NVARCHAR(50) NOT NULL,
	ResponsiblePerson NVARCHAR(50) NULL
);
GO

CREATE TABLE Counterparties (
	CounterpartyId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CounterpartyName NVARCHAR(50) NOT NULL,
	CityId int FOREIGN KEY (CityId) REFERENCES Cities(CityId)
);
GO

CREATE TABLE Sales  (
	SaleId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	SaleName NVARCHAR(50) NOT NULL, 
	CounterpartyId int FOREIGN KEY (CounterpartyId) REFERENCES Counterparties(CounterpartyId),
	ContactId int FOREIGN KEY (ContactId) REFERENCES Contacts(ContactId)
);
GO

INSERT INTO Cities (CityName)  
VALUES 
('FirstCity'),
('SecondCity')
GO

INSERT INTO Contacts (ContactPerson, ResponsiblePerson)  
VALUES 
('FirstContactPerson', 'FirstResponsiblePerson'),
('SecondContactPerson', 'SecondResponsiblePerson')
GO

INSERT INTO Counterparties (CounterpartyName, CityId)  
VALUES 
('FirstCounterparty', 1),
('SecondCounterparty', 2)
GO

INSERT INTO Sales (SaleName, CounterpartyId, ContactId)  
VALUES 
('FirstSale 1', 1, 1),
('SecondSale 2', 2, 2),
GO
