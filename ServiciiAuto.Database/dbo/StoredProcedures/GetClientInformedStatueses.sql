CREATE PROCEDURE [dbo].[GetClientInformedStatueses]
AS
BEGIN
	SELECT [Id],StatusName FROM dbo.ClientInformedStatus;
END
