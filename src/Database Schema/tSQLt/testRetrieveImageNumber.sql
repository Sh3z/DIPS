EXEC tSQLt.NewTestClass 'testRetrieveImageNumber';
GO

CREATE PROCEDURE testRetrieveImageNumber
AS
BEGIN
	DECLARE @table TABLE(result int);
	DECLARE @table2 TABLE(result int);
	DECLARE @patient int = 2, @series int = 33, @number varchar(5) = '02';

	EXEC tSQLt.FakeTable 'imageProperties';
	EXEC tSQLt.FakeTable 'images';

	INSERT INTO imageProperties (patientID, seriesID) VALUES (@patient,@series);
	INSERT INTO images (seriesID, imageNumber) VALUES (@series,@number);

	INSERT INTO @table EXEC [dbo].[spr_RetrieveImageNumber_v001] @patient, @series, @number;
	DECLARE @expect int; SET @expect = 1;
	DECLARE @actual int; SET @actual = (SELECT result FROM @table);
	EXEC tSQLt.AssertEquals @expect,@actual;

	INSERT INTO @table2 EXEC [dbo].[spr_RetrieveImageNumber_v001] @patient, @series, '05';
	SET @expect = 0;
	SET @actual = (SELECT result FROM @table2);
	EXEC tSQLt.AssertEquals @expect,@actual;
	
END;
GO