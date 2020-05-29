delete from Transactions
go
delete from AssetPrices
go
delete from Assets
go
insert into Assets (Id, Title, Ticker, AssetType, CreatedDateUtc)
values
(newid(), 'Microsoft', 'MSFT', 1, getdate()),
(newid(), 'Apple', 'AAPL', 1, getdate()),
(newid(), 'Tesla', 'TSLA', 1, getdate()),
(newid(), 'Amazon', 'AMZN', 1, getdate()),
(newid(), 'Gold', 'XAU', 2, getdate()),
(newid(), 'Silver', 'XAG', 2, getdate())

declare @assetId uniqueidentifier
set @assetId = (select Id from Assets where [Title]='Microsoft')

insert into AssetPrices (Id, AssetId, [Date], OpenPrice, HighPrice, LowPrice, ClosePrice, CreatedDateUtc)
values
(newid(), @assetId, '2020-01-03',158.32,159.95,158.06,158.62, getdate()),
(newid(), @assetId, '2020-01-06',157.08,159.10,156.51,159.03, getdate()),
(newid(), @assetId, '2020-01-07',159.32,159.67,157.32,157.58, getdate()),
(newid(), @assetId, '2020-01-08',158.93,160.80,157.95,160.09, getdate()),
(newid(), @assetId, '2020-01-09',161.84,162.22,161.03,162.09, getdate()),
(newid(), @assetId, '2020-01-10',162.82,163.22,161.18,161.34, getdate()),
(newid(), @assetId, '2020-01-13',161.76,163.31,161.26,163.28, getdate()),
(newid(), @assetId, '2020-01-14',163.39,163.60,161.72,162.13, getdate()),
(newid(), @assetId, '2020-01-15',162.62,163.94,162.57,163.18, getdate()),
(newid(), @assetId, '2020-01-16',164.35,166.24,164.03,166.17, getdate()),
(newid(), @assetId, '2020-01-17',167.42,167.47,165.43,167.10, getdate()),
(newid(), @assetId, '2020-01-21',166.68,168.19,166.43,166.50, getdate()),
(newid(), @assetId, '2020-01-22',167.40,167.49,165.68,165.70, getdate()),
(newid(), @assetId, '2020-01-23',166.19,166.80,165.27,166.72, getdate()),
(newid(), @assetId, '2020-01-24',167.51,167.53,164.45,165.04, getdate()),
(newid(), @assetId, '2020-01-27',161.15,163.38,160.20,162.28, getdate()),
(newid(), @assetId, '2020-01-28',163.78,165.76,163.07,165.46, getdate()),
(newid(), @assetId, '2020-01-29',167.84,168.75,165.69,168.04, getdate()),
(newid(), @assetId, '2020-01-30',174.05,174.05,170.79,172.78, getdate())

set @assetId = (select Id from Assets where [Title]='Apple')
insert into AssetPrices (Id, AssetId, [Date], OpenPrice, HighPrice, LowPrice, ClosePrice, CreatedDateUtc)
values
(newid(), @assetId, '2020-01-02',296.24,300.60,295.19,300.35, getdate()),
(newid(), @assetId, '2020-01-03',297.15,300.58,296.50,297.43, getdate()),
(newid(), @assetId, '2020-01-06',293.79,299.96,292.75,299.80, getdate()),
(newid(), @assetId, '2020-01-07',299.84,300.90,297.48,298.39, getdate()),
(newid(), @assetId, '2020-01-08',297.16,304.44,297.16,303.19, getdate()),
(newid(), @assetId, '2020-01-09',307.24,310.43,306.20,309.63, getdate()),
(newid(), @assetId, '2020-01-10',310.60,312.67,308.25,310.33, getdate()),
(newid(), @assetId, '2020-01-13',311.64,317.07,311.15,316.96, getdate()),
(newid(), @assetId, '2020-01-14',316.70,317.57,312.17,312.68, getdate()),
(newid(), @assetId, '2020-01-15',311.85,315.50,309.55,311.34, getdate()),
(newid(), @assetId, '2020-01-16',313.59,315.70,312.09,315.24, getdate()),
(newid(), @assetId, '2020-01-17',316.27,318.74,315.00,318.73, getdate()),
(newid(), @assetId, '2020-01-21',317.19,319.02,316.00,316.57, getdate()),
(newid(), @assetId, '2020-01-22',318.58,319.99,317.31,317.70, getdate()),
(newid(), @assetId, '2020-01-23',317.92,319.56,315.65,319.23, getdate()),
(newid(), @assetId, '2020-01-24',320.25,323.33,317.52,318.31, getdate()),
(newid(), @assetId, '2020-01-27',310.06,311.77,304.88,308.95, getdate()),
(newid(), @assetId, '2020-01-28',312.60,318.40,312.19,317.69, getdate()),
(newid(), @assetId, '2020-01-29',324.45,327.85,321.38,324.34, getdate()),
(newid(), @assetId, '2020-01-30',320.54,324.09,318.75,323.87, getdate())

