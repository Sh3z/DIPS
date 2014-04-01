EXEC tSQLt.NewTestClass 'testRetrieveSelectedTechnique';
GO

CREATE PROCEDURE testRetrieveSelectedTechnique
AS
BEGIN
	DECLARE @table TABLE(technique xml);
	DECLARE @id int = 2, @technique xml;
	SET @technique = '<pipeline><algorithms><algorithm name="gamma"><properties><property name="gamma" value="3" /></properties></algorithm></algorithms></pipeline>';


	EXEC tSQLt.FakeTable 'imageProcessing';
	INSERT INTO imageProcessing (ID,name,technique) VALUES (1,'Gamma',@technique);
	INSERT INTO imageProcessing (ID,name,technique) VALUES (2,'Blurr',@technique);
	INSERT INTO imageProcessing (ID,name,technique) VALUES (3,'Gamma 2.0',@technique);

    INSERT INTO @table EXEC [dbo].[spr_RetrieveSelectedTechnique_v001] @id;

	DECLARE @rowCount int;
	SET @rowCount = (SELECT COUNT(technique) from @table);
	EXEC tSQLt.AssertEquals 1,@rowCount;

	DECLARE @actual int,@result xml;
	SET @result = (SELECT technique from @table);
	IF @result IS NULL SET @actual = 0;
	ELSE SET @actual = 1;
	EXEC tSQLt.AssertEquals 1,@actual;
END;
GO