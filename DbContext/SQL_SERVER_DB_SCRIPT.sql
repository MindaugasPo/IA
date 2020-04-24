create database IA
GO

create table [IA].[dbo].[Assets](
	Id uniqueidentifier PRIMARY KEY,
	Title nvarchar(200) not null,
	Ticker nvarchar(10) null,
	AssetType int not null,
	CreatedDateUtc datetime2 not null
)
GO

create table [IA].[dbo].[Version] (
	[Key] nvarchar(10),
	[Value] int
)
GO

insert into [IA].[dbo].[Version] 
([Key], [Value]) values ('DbVersion', 1)
GO

create table [IA].[dbo].[Portfolios] (
	Id uniqueidentifier,
	Title nvarchar(50) not null,
	CreatedDateUtc datetime2 not null,
	constraint PK_Portfolios primary key nonclustered (Id)
)
GO

create table [IA].[dbo].[Transactions](
	Id uniqueidentifier,
	AssetId uniqueidentifier not null,
	PortfolioId uniqueidentifier not null,
	OpenPrice decimal (19,4) not null,
	OpenDateUtc datetime2 not null,
	Amount decimal(19,4) not null,
	Commission decimal(19,4) not null,
	TransactionType int not null,
	Currency int not null,
	ClosePrice decimal (19,4) null,
	CloseDateUtc datetime2 null,
	CreatedDateUtc datetime2 not null,
	constraint PK_Transactions primary key nonclustered (Id),
	constraint FK_Transactions_Assets foreign key (AssetId) references Assets (Id),
	constraint FK_Transactions_Portfolio foreign key (PortfolioId) references Portfolios (Id)
)
GO

update [IA].[dbo].[Version]
set [Value] = 2
where [Key] = 'DbVersion'
GO

create table [IA].[dbo].[AssetPrices] (
	Id uniqueidentifier,
	AssetId uniqueidentifier not null,
	[Date] datetime2 not null,
	OpenPrice decimal(19,4) not null,
	HighPrice decimal(19,4) not null,
	LowPrice decimal(19,4) not null,
	ClosePrice decimal(19,4) not null,
	CreatedDateUtc datetime2 not null,
	constraint PK_AssetPrices primary key nonclustered (Id),
	constraint FK_AssetPrices_Assets foreign key (AssetId) references Assets (Id),
)

update [IA].[dbo].[Version]
set [Value] = 3
where [Key] = 'DbVersion'
GO
