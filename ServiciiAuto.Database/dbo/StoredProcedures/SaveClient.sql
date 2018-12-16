CREATE PROCEDURE [dbo].[SaveClient]
	@IsNew bit,
	@Id UNIQUEIDENTIFIER,
	@Email NVARCHAR(300) = NULL,
	@Name NVARCHAR(255) = NULL,
	@PhoneNumber NVARCHAR(20) = NULL
AS
BEGIN
	IF(@IsNew = 1)
	BEGIN
		INSERT INTO dbo.Clients(Id,Name,Email,PhoneNumber)
		VALUES
		(
			@Id,
			@Name,
			@Email,
			@PhoneNumber
		);
	END
	ELSE
	BEGIN
		UPDATE dbo.Clients SET
			Name = @Name,
			Email = @Email,
			PhoneNumber = @PhoneNumber
		WHERE Id = @Id
	END
END
