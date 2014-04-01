EXEC tSQLt.NewTestClass 'testDeleteExcessLog';
GO

CREATE PROCEDURE testDeleteExcessLog
AS
BEGIN

	EXEC tSQLt.FakeTable 'timeLog';

	DECLARE @Iterator INT
	SET @Iterator = 0
 
	WHILE (@Iterator < 21)
	BEGIN
		INSERT INTO timeLog (logID) VALUES (@Iterator+1);
		Set @Iterator = @Iterator + 1
	END

	DECLARE @rowCount int
	SET @rowCount = (SELECT COUNT(logID) from timeLog);

	EXEC tSQLt.AssertEquals 21,@rowCount;
    EXEC [dbo].[spr_DeleteExcessLog_v001];

	SET @rowCount = (SELECT COUNT(logID) from timeLog);

	EXEC tSQLt.AssertEquals 20,@rowCount;

END;
GO