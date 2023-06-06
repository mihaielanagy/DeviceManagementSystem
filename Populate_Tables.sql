-- Select the database to use
USE DeviceManagement;
GO

-- Populate OperatingSystems table
IF NOT EXISTS (SELECT 1 FROM OperatingSystems WHERE [Name] = 'iOS')
BEGIN 
	INSERT INTO OperatingSystems ([Name]) VALUES('iOS')
END           
GO

IF NOT EXISTS (SELECT 1 FROM OperatingSystems WHERE [Name] = 'Android')
BEGIN 
	INSERT INTO OperatingSystems ([Name]) VALUES('Android')
END
GO

IF NOT EXISTS (SELECT 1 FROM OperatingSystems WHERE [Name] = 'iPadOS')
BEGIN 
	INSERT INTO OperatingSystems ([Name]) VALUES('iPadOS')
END
GO

-- Populate OperatingSystemVersions table
IF NOT EXISTS (SELECT 1 FROM OperatingSystemVersions WHERE [Name] = 'Android 14')
BEGIN 
	INSERT INTO OperatingSystemVersions (Id_OS, [Name])
	SELECT Id, 'Android 14'
	FROM OperatingSystems
	WHERE [Name] = 'Android'
END
GO

IF NOT EXISTS (SELECT 1 FROM OperatingSystemVersions WHERE [Name] = 'Android 13')
BEGIN 
	INSERT INTO OperatingSystemVersions (Id_OS, [Name])
	SELECT Id, 'Android 13'
	FROM OperatingSystems
	WHERE [Name] = 'Android'
END
GO

IF NOT EXISTS (SELECT 1 FROM OperatingSystemVersions WHERE [Name] = 'iOS 16')
BEGIN 
	INSERT INTO OperatingSystemVersions (Id_OS, [Name])
	SELECT Id, 'iOS 16'
	FROM OperatingSystems
	WHERE [Name] = 'iOS'
END
GO

IF NOT EXISTS (SELECT 1 FROM OperatingSystemVersions WHERE [Name] = 'iOS 15')
BEGIN 
	INSERT INTO OperatingSystemVersions (Id_OS, [Name])
	SELECT Id, 'iOS 15'
	FROM OperatingSystems
	WHERE [Name] = 'iOS'
END
GO

IF NOT EXISTS (SELECT 1 FROM OperatingSystemVersions WHERE [Name] = 'iPadOS 16')
BEGIN 
	INSERT INTO OperatingSystemVersions (Id_OS, [Name])
	SELECT Id, 'iPadOS 16'
	FROM OperatingSystems
	WHERE [Name] = 'iPadOS'
END
GO

-- Populate Manufacturers table
IF NOT EXISTS (SELECT 1 FROM Manufacturers WHERE [Name] = 'Apple')
BEGIN 
	INSERT INTO Manufacturers ([Name]) VALUES('Apple')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Manufacturers WHERE [Name] = 'Samsung')
BEGIN 
	INSERT INTO Manufacturers ([Name]) VALUES('Samsung')
END           
GO

-- Populate DeviceTypes table
IF NOT EXISTS (SELECT 1 FROM DeviceTypes WHERE [Name] = 'Phone')
BEGIN 
	INSERT INTO DeviceTypes ([Name]) VALUES('Phone')
END           
GO

IF NOT EXISTS (SELECT 1 FROM DeviceTypes WHERE [Name] = 'Tablet')
BEGIN 
	INSERT INTO DeviceTypes ([Name]) VALUES('Tablet')
END           
GO

-- Populate Processors table
IF NOT EXISTS (SELECT 1 FROM Processors WHERE [Name] = 'A16 Bionic')
BEGIN 
	INSERT INTO Processors ([Name]) VALUES('A16 Bionic')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Processors WHERE [Name] = 'Snapdragon 8')
BEGIN 
	INSERT INTO Processors ([Name]) VALUES('Snapdragon 8')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Processors WHERE [Name] = 'Apple M1')