set @assetId = (select Id from Assets where [Title]='Tesla')
insert into AssetPrices (Id, AssetId, [Date], OpenPrice, HighPrice, LowPrice, ClosePrice, CreatedDateUtc)
values
(newid(), @assetId, '2020-01-02',424.50,430.70,421.71,430.26, getdate()),
(newid(), @assetId, '2020-01-03',440.50,454.00,436.92,443.01, getdate()),
(newid(), @assetId, '2020-01-06',440.47,451.56,440.00,451.54, getdate()),
(newid(), @assetId, '2020-01-07',461.40,471.63,453.36,469.06, getdate()),
(newid(), @assetId, '2020-01-08',473.70,498.49,468.23,492.14, getdate()),
(newid(), @assetId, '2020-01-09',497.10,498.80,472.87,481.34, getdate()),
(newid(), @assetId, '2020-01-10',481.79,484.94,473.70,478.15, getdate()),
(newid(), @assetId, '2020-01-13',493.50,525.63,492.00,524.86, getdate()),
(newid(), @assetId, '2020-01-14',544.26,547.41,524.90,537.92, getdate()),
(newid(), @assetId, '2020-01-15',529.76,537.84,516.79,518.50, getdate()),
(newid(), @assetId, '2020-01-16',493.75,514.46,492.17,513.49, getdate()),
(newid(), @assetId, '2020-01-17',507.61,515.67,503.16,510.50, getdate()),
(newid(), @assetId, '2020-01-21',530.25,548.58,528.41,547.20, getdate()),
(newid(), @assetId, '2020-01-22',571.89,594.50,559.10,569.56, getdate()),
(newid(), @assetId, '2020-01-23',564.25,582.00,555.60,572.20, getdate()),
(newid(), @assetId, '2020-01-24',570.63,573.86,554.26,564.82, getdate()),
(newid(), @assetId, '2020-01-27',541.99,564.44,539.28,558.02, getdate()),
(newid(), @assetId, '2020-01-28',568.49,576.81,558.08,566.90, getdate()),
(newid(), @assetId, '2020-01-29',575.69,589.80,567.43,580.99, getdate()),
(newid(), @assetId, '2020-01-30',632.42,650.88,618.00,640.81, getdate())

set @assetId = (select Id from Assets where [Title]='Amazon')
insert into AssetPrices (Id, AssetId, [Date], OpenPrice, HighPrice, LowPrice, ClosePrice, CreatedDateUtc)
values
(newid(), @assetId, '2020-01-02',1875.00,1898.01,1864.15,1898.01, getdate()),
(newid(), @assetId, '2020-01-03',1864.50,1886.20,1864.50,1874.97, getdate()),
(newid(), @assetId, '2020-01-06',1860.00,1903.69,1860.00,1902.88, getdate()),
(newid(), @assetId, '2020-01-07',1904.50,1913.89,1892.04,1906.86, getdate()),
(newid(), @assetId, '2020-01-08',1898.04,1911.00,1886.44,1891.97, getdate()),
(newid(), @assetId, '2020-01-09',1909.89,1917.82,1895.80,1901.05, getdate()),
(newid(), @assetId, '2020-01-10',1905.37,1906.94,1880.00,1883.16, getdate()),
(newid(), @assetId, '2020-01-13',1891.31,1898.00,1880.80,1891.30, getdate()),
(newid(), @assetId, '2020-01-14',1885.88,1887.11,1858.55,1869.44, getdate()),
(newid(), @assetId, '2020-01-15',1872.25,1878.86,1855.09,1862.02, getdate()),
(newid(), @assetId, '2020-01-16',1882.99,1885.59,1866.02,1877.94, getdate()),
(newid(), @assetId, '2020-01-17',1885.89,1886.64,1857.25,1864.72, getdate()),
(newid(), @assetId, '2020-01-21',1865.00,1894.27,1860.00,1892.00, getdate()),
(newid(), @assetId, '2020-01-22',1896.09,1902.50,1883.34,1887.46, getdate()),
(newid(), @assetId, '2020-01-23',1885.11,1889.98,1872.76,1884.58, getdate()),
(newid(), @assetId, '2020-01-24',1891.37,1894.99,1847.44,1861.64, getdate()),
(newid(), @assetId, '2020-01-27',1820.00,1841.00,1815.34,1828.34, getdate()),
(newid(), @assetId, '2020-01-28',1840.50,1858.11,1830.02,1853.25, getdate()),
(newid(), @assetId, '2020-01-29',1864.00,1874.75,1855.02,1858.00, getdate()),
(newid(), @assetId, '2020-01-30',1858.00,1872.87,1850.61,1870.68, getdate())
