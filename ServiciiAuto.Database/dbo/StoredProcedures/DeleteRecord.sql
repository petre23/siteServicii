CREATE PROCEDURE [dbo].[DeleteRecord]
	@recordId UNIQUEIDENTIFIER 
AS
BEGIN
	DELETE FROM dbo.Records WHERE Id = @recordId;
END