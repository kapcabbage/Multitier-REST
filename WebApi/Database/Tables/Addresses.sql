CREATE TABLE [dbo].[Addresses]
(
	[AddressId] INT NOT NULL IDENTITY(1,1), 
	[CustomerId] VARCHAR(5) NOT NULL,
	[AddressType] VARCHAR(1) NOT NULL,
	[Name] NVARCHAR (100) NOT NULL,
	[Street] NVARCHAR(100) NOT NULL,
	[Zip] VARCHAR(20) NOT NULL,
	[City] NVARCHAR(100) NOT NULL,
	[Country] VARCHAR(2) NOT NULL,

	CONSTRAINT [PK_AddressId] PRIMARY KEY ([AddressId]),
	CONSTRAINT [FK_Address_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers](CustomerId)
)
