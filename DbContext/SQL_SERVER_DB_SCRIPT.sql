create database [IA]
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


CREATE TABLE [IA].[dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [IA].[dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [IA].[dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [IA].[dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [IA].[dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [IA].[dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE TABLE [IA].[dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [IA].[dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [IA].[dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [IA].[dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [IA].[dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [IA].[dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [IA].[dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [IA].[dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [IA].[dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [IA].[dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [IA].[dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [IA].[dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [IA].[dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [IA].[dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [IA].[dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [IA].[dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [IA].[dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [IA].[dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [IA].[dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO


update [IA].[dbo].[Version]
set [Value] = 4
where [Key] = 'DbVersion'
GO

-- IMPORTANT !
-- This DELETE code should not be run in actual prod system, as it deletes existing portfolios.
-- better is to add nullable column, populate it (however it is suitable for your deployment),
-- add roreign key constraint, and make the column non nullable.
DELETE FROM [IA].[dbo].[Portfolios] 
GO

ALTER TABLE [IA].[dbo].[Portfolios] 
ADD UserId nvarchar(450) not null

ALTER TABLE [IA].[dbo].[Portfolios] WITH CHECK ADD  CONSTRAINT [FK_Portfolios_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [IA].[dbo].[AspNetUsers] ([Id])
GO

update [IA].[dbo].[Version]
set [Value] = 5
where [Key] = 'DbVersion'
GO
