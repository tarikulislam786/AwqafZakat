﻿CREATE TABLE [dbo].[tblAdminRegistrations] (
    [adminRegistrationID]   INT            IDENTITY (1, 1) NOT NULL,
    [administratorNumber]   INT    unique        NOT NULL,
    [registrationDateHijri] DATE           NULL,
    [registrationDate]      DATE           NULL,
    [tripleNameTribe]       NVARCHAR (50)  NULL,
    [civilNumber]           NVARCHAR (50) unique NULL,
    [railNumber]            NVARCHAR (50)  NULL,
    [homeNumber]            NVARCHAR (50)  NULL,
    [mosqueName]            NVARCHAR (100) NULL,
    [regionName]            NVARCHAR (50)  NULL,
    [supervisorWhatsapp]    NVARCHAR (50)  NULL,
    [supervisorOfficePhone] NVARCHAR (50)  NULL,
    [supervisorHomePhone]   NVARCHAR (50)  NULL,
    [relativeName]          NVARCHAR (100) NULL,
    [relativePhone]         NVARCHAR (50)  NULL,
    [assistantName]         NVARCHAR (100) NULL,
    [assistantPhone]        NVARCHAR (50)  NULL,
    [adminIdCard]           NVARCHAR (100) NULL,
    [assistantIdCard]       NVARCHAR (100) NULL,
    [chairmanName]          NVARCHAR (100) NULL,
    [vchairmanName]         NVARCHAR (100) NULL,
    [notes]                 NVARCHAR (MAX) NULL,
    [hardcopyAttach]        NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([adminRegistrationID] ASC)
);

