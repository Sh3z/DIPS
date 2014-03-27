EXEC tSQLt.NewTestClass 'testRetrieveNextPatientID';
GO

CREATE PROCEDURE testRetrieveNextPatientID
AS
BEGIN
	DECLARE @table TABLE(result int);
	DECLARE @table2 TABLE(result int);
	DECLARE @keyword varchar(10) = 'Sep',
	@keyword2 varchar(10) = 'Aug';

	EXEC tSQLt.FakeTable 'patient';

	INSERT INTO @table2 EXEC [dbo].[spr_RetrieveNextPatientID_v001] @keyword2;
	DECLARE @expect int; SET @expect = 1;
	DECLARE @actual int; SET @actual = (SELECT result FROM @table2);
	EXEC tSQLt.AssertEquals @expect,@actual;

	INSERT INTO patient (patientID) VALUES ('Sep_003');
	INSERT INTO patient (patientID) VALUES ('Sep_004');
    INSERT INTO @table EXEC [dbo].[spr_RetrieveNextPatientID_v001] @keyword;

	SET @expect = 5;
	SET @actual = (SELECT result FROM @table);
	EXEC tSQLt.AssertEquals @expect,@actual;

	
END;
GO