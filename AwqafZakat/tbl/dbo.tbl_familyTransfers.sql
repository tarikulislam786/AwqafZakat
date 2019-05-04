﻿CREATE TABLE [dbo].[tblFamilyTransfers] (
    [familyTransferID]  INT            IDENTITY (1, 1) NOT NULL,
    [familyRegID]       INT            NULL,
    [transferDate]      DATE           NULL,
    [ecoordinate]       INT            NULL,
    [ncoordinate]       INT            NULL,
    [oldRegion]         NVARCHAR (100) NULL,
    [newRegion]         NVARCHAR (100) NULL,
    [oldVillage]        NVARCHAR (100) NULL,
    [newVillage]        NVARCHAR (100) NULL,
    [oldRailNumber]     NVARCHAR (50)  NULL,
    [newRailNumber]     NVARCHAR (50)  NULL,
    [oldHomeNumber]     NVARCHAR (50)  NULL,
    [newHomeNumber]     NVARCHAR (50)  NULL,
    [oldMosque]         NVARCHAR (100) NULL,
    [newMosque]         NVARCHAR (100) NULL,
    [oldMobile]         NVARCHAR (50)  NULL,
    [oldTelephone]      NVARCHAR (50)  NULL,
    [newMobile]         NVARCHAR (50)  NULL,
    [newTelephone]      NVARCHAR (50)  NULL,
    [prevAdmin]         NVARCHAR (50)  NULL,
    [prevAdminPhone]    NVARCHAR (50)  NULL,
    [currentAdmin]      NVARCHAR (50)  NULL,
    [currentAdminPhone] NVARCHAR (50)  NULL,
    [chairmanName]      NVARCHAR (100) NULL,
    [vchairmanName]     NVARCHAR (100) NULL,
    [electricityAttach] NVARCHAR (255) NULL,
    [hardcopyAttach]    NVARCHAR (255) NULL,
    [notes]             NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([familyTransferID] ASC),
    CONSTRAINT [FK_tblFamilyTransfers_tblFamilyRegistrations] FOREIGN KEY ([familyRegID]) REFERENCES [dbo].[tblFamilyRegistrations] ([familyRegID])
);

