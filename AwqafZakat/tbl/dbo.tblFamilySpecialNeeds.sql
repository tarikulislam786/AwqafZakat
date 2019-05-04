CREATE TABLE [dbo].[tblFamilySpecialNeeds]
(
    [familySpecialNeedId] INT IDENTITY (1, 1) NOT NULL,
    [familyRegID]       INT            NULL,
    [familySpecialneed]  NVARCHAR (255) NULL,
     PRIMARY KEY CLUSTERED ([familySpecialNeedId] ASC), 
      CONSTRAINT [FK_tblFamilySpecialNeeds_tblFamilyRegistrations] FOREIGN KEY ([familyRegID]) REFERENCES [tblFamilyRegistrations]([familyRegID])
)
