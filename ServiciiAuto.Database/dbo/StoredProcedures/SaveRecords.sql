CREATE PROCEDURE [dbo].[SaveRecords]
	@IsNew bit,
	@RecordId UNIQUEIDENTIFIER,
	@ExpirationDate Datetime,
	@CreationDate Datetime,
	@CarRegistartionNumber Nvarchar(255),
	@AdditionalInfo NVarchar(max) = NULL,
	@ClientId UNIQUEIDENTIFIER = NULL,
	@RecordType INT,
	@Email NVARCHAR(255) = NULL,
	@PhoneNumber NVARCHAR(255),
	@ClientName NVARCHAR(300),
	@VehicleTypeId INT
AS
BEGIN
	
	DECLARE @newClient UNIQUEIDENTIFIER = NULL;
	IF(@ClientId IS NULL OR @ClientId = (SELECT CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)))
	BEGIN
		SET @newClient = NEWID();
		INSERT INTO dbo.Clients 
		VALUES
		(
			@newClient,
			@PhoneNumber,
			@Email,
			@ClientName
		)
	END
	ELSE
	BEGIN
		IF((SELECT TOP 1 Count(Id) FROM dbo.Clients where Id = @ClientId) > 0)
		BEGIN
			UPDATE Clients SET 
				Name = @ClientName,
				Email = @Email,
				PhoneNumber = @PhoneNumber
		END
		SET @newClient = @ClientId;
	END
	
	IF(@IsNew = 1)
	BEGIN
		INSERT INTO dbo.Records 
		VALUES
		(
			@RecordId,
			@ExpirationDate,
			@CreationDate,
			@CarRegistartionNumber,
			@AdditionalInfo,
			@newClient,
			@RecordType,
			@VehicleTypeId
		);
	END
	ELSE
	BEGIN
		UPDATE dbo.Records SET
			ExpirationDate = @ExpirationDate,
			CreationDate = @CreationDate,
			CarRegistartionNumber = @CarRegistartionNumber,
			AdditionalInfo = @AdditionalInfo,
			ClientId = @ClientId,
			RecordType = @RecordId,
			VehicleType = @VehicleTypeId
		WHERE Id = @RecordId
	END
END
