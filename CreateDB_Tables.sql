-- Create the database
IF NOT EXISTS (
    SELECT 1
    FROM sys.databases
    WHERE name = 'DeviceManagement'
)
BEGIN
    CREATE DATABASE DeviceManagement;
    PRINT 'DeviceManagement database created successfully.';
END
GO

-- Select the database to use
USE DeviceManagement;
GO

-- DEVICES INFO TABLES
-- Create OperatingSystems table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'OperatingSystems')
BEGIN
    CREATE TABLE OperatingSystems (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [Name] VARCHAR(200) NOT NULL
    );
    PRINT 'OperatingSystems table created successfully.';
END
GO

-- Create OperatingSystemVersions table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'OperatingSystemVersions')
BEGIN
    CREATE TABLE OperatingSystemVersions (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        Id_OS INT NOT NULL,
        [Name] VARCHAR(200) NOT NULL,
        FOREIGN KEY (Id_OS) REFERENCES OperatingSystems (Id)
    );
    PRINT 'OperatingSystemVersions table created successfully.';
END
GO

-- Create Manufacturers table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Manufacturers')
BEGIN
    CREATE TABLE Manufacturers (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [Name] VARCHAR(200) NOT NULL
    );
    PRINT 'Manufacturers table created successfully.';
END
GO

-- Create DeviceTypes table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'DeviceTypes')
BEGIN
    CREATE TABLE DeviceTypes (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [Name] VARCHAR(200) NOT NULL
    );
    PRINT 'Manufacturers table created successfully.';
END
GO

-- Create Processors table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Processors')
BEGIN
    CREATE TABLE Processors (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [Name] VARCHAR(200) NOT NULL
    );
    PRINT 'Processor table created successfully.';
END
GO

--Create RAMAmounts table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'RAMAmounts')
BEGIN
	CREATE TABLE RAMAmounts(
		Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		Amount INT NOT NULL
	);
	PRINT 'RAMAmounts table created successfully';
END
GO


-- USERS TABLES
-- Create Countries table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Countries')
BEGIN    
	CREATE TABLE Countries (
		Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		[Name] VARCHAR(200) NOT NULL
	);
	PRINT 'Countries table created successfully.';
END
GO

-- Create Cities table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Cities')
BEGIN
    CREATE TABLE Cities (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [Name] VARCHAR(200) NOT NULL,
        Id_Country INT NOT NULL,
        FOREIGN KEY (Id_Country) REFERENCES Countries(Id)
    );
    PRINT 'Cities table created successfully.';
END
GO

-- Create Locations table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Locations')
BEGIN
    CREATE TABLE Locations (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        [Address] VARCHAR(200) NOT NULL,
        Id_City INT NOT NULL,
        FOREIGN KEY (Id_City) REFERENCES Cities(Id)
    );
    PRINT 'Locations table created successfully.';
END
GO

-- Create Roles table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Roles')
BEGIN   
    CREATE TABLE Roles (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        Name VARCHAR(200) NOT NULL
    );
    PRINT 'Roles table created successfully.';
END
GO

-- Create Users table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        FirstName VARCHAR(200) NOT NULL,
        LastName VARCHAR(200) NOT NULL,
        Id_Role INT NOT NULL,
        Id_Location INT NOT NULL,
        [Password] VARCHAR(200) NOT NULL,
        Email VARCHAR(200) NOT NULL UNIQUE,
        FOREIGN KEY (Id_Role) REFERENCES Roles(Id),
		FOREIGN KEY (Id_Location) REFERENCES Locations(Id)
	);
    PRINT 'Users table created successfully.';
END
GO

-- DEVICES TABLE
-- Create Devices table
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Devices')
BEGIN
    CREATE TABLE Devices (
        [Id] INT PRIMARY KEY IDENTITY(1,1),
        [Name] VARCHAR(50) NOT NULL,
        [Id_Manufacturer] INT NOT NULL,
		[Id_DeviceType] INT NOT NULL,
        [Id_OSVersion] INT NOT NULL,
        [Id_Processor] INT NOT NULL,
        [Id_RAMAmount] INT NOT NULL,
        [Id_CurrentUser] INT,
        FOREIGN KEY (Id_Manufacturer) REFERENCES Manufacturers (Id),
        FOREIGN KEY (Id_DeviceType) REFERENCES DeviceTypes(Id),
        FOREIGN KEY (Id_OSVersion) REFERENCES OperatingSystemVersions (Id),
        FOREIGN KEY (Id_Processor) REFERENCES Processors (Id),
        FOREIGN KEY (Id_RAMAmount) REFERENCES RAMAmounts (Id),
        FOREIGN KEY (Id_CurrentUser) REFERENCES Users (Id)
    );
    PRINT 'Devices table created successfully.';
END
GO

