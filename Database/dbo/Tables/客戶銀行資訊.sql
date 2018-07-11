CREATE TABLE [dbo].[客戶銀行資訊] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [客戶Id]      INT           NOT NULL,
    [銀行名稱]      NVARCHAR (50) NOT NULL,
    [銀行代碼]      INT           NOT NULL,
    [分行代碼]      INT           NULL,
    [帳戶名稱]      NVARCHAR (50) NOT NULL,
    [帳戶號碼]      NVARCHAR (20) NOT NULL,
    [IsDeleted] BIT           CONSTRAINT [DF_客戶銀行資訊_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_客戶銀行資訊] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_客戶銀行資訊_客戶資料] FOREIGN KEY ([客戶Id]) REFERENCES [dbo].[客戶資料] ([Id])
);