BEGIN 
	INSERT INTO Processors ([Name]) VALUES('Apple M1')
END           
GO

--Populate RAMAmounts table
IF NOT EXISTS (SELECT 1 FROM RAMAmounts WHERE Amount = 8)
BEGIN 
	INSERT INTO RAMAmounts (Amount) VALUES(8)
END           
GO

IF NOT EXISTS (SELECT 1 FROM RAMAmounts WHERE Amount = 6)
BEGIN 
	INSERT INTO RAMAmounts (Amount) VALUES(6)
END           
GO

IF NOT EXISTS (SELECT 1 FROM RAMAmounts WHERE Amount = 12)
BEGIN 
	INSERT INTO RAMAmounts (Amount) VALUES(12)
END           
GO
IF NOT EXISTS (SELECT 1 FROM RAMAmounts WHERE Amount = 4)
BEGIN 
	INSERT INTO RAMAmounts (Amount) VALUES(4)
END           
GO

-- Populate Countries table
IF NOT EXISTS (SELECT 1 FROM Countries WHERE [Name] = 'United Kingdom')
BEGIN 
	INSERT INTO Countries ([Name]) VALUES('United Kingdom')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Countries WHERE [Name] = 'Romania')
BEGIN 
	INSERT INTO Countries ([Name]) VALUES('Romania')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Countries WHERE [Name] = 'India')
BEGIN 
	INSERT INTO Countries ([Name]) VALUES('India')
END           
GO

-- Populate Cities table
IF NOT EXISTS (SELECT 1 FROM Cities WHERE [Name] = 'London')
BEGIN 
	INSERT INTO Cities ([Name], Id_Country)
	SELECT 'London', Id
	FROM Countries
	WHERE [Name] = 'United Kingdom'
END
GO

IF NOT EXISTS (SELECT 1 FROM Cities WHERE [Name] = 'Cluj')
BEGIN 
	INSERT INTO Cities ([Name], Id_Country)
	SELECT 'Cluj', Id
	FROM Countries
	WHERE [Name] = 'Romania'
END
GO

IF NOT EXISTS (SELECT 1 FROM Cities WHERE [Name] = 'Mumbai')
BEGIN 
	INSERT INTO Cities ([Name], Id_Country)
	SELECT 'Mumbai', Id
	FROM Countries
	WHERE [Name] = 'India'
END
GO

-- Populate Locations table
IF NOT EXISTS (SELECT 1 FROM Locations WHERE [Address] = '1 Tower Place West, Tower Place')
BEGIN 
	INSERT INTO Locations ([Address], Id_City)
	SELECT '1 Tower Place West, Tower Place', Id
	FROM Cities
	WHERE [Name] = 'London'
END
GO

IF NOT EXISTS (SELECT 1 FROM Locations WHERE [Address] = 'no. 77, 21st December 1989, Block E-F, 6th Floor, The Office')
BEGIN 
	INSERT INTO Locations ([Address], Id_City)
	SELECT 'no. 77, 21st December 1989, Block E-F, 6th Floor, The Office', Id
	FROM Cities
	WHERE [Name] = 'Cluj'
END
GO

IF NOT EXISTS (SELECT 1 FROM Locations WHERE [Address] = 'Supreme IT Park, Hiranandani Gardens')
BEGIN 
	INSERT INTO Locations ([Address], Id_City)
	SELECT 'Supreme IT Park, Hiranandani Gardens', Id
	FROM Cities
	WHERE [Name] = 'Mumbai'
END
GO

-- Populating Roles table
IF NOT EXISTS (SELECT 1 FROM Roles WHERE [Name] = 'Software Engineer')
BEGIN 
	INSERT INTO Roles ([Name]) VALUES('Software Engineer')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Roles WHERE [Name] = 'Graduate Software Engineer')
