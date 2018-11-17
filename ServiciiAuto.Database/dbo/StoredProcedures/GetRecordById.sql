﻿CREATE PROCEDURE [dbo].[GetRecordById]
	@recordId UNIQUEIDENTIFIER 
AS
BEGIN
	SELECT r.Id,
		   r.AdditionalInfo,
		   r.CarRegistartionNumber,
		   r.CreationDate,
		   r.ExpirationDate,
		   r.ClientId,
		   r.RecordType,
		   c.PhoneNumber,
		   c.Email,
		   c.Name as RecordTypeName,
		   rt.TypeName,
		   r.VehicleType,
		   vt.Name as VehicleTypeName
	FROM dbo.Records r
	INNER JOIN dbo.Clients c ON c.Id = r.ClientId
	INNER JOIN dbo.RecordTypes rt ON rt.Id = r.RecordType
	INNER JOIN dbo.VehicleType vt ON vt.Id = r.VehicleType
	WHERE r.Id = @recordId
END