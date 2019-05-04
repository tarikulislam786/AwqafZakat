CREATE TABLE [dbo].[tblFamilyPropertyDetails]
(
	[familyPropertyDetailID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [familyRegID] INT NULL, 
    [familyProperty] NVARCHAR(255) NULL, 
    [numOfProperty] INT NULL, 
    CONSTRAINT [FK_tblFamilyPropertyDetails_tblFamilyRegistrations] FOREIGN KEY ([familyRegID]) REFERENCES [tblFamilyRegistrations]([familyRegID])
)
