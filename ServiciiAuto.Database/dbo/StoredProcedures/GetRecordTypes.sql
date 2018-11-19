CREATE PROCEDURE [dbo].[GetRecordTypes]
AS
BEGIN
	SELECT [Id],TypeName FROM dbo.RecordTypes;
END
