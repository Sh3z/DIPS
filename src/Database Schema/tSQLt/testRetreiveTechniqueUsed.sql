EXEC tSQLt.NewTestClass 'testRetreiveTechniqueUsed';
GO

CREATE PROCEDURE testRetreiveTechniqueUsed
AS
BEGIN
	DECLARE @table TABLE(method varchar(100),technique xml,id int);
	DECLARE @imageUID varchar(70) = '183.138.1.0.98',
	@name varchar(100) = 'Gamma',
	@technique xml = '<pipeline><algorithms><algorithm name="gamma"><properties><property name="gamma" value="3" /></properties></algorithm></algorithms></pipeline>';


	EXEC tSQLt.FakeTable 'processedImages';
	EXEC tSQLt.FakeTable 'imageProcessing';
	INSERT INTO imageProcessing (ID,name,technique) VALUES (2,@name,@technique);
	INSERT INTO imageProcessing (ID,name,technique) VALUES (2,'Blurr','<pipeline></pipeline>');
	INSERT INTO imageProcessing (ID,name,technique) VALUES (2,@name,@technique);
	INSERT INTO imageProcessing (ID,name,technique) VALUES (2,'Brighten','<pipeline></pipeline>');
	INSERT INTO imageProcessing (ID,name,technique) VALUES (8,'Dummy',@technique);
	INSERT INTO processedImages (imageUID,processMethod) VALUES (@imageUID,2);
	INSERT INTO processedImages (imageUID,processMethod) VALUES ('183.138.1',8);

    INSERT INTO @table EXEC [dbo].[spr_RetreiveTechniqueUsed_v001] @imageUID;
	DECLARE @rowCount int;
	SET @rowCount = (SELECT COUNT(method) from @table);
	EXEC tSQLt.AssertEquals 4,@rowCount;

	DECLARE @table2 TABLE(method varchar(100),technique xml,id int);
	DECLARE @expected varchar(100);
	INSERT INTO @table2 EXEC [dbo].[spr_RetreiveTechniqueUsed_v001] '183.138.1';
	SET @expected = (SELECT method from @table2);
	EXEC tSQLt.AssertEquals 'Dummy',@expected;
END;
GO