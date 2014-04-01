EXEC tSQLt.NewTestClass 'testInsertTechnique';
GO

CREATE PROCEDURE testInsertTechnique
AS
BEGIN
	DECLARE @name varchar(100),
	@technique xml = '<pipeline><algorithms><algorithm name="gamma"><properties><property name="gamma" value="3" /></properties></algorithm></algorithms></pipeline>';

	EXEC tSQLt.FakeTable 'imageProcessing';
	EXEC [dbo].[spr_InsertTechnique_v001] @name,@technique;

	DECLARE @actualName varchar(100), @actualTechnique xml;
	SET @actualName = (SELECT name FROM imageProcessing);
	EXEC tSQLt.AssertEquals @name,@actualName;
	SET @actualTechnique = (SELECT technique FROM imageProcessing);
	DECLARE @exist int;
	IF @actualTechnique IS NULL SET @exist = 0;
	ELSE SET @exist = 1;
	EXEC tSQLt.AssertEquals 1,@exist;
END;
GO