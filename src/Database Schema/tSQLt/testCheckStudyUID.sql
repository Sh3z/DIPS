EXEC tSQLt.NewTestClass 'testCheckStudyUID';
GO

CREATE PROCEDURE testCheckStudyUID
AS
BEGIN
	DECLARE @table TABLE(result int);
	DECLARE @table2 TABLE(result int);

	DECLARE @study VARCHAR(70);
	SET @study = '8.932.43.546.2.211.46667.4';

	EXEC tSQLt.FakeTable 'patient';
	INSERT INTO patient (studyUID) VALUES (@study);
    INSERT INTO @table EXEC [dbo].[spr_CheckStudyUIDExist_v001] @study;
	INSERT INTO @table2 EXEC [dbo].[spr_CheckStudyUIDExist_v001] '1.1.1.1';

	DECLARE @expect int; SET @expect = 1;
	DECLARE @actual int; SET @actual = (SELECT 1 FROM @table);
	EXEC tSQLt.AssertEquals @expect,@actual;

	SET @actual = (SELECT 1 FROM @table2);
	EXEC tSQLt.AssertEquals NULL, @actual;

END;
GO