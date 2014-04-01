EXEC tSQLt.NewTestClass 'testCheckSeriesUID';
GO

CREATE PROCEDURE testCheckSeriesUID
AS
BEGIN
	DECLARE @table TABLE(result int);
    DECLARE @table2 TABLE(result int);
	DECLARE @series VARCHAR(70);
	SET @series = '1.32';

	EXEC tSQLt.FakeTable 'imageProperties';
	INSERT INTO imageProperties (seriesUID) VALUES ('1.32');
    INSERT INTO @table EXEC [dbo].[spr_CheckSeriesUIDExist_v001] @series;
	INSERT INTO @table2 EXEC [dbo].[spr_CheckStudyUIDExist_v001] '1.1.1.1';

	DECLARE @expect int; SET @expect = 1;
	DECLARE @actual int; SET @actual = (SELECT 1 FROM @table);
	EXEC tSQLt.AssertEquals @expect,@actual;

	SET @actual = (SELECT 1 FROM @table2);
	EXEC tSQLt.AssertEquals NULL, @actual;
END;
GO