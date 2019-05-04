CREATE TABLE [dbo].[tblUsers]
(
	[userID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [fname] NVARCHAR(50) NULL, 
    [lname] NVARCHAR(50) NULL, 
    [userName] NVARCHAR(100) unique not NULL, 
    [email] NVARCHAR(100) unique NULL, 
    [password] NVARCHAR(MAX) NULL, 
    [isAdmin] BIT NULL
)
