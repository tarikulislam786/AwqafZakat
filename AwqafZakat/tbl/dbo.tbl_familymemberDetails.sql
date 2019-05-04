CREATE TABLE [dbo].[tblFamilymemberDetails] (
    [memberID]             INT            IDENTITY (1, 1) NOT NULL,
    [familyRegID]          INT            NULL,
    [educationLevelID]     INT            NULL,
    [familymembername]     NVARCHAR (100) NULL,
    [memberCivilNumber]    NVARCHAR (50)  unique NULL,
    [case]                 NVARCHAR (100) NULL,
    [relativerelation]     NVARCHAR (50)  NULL,
    [educationalInstitute] NVARCHAR (255) NULL,
    [memberNotes]          NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([memberID] ASC),
    CONSTRAINT [FK_tblFamilymemberDetails_tblFamilyRegistrations] FOREIGN KEY ([familyRegID]) REFERENCES [dbo].[tblFamilyRegistrations] ([familyRegID]),
    CONSTRAINT [FK_tblFamilymemberDetails_tblEducationLevels] FOREIGN KEY ([educationLevelID]) REFERENCES [dbo].[tblEducationLevels] ([educationLevelID])
);

