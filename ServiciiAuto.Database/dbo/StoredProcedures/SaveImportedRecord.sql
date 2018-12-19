CREATE PROCEDURE [dbo].[SaveImportedRecord]
	@RecordId UNIQUEIDENTIFIER,
	@ExpirationDate Datetime,
	@CreationDate Datetime,
	@CarRegistartionNumber Nvarchar(255),
	@AdditionalInfo NVarchar(max) = NULL,
	@RecordTypeName NVARCHAR(255) = NULL,
	@Email NVARCHAR(255) = NULL,
	@PhoneNumber NVARCHAR(255) = NULL,
	@ClientName NVARCHAR(300) = NULL,
	@ModifiedByUser UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @RecordType INT = (SELECT Id FROM RecordTypes WHERE TypeName = @RecordTypeName);

	IF(@RecordType IS NULL)
	BEGIN
		INSERT INTO RecordTypes VALUES(@RecordTypeName)
		SET @RecordType = (SELECT Id FROM RecordTypes WHERE TypeName = @RecordTypeName)
	END

	DECLARE @ClientId UNIQUEIDENTIFIER = NULL;

	IF(@PhoneNumber IS NOT NULL)
	BEGIN
		SET @ClientId = (SELECT Id FROM Clients WHERE PhoneNumber = @PhoneNumber)
		IF(@ClientId IS NULL)
		BEGIN
			DECLARE @newClientId UNIQUEIDENTIFIER = NEWID()
			INSERT INTO Clients VALUES
			(
				@newClientId,
				@PhoneNumber,
				@Email,
				@ClientName
			)
			SET @ClientId = @newClientId
		END
	END
	ELSE
	BEGIN
		SET @ClientId = (SELECT CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER));
	END

	INSERT INTO dbo.Records(Id,ExpirationDate,CreationDate,CarRegistartionNumber,AdditionalInfo,ClientId,RecordType,ModifiedByUser)
		VALUES
		(
			@RecordId,
			@ExpirationDate,
			@CreationDate,
			@CarRegistartionNumber,
			@AdditionalInfo,
			@ClientId,
			@RecordType,
			@ModifiedByUser
		);
END