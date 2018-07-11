CREATE TABLE [dbo].[客戶聯絡人] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [客戶Id]      INT            NOT NULL,
    [職稱]        NVARCHAR (50)  NOT NULL,
    [姓名]        NVARCHAR (50)  NOT NULL,
    [Email]     NVARCHAR (250) NOT NULL,
    [手機]        NVARCHAR (50)  NULL,
    [電話]        NVARCHAR (50)  NULL,
    [IsDeleted] BIT            CONSTRAINT [DF_客戶聯絡人_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Contacts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_客戶聯絡人_客戶資料] FOREIGN KEY ([客戶Id]) REFERENCES [dbo].[客戶資料] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CustomerId]
    ON [dbo].[客戶聯絡人]([客戶Id] ASC);

