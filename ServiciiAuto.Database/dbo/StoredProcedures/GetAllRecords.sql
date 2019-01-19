CREATE PROCEDURE [dbo].[GetAllRecords]
	@StartDate DateTime = null,
	@EndDate Datetime = null,
	@RecordType int = null,
	@ClientId UNIQUEIDENTIFIER = null,
	@PhoneNumber nvarchar(30) = null,
	@CarRegistrationNumber nvarchar(50) = null,
	@PageSize int = 50,
	@PageNumber int = 0
AS
BEGIN
	SELECT r.Id,
		   r.AdditionalInfo,
		   r.CarRegistartionNumber,
		   r.CreationDate,
		   r.ExpirationDate,
		   r.ClientId,
		   c.Name as ClientName,
		   r.RecordType,
		   c.PhoneNumber,
		   c.Email,
		   rt.TypeName as RecordTypeName,
		   rt.TypeName,
		   r.VehicleType,
		   vt.Name as VehicleTypeName,
		   r.Id,
		   u.Username,
		   r.ClientInformedStatus AS ClientInformedStatusId,
		   cis.StatusName as ClientInformedStatusName,
		   (SELECT Count(*) FROM dbo.Records r
				LEFT JOIN dbo.Clients c ON c.Id = r.ClientId
				LEFT JOIN dbo.RecordTypes rt ON rt.Id = r.RecordType
				LEFT JOIN dbo.VehicleType vt ON vt.Id = r.VehicleType
				INNER JOIN dbo.Users u ON u.Id = r.ModifiedByUser
				LEFT JOIN dbo.ClientInformedStatus cis ON cis.Id = r.ClientInformedStatus
				WHERE (@RecordType IS NULL OR r.RecordType = @RecordType)
				AND ((@startDate IS NULL AND @endDate IS NULL) OR  r.ExpirationDate BETWEEN @startDate AND @endDate)
				AND (@CarRegistrationNumber IS NULL OR r.CarRegistartionNumber like '%'+ (CASE WHEN @CarRegistrationNumber IS NOT NULL THEN @CarRegistrationNumber ELSE '' END ) +'%')
				AND (@PhoneNumber IS NULL OR c.PhoneNumber like '%'+ (CASE WHEN @PhoneNumber IS NOT NULL THEN @PhoneNumber ELSE '' END ) +'%')
				AND (@ClientId IS NULL OR r.ClientId = @ClientId)) AS TotalRows
	FROM dbo.Records r
	LEFT JOIN dbo.Clients c ON c.Id = r.ClientId
	LEFT JOIN dbo.RecordTypes rt ON rt.Id = r.RecordType
	LEFT JOIN dbo.VehicleType vt ON vt.Id = r.VehicleType
	INNER JOIN dbo.Users u ON u.Id = r.ModifiedByUser
	LEFT JOIN dbo.ClientInformedStatus cis ON cis.Id = r.ClientInformedStatus
	WHERE (@RecordType IS NULL OR r.RecordType = @RecordType)
	AND ((@startDate IS NULL AND @endDate IS NULL) OR  r.ExpirationDate BETWEEN @startDate AND @endDate)
	AND (@CarRegistrationNumber IS NULL OR r.CarRegistartionNumber like '%'+ (CASE WHEN @CarRegistrationNumber IS NOT NULL THEN @CarRegistrationNumber ELSE '' END ) +'%')
	AND (@PhoneNumber IS NULL OR c.PhoneNumber like '%'+ (CASE WHEN @PhoneNumber IS NOT NULL THEN @PhoneNumber ELSE '' END ) +'%')
	AND (@ClientId IS NULL OR r.ClientId = @ClientId)
	ORDER BY ExpirationDate
	OFFSET (@PageSize*@PageNumber) ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END