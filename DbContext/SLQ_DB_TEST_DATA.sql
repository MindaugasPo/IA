insert into Assets (Id, Title, Ticker, AssetType, CreatedDateUtc)
values
(newid(), 'Microsoft', 'MSFT', 1, getdate()),
(newid(), 'Apple', 'AAPL', 1, getdate()),
(newid(), 'Tesla', 'TSLA', 1, getdate()),
(newid(), 'Amazon', 'AMZN', 1, getdate()),
(newid(), 'Gold', 'XAU', 2, getdate()),
(newid(), 'Silver', 'XAG', 2, getdate())

insert into Portfolios (Id, Title, CreatedDateUtc)
values
('3DA15C4C-D24D-4881-94FE-AF666FE835EB', 'Awesome Portfolio', getdate())

declare @assetId uniqueidentifier
declare @portfolioId uniqueidentifier
set @assetId = (select top 1 Id from Assets)
set @portfolioId = (select top 1 Id from Portfolios)

insert into [dbo].[Transactions]
(Id, AssetId, PortfolioId, OpenPrice, OpenDateUtc, Amount, Commission, TransactionType, Currency, ClosePrice, CloseDateUtc, CreatedDateUtc)
values
(newid(), @assetId, @portfolioId, 100, dateadd(d, -20, getdate()), 5, 10, 1, 1, 120, dateadd(d, -10, getdate()), getdate()),
(newid(), @assetId, @portfolioId, 100, getdate(), 5, 10, 1, 1, null, null, getdate()),
(newid(), @assetId, @portfolioId, 100, getdate(), 5, 10, 1, 1, null, null, getdate())

insert into AssetPrices (Id, AssetId, [Date], OpenPrice, HighPrice, LowPrice, ClosePrice, CreatedDateUtc)
values
(newid(), @assetId, '2020-01-01', 1, 5, 1, 2, getdate()),
(newid(), @assetId, '2020-01-02', 2, 5, 1, 3, getdate()),
(newid(), @assetId, '2020-01-03', 3, 6, 2, 4, getdate()),
(newid(), @assetId, '2020-01-04', 4, 6, 3, 6, getdate()),
(newid(), @assetId, '2020-01-05', 6, 6, 5, 6, getdate()),
(newid(), @assetId, '2020-01-06', 6, 8, 4, 4, getdate())
