EXEC tSQLt.NewTestClass 'testRetrieveAllTechnique';
GO

CREATE PROCEDURE testRetrieveAllTechnique
AS
BEGIN
	DECLARE @table TABLE(id int, method varchar(100), technique xml);
	DECLARE @id int, @name varchar(100), @technique xml;
	SET @technique = '<pipeline><algorithms><algorithm name="gamma"><properties><property name="gamma" value="3" /></properties></algorithm></algorithms></pipeline>';

	EXEC tSQLt.FakeTable 'imageProcessing';
	INSERT INTO imageProcessing (ID,name,technique) VALUES (1,'Gamma',@technique);
	INSERT INTO imageProcessing (ID,name,technique) VALUES (2,'Blurr',@technique);
	INSERT INTO imageProcessing (ID,name,technique) VALUES (3,'Gamma 2.0',@technique);
	INSERT INTO imageProcessing (ID,name,technique) VALUES (4,'Brighten',@technique);
	INSERT INTO imageProcessing (ID,name,technique) VALUES (5,'Gamma -3',@technique);

    INSERT INTO @table EXEC [dbo].[spr_RetrieveAllTechnique_v001];

	DECLARE @rowCount int;
	SET @rowCount = (SELECT COUNT(technique) from @table);
	EXEC tSQLt.AssertEquals 5,@rowCount;


END;
GO