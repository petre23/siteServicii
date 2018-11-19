CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Username] NVARCHAR(250) NOT NULL,
	[Password] NVARCHAR(300) NOT NULL,
	[RoleLevel] int NOT NULL DEFAuLT(0) -- 0 -> admin, 1 -> read and write, 2 -- read only
)
