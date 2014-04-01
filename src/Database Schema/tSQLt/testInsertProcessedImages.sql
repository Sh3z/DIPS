EXEC tSQLt.NewTestClass 'testInsertProcessedImages';
GO

CREATE PROCEDURE testInsertProcessedImages
AS
BEGIN
	DECLARE @processMethod int = 4,
	@imageUID varchar(70) = '183.18.2.1',
	@imageBlob varbinary(MAX) = 0xFFD8FFE000104A464946000;

	EXEC tSQLt.FakeTable 'processedImages';
	EXEC [dbo].[spr_InsertProcessedImages_v001] @processMethod,@imageUID,@imageBlob;

	DECLARE @method int, @uid varchar(70), @blob VARBINARY(MAX)
	SET @method = (SELECT processMethod FROM processedImages);
	EXEC tSQLt.AssertEquals @processMethod,@method;
	SET @uid = (SELECT imageUID FROM processedImages);
	EXEC tSQLt.AssertEquals @imageUID,@uid;
	SET @blob = (SELECT imageBlob FROM processedImages);
	DECLARE @actual int;
	IF @blob IS NULL SET @actual = 0;
	ELSE SET @actual = 1;
	EXEC tSQLt.AssertEquals 1,@actual;
END;
GO