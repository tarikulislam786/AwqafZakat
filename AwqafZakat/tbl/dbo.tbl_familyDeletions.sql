CREATE TABLE [dbo].[tblFamilyDeletions] (
    [familyDeletionID]  INT            IDENTITY (1, 1) NOT NULL,
    [familyRegID]       INT            NULL,
    [deletionDate]      DATE           NULL,
    [familyName]    NVARCHAR (255) NULL,
    [deletionReason]    NVARCHAR (255) NULL,
    [familyIncomeAtDel] DECIMAL (18)   NULL,
    [adminName]         NVARCHAR (100) NULL,
    [chairmanName]      NVARCHAR (100) NULL,
    [vchairmanName]     NVARCHAR (100) NULL,
    [hardcopyAttach]    NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([familyDeletionID] ASC),
    CONSTRAINT [FK_tblFamilyDeletions_tblFamilyRegistrations] FOREIGN KEY ([familyRegID]) REFERENCES [dbo].[tblFamilyRegistrations] ([familyRegID])
);

