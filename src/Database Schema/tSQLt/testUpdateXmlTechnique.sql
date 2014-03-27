EXEC tSQLt.NewTestClass 'testUpdateXmlTechnique';
GO

CREATE PROCEDURE testUpdateXmlTechnique
AS
BEGIN
	DECLARE @id int = 2,
	@name varchar(100) = 'Gamma 0.0',
	@technique xml = NULL;

	EXEC tSQLt.FakeTable 'imageProcessing';
	INSERT INTO imageProcessing (ID,name,technique) VALUES (@id,@name,@technique);

	DECLARE @actualName varchar(20), @actualTech xml, @empty int;
	SET @actualName = (SELECT name FROM imageProcessing);
	SET @actualTech = (SELECT technique FROM imageProcessing);
	IF @actualTech IS NULL SET @empty = 1;
	ELSE SET @empty = 0;
	EXEC tSQLt.AssertEquals @name,@actualName;
	EXEC tSQLt.AssertEquals 1,@empty

	DECLARE @newName varchar(100) = 'Gamma 3.0', 
	@newTech xml = '<pipeline><algorithms><algorithm name="gamma"><properties><property name="gamma" value="3" /></properties></algorithm></algorithms></pipeline>';
	EXEC [dbo].[spr_UpdateXmlTechnique_v001] @id,@newName,@newTech;

	SET @actualName = (SELECT name FROM imageProcessing);
	SET @actualTech = (SELECT technique FROM imageProcessing);
	IF @actualTech IS NULL SET @empty = 1;
	ELSE SET @empty = 0;
	EXEC tSQLt.AssertEquals @newName,@actualName;
	EXEC tSQLt.AssertEquals 0,@empty;
END;
GO