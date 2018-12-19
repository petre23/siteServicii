INSERT INTO Users VALUES(NEWID(),'petre@test.ro','test',1);
INSERT INTO VehicleType VALUES('vehicle 1');
INSERT INTO RecordTypes VALUES('Asigurare');
INSERT INTO Clients VALUES((SELECT CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)),' ',NULL,'Utilizator necunoscut');