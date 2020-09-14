USE [cleanIt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE PrivateCustomer(
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[firstName] NVARCHAR (100) NOT NULL,
	[lastName] NVARCHAR (100) NOT NULL,
	[address] NVARCHAR (100) NOT NULL,
	[zipCode] INT NOT NULL,
	[phoneNumber] INT NOT NULL,
);

CREATE TABLE CorporateCustomer(
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[companyName] NVARCHAR (100) NOT NULL,
	[seNumber] INT NOT NULL,
	[phoneNumber] INT NOT NULL,
);

CREATE TABLE booking(
	[id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[workingHours] INT NOT NULL,
	[hourlyPay] INT NOT NULL,
	[bookingDescription] NVARCHAR (250) NOT NULL,
	[foreignKey] int NOT NULL,

);


SET IDENTITY_INSERT [PrivateCustomer] ON
INSERT INTO [PrivateCustomer] ([id], [firstName], [lastName], [address], [zipCode], [phoneNumber]) VALUES(1, 'Jens', 'Larsen', 'Skrænten 23', 9400, 25218414)
INSERT INTO [PrivateCustomer] ([id], [firstName], [lastName], [address], [zipCode], [phoneNumber]) VALUES(3, 'Bent', 'Larsen', 'Skrænten 23', 9400, 25218414)
INSERT INTO [PrivateCustomer] ([id], [firstName], [lastName], [address], [zipCode], [phoneNumber]) VALUES(5, 'Lars', 'Larsen', 'Skrænten 23', 9400, 25218414)
SET IDENTITY_INSERT [PrivateCustomer] OFF


SET IDENTITY_INSERT [CorporateCustomer] ON
INSERT INTO [CorporateCustomer] ([id], [companyName], [seNumber], [phoneNumber]) VALUES(2, 'Jens INC', 34343434, 69696969)
INSERT INTO [CorporateCustomer] ([id], [companyName], [seNumber], [phoneNumber]) VALUES(4, 'Lars INC', 34343434, 69696969)
SET IDENTITY_INSERT [CorporateCustomer] OFF

SELECT * FROM CorporateCustomer