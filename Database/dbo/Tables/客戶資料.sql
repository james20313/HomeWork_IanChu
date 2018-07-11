CREATE TABLE [dbo].[客戶資料] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [客戶名稱]      NVARCHAR (50)  NOT NULL,
    [統一編號]      CHAR (8)       NOT NULL,
    [電話]        NVARCHAR (50)  NOT NULL,
    [傳真]        NVARCHAR (50)  NULL,
    [地址]        NVARCHAR (100) NULL,
    [Email]     NVARCHAR (250) NULL,
    [IsDeleted] BIT            CONSTRAINT [DF_客戶資料_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Customers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

