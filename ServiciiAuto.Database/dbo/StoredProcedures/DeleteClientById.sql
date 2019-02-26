CREATE PROCEDURE [dbo].[DeleteClientById]
	@clientId UNIQUEIDENTIFIER 
AS
BEGIN
	DELETE FROM dbo.Records WHERE ClientId = @clientId; 
	DELETE FROM dbo.Clients WHERE Id = @clientId;
END