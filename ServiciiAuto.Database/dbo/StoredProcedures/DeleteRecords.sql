CREATE PROCEDURE [dbo].[DeleteRecords]
	@recordId UNIQUEIDENTIFIER 
AS
BEGIN
	DELETE FROM dbo.Records WHERE Id = @recordId;
END
