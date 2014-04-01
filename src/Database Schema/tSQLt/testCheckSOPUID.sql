EXEC tSQLt.NewTestClass 'testCheckSOPUID';
GO

CREATE PROCEDURE testCheckSOPUID
AS
BEGIN
	DECLARE @table TABLE(result int);
	DECLARE @table2 TABLE(result int);
	DECLARE @SOP VARCHAR(70);
	SET @SOP = '1.32.27.348.134';

	EXEC tSQLt.FakeTable 'images';
	INSERT INTO images (imageUID) VALUES (@SOP);
    INSERT INTO @table EXEC [dbo].[spr_CheckSOPUIDExist_v001] @SOP;
	INSERT INTO @table2 EXEC [dbo].[spr_CheckStudyUIDExist_v001] '1.1.1.1';

	DECLARE @expect int; SET @expect = 1;
	DECLARE @actual int; SET @actual = (SELECT 1 FROM @table);
	EXEC tSQLt.AssertEquals @expect,@actual;

	SET @actual = (SELECT 1 FROM @table2);
	EXEC tSQLt.AssertEquals NULL, @actual;

END;
GO
