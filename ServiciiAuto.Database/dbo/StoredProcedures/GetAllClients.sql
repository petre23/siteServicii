CREATE PROCEDURE [dbo].[GetAllClients]
AS
BEGIN
	SELECT c.Id,c.Email,c.Name,c.PhoneNumber
	FROM dbo.Clients c
END