CREATE TABLE [dbo].[tblSettings]
(
	[settingsID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [settingsName] NVARCHAR(50) NULL, 
    [value] NVARCHAR(100) NULL, 
    [notes] NVARCHAR(MAX) NULL
)
