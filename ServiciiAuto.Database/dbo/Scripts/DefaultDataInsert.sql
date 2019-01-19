INSERT INTO Users VALUES(NEWID(),'petre@test.ro','098f6bcd4621d373cade4e832627b4f6',1);
INSERT INTO Users VALUES(NEWID(),'cristi.autoconsult@gmail.com','41421953d99ef8264d8d7495e96a3f03',1); -- pass mobatehnic!
INSERT INTO Users VALUES(NEWID(),'readonlymode@test.ro','098f6bcd4621d373cade4e832627b4f6',0); -- pass test

INSERT INTO VehicleType VALUES('Tir');
INSERT INTO VehicleType VALUES('Berlină');
INSERT INTO VehicleType VALUES('Break');
INSERT INTO VehicleType VALUES('Coupé');
INSERT INTO VehicleType VALUES('Cabriolet');
INSERT INTO VehicleType VALUES('Limuzină');
INSERT INTO VehicleType VALUES('Roadster');
INSERT INTO VehicleType VALUES('Spider');
INSERT INTO VehicleType VALUES('Pick-up');

INSERT INTO dbo.ClientInformedStatus VALUES
(
'Client informat prin SMS'
)

INSERT INTO dbo.ClientInformedStatus VALUES
(
'Client informat prin apel telefonic'
)

INSERT INTO Clients VALUES((SELECT CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)),' ',NULL,'Client necunoscut');