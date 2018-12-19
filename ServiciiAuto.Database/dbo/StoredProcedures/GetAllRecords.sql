CREATE PROCEDURE [dbo].[GetAllRecords]
	@startDate DateTime = null,
	@endDate Datetime = null,
	@recordType int = null
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
		   cis.StatusName as ClientInformedStatusName
	FROM dbo.Records r
	LEFT JOIN dbo.Clients c ON c.Id = r.ClientId
	LEFT JOIN dbo.RecordTypes rt ON rt.Id = r.RecordType
	LEFT JOIN dbo.VehicleType vt ON vt.Id = r.VehicleType
	INNER JOIN dbo.Users u ON u.Id = r.ModifiedByUser
	LEFT JOIN dbo.ClientInformedStatus cis ON cis.Id = r.ClientInformedStatus
	--WHERE (@recordType IS NULL OR r.RecordType = @recordType)
	--AND (r.ExpirationDate BETWEEN @startDate AND @endDate)
END