BEGIN 
	INSERT INTO Roles ([Name]) VALUES('Graduate Software Engineer')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Roles WHERE [Name] = 'Manager')
BEGIN 
	INSERT INTO Roles ([Name]) VALUES('Manager')
END           
GO

-- Populate Users table
IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'john.doe@darwinmail.com')
BEGIN 
	INSERT INTO Users (FirstName, LastName, Id_Role, Id_Location, [Password], Email) VALUES('John', 'Doe', 1, 1, 'password123', 'john.doe@darwinmail.com')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'mihaiela.nagy@darwinmail.com')
BEGIN 
	INSERT INTO Users (FirstName, LastName, Id_Role, Id_Location, [Password], Email) VALUES('Mihaiela', 'Nagy', 2, 2, 'verysecurepassword!5', 'mihaiela.nagy@darwinmail.com')
END           
GO

IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'anshu.mahal@darwinmail.com')
BEGIN 
	INSERT INTO Users (FirstName, LastName, Id_Role, Id_Location, [Password], Email) VALUES('Anshu', 'Mahal', 3, 3, 'verysecurepassword!123', 'anshu.mahal@darwinmail.com')
END           
GO

-- Populate Devices table
IF NOT EXISTS (SELECT 1 FROM Devices WHERE [Name] = 'Samsung Galaxy S22')
BEGIN 
	INSERT INTO Devices ([Name], Id_Manufacturer, Id_DeviceType, Id_OSVersion, Id_Processor, Id_RAMAmount, Id_CurrentUser)
	VALUES('Samsung Galaxy S22',2,1,2,2,1,1)
END           
GO

IF NOT EXISTS (SELECT 1 FROM Devices WHERE [Name] = 'Samsung Galaxy S23')
BEGIN 
	INSERT INTO Devices ([Name], Id_Manufacturer, Id_DeviceType, Id_OSVersion, Id_Processor, Id_RAMAmount, Id_CurrentUser)
	VALUES('Samsung Galaxy S23',2,1,2,2,1,1)
END           
GO

IF NOT EXISTS (SELECT 1 FROM Devices WHERE [Name] = 'iPhone 14 Pro')
BEGIN 
	INSERT INTO Devices ([Name], Id_Manufacturer, Id_DeviceType, Id_OSVersion, Id_Processor, Id_RAMAmount)
	VALUES('iPhone 14 Pro',1,1,3,1,2)
END           
GO

IF NOT EXISTS (SELECT 1 FROM Devices WHERE [Name] = 'iPhone 14')
BEGIN 
	INSERT INTO Devices ([Name], Id_Manufacturer, Id_DeviceType, Id_OSVersion, Id_Processor, Id_RAMAmount)
	VALUES('iPhone 14',1,1,3,1,2)
END           
GO

IF NOT EXISTS (SELECT 1 FROM Devices WHERE [Name] = 'iPad 10')
BEGIN 
	INSERT INTO Devices ([Name], Id_Manufacturer, Id_DeviceType, Id_OSVersion, Id_Processor, Id_RAMAmount)
	VALUES('iPad 10',1,2,5,3,4)
END           
GO

IF NOT EXISTS (SELECT 1 FROM Devices WHERE [Name] = 'SAMSUNG Galaxy Tab S8')
BEGIN 
	INSERT INTO Devices ([Name], Id_Manufacturer, Id_DeviceType, Id_OSVersion, Id_Processor, Id_RAMAmount)
	VALUES('SAMSUNG Galaxy Tab S8',2,2,2,2,1)
END           
GO

IF NOT EXISTS (SELECT 1 FROM Devices WHERE [Name] = 'Samsung Galaxy Tab A8')
BEGIN 
	INSERT INTO Devices ([Name], Id_Manufacturer, Id_DeviceType, Id_OSVersion, Id_Processor, Id_RAMAmount)
	VALUES('Samsung Galaxy Tab A8',2,2,2,2,1)
END           
GO