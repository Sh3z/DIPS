EXEC tSQLt.NewTestClass 'testCreateLog';
GO

CREATE PROCEDURE testCreateLog
AS
BEGIN

	EXEC tSQLt.FakeTable 'timeLog';

    EXEC [dbo].[spr_CreateLog_v001];

	DECLARE @result datetime;
	SET @result = (SELECT 1 FROM timeLog);

	DECLARE @actual int;
	SET @actual = ISDATE(@result);

	EXEC tSQLt.AssertEquals @actual,1;

END;
GO