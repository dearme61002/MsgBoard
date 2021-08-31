USE MsgBoard
Go

CREATE VIEW vwDisplayPost AS
SELECT [dbo].[Posting].[PostID], 
	   [dbo].[Posting].[Title],
	   [dbo].[Accounting].[Name],
	   [dbo].[Posting].[CreateDate],
	   [dbo].[Accounting].[Level],
	   [dbo].[Posting].[ismaincontent]
FROM [dbo].[Posting] 
INNER JOIN [dbo].[Accounting]
ON [dbo].[Posting].[UserID] = [dbo].[Accounting].[UserID]
Go

CREATE VIEW vwDisplayMsg AS
SELECT [dbo].[Message].[PostID],
	   [dbo].[Message].[Body], 
       [dbo].[Accounting].[Name],
	   [dbo].[Message].[CreateDate]	   
FROM [dbo].[Message] 
INNER JOIN [dbo].[Accounting]
ON [dbo].[Message].[UserID] = [dbo].[Accounting].[UserID]
Go