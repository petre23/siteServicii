CREATE PROCEDURE [dbo].[GetVehicleTypes]
AS
BEGIN
	SELECT [Id],Name FROM dbo.VehicleType;
END
