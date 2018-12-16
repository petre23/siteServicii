CREATE PROCEDURE [dbo].[GetClientById]
	@clientId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT c.Id,c.Email,c.Name,c.PhoneNumber
	FROM dbo.Clients c
	WHERE c.Id = @clientId
END
