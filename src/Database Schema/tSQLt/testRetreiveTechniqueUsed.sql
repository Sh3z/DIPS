EXEC tSQLt.NewTestClass 'testRetreiveTechniqueUsed';
GO

CREATE PROCEDURE testRetreiveTechniqueUsed
AS
BEGIN
	DECLARE @table TABLE(method varchar(100));
	DECLARE @imageUID varchar(70) = '183.138.1.0.98';

	EXEC tSQLt.FakeTable 'processedImages';
	INSERT INTO processedImages (imageUID,processMethod) VALUES (@imageUID,'Gamma');
	INSERT INTO processedImages (imageUID,processMethod) VALUES (@imageUID,'Blurr');
	INSERT INTO processedImages (imageUID,processMethod) VALUES ('1.2.23.4','Gamma');
	INSERT INTO processedImages (imageUID,processMethod) VALUES (@imageUID,'Brighten');
	INSERT INTO processedImages (imageUID,processMethod) VALUES (@imageUID,'Gamma');
	INSERT INTO processedImages (imageUID,processMethod) VALUES (@imageUID,'Gamma');

    INSERT INTO @table EXEC [dbo].[spr_RetreiveTechniqueUsed_v001] @imageUID;

	DECLARE @rowCount int;
	SET @rowCount = (SELECT COUNT(method) from @table);
	EXEC tSQLt.AssertEquals 3,@rowCount;
END;
GO