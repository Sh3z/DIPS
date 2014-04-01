EXEC tSQLt.NewTestClass 'testUpdateModifiedDate';
GO

CREATE PROCEDURE testUpdateModifiedDate
AS
BEGIN
	DECLARE @id int = 2, @initial datetime = cast('2010-1-1' as datetime);

	EXEC tSQLt.FakeTable 'imageProperties';
	INSERT INTO imageProperties (seriesID,lastModifiedDate) VALUES (@id,@initial);

	DECLARE @actual datetime,@diff int;
	SET @actual = (SELECT lastModifiedDate FROM imageProperties);
	EXEC tSQLt.AssertEquals @initial,@actual;

	EXEC [dbo].[spr_UpdateModifiedDate_v001] @id;

	SET @actual = (SELECT lastModifiedDate FROM imageProperties);
	SET @diff = DATEDIFF(day,current_timestamp,@actual);
	EXEC tSQLt.AssertEquals 0,@diff;
END;
GO