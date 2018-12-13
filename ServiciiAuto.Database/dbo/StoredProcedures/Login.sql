CREATE PROCEDURE [dbo].[Login]
	@Username NVARCHAR(255),
	@Password NVARCHAR(255)
AS
BEGIN
	SELECT TOP 1 
			u.Id,
			u.Username,
			u.Password,
			u.RoleLevel
			FROM dbo.Users u 
			WHERE u.Username = @Username 
			AND u.Password = @Password
END