EXEC tSQLt.NewTestClass 'testUpdateSeriesAvailable';
GO

CREATE PROCEDURE testUpdateSeriesAvailable
AS
BEGIN
	DECLARE @id int = 2, @initial int = 8;

	EXEC tSQLt.FakeTable 'patient';
	INSERT INTO patient (tableID,seriesAvailable) VALUES (@id,@initial);

	DECLARE @actual int;
	SET @actual = (SELECT seriesAvailable FROM patient);
	EXEC tSQLt.AssertEquals @initial,@actual;

	EXEC [dbo].[spr_UpdateSeriesAvailable_v001] @id;

	SET @actual = (SELECT seriesAvailable FROM patient);
	EXEC tSQLt.AssertEquals 9,@actual;
END;
GO