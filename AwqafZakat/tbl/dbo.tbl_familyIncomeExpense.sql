CREATE TABLE [dbo].[tblFamilyIncomeExpense] (
    [familyIncomeExpenseID] INT            IDENTITY (1, 1) NOT NULL,
    [familyRegID]           INT            NULL,
    [type]                  NCHAR (1)      NULL,
    [particular]            NVARCHAR (255) NULL,
    [quantity]              INT            NULL,
    [amount]                DECIMAL (18)   NULL,
    PRIMARY KEY CLUSTERED ([familyIncomeExpenseID] ASC),
    CONSTRAINT [FK_tblFamilyIncomeExpense_tblFamilyRegistrations] FOREIGN KEY ([familyRegID]) REFERENCES [dbo].[tblFamilyRegistrations] ([familyRegID])
);

