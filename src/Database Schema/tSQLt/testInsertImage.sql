EXEC tSQLt.NewTestClass 'testInsertImage';
GO

CREATE PROCEDURE testInsertImage
AS
BEGIN
	DECLARE @imgID int = 22,
	@imgUID varchar(70) = '23.234.1.23',
	@imgNum varchar(5) = '093',
	@imgBlob varbinary(Max) = 0xFFD8FFE000104A46494600010101006000600000;

	EXEC tSQLt.FakeTable 'images';
	EXEC [dbo].[spr_InsertImages_v001] @imgID,@imgUID,@imgNum,@imgBlob;

	DECLARE @uid VARCHAR(70), @blob VARBINARY(MAX)
	SET @uid = (SELECT imageUID FROM images);
	EXEC tSQLt.AssertEquals @imgUID,@uid;
	SET @blob = (SELECT imageBlob FROM images);
	DECLARE @actual int;
	IF @blob IS NULL SET @actual = 0;
	ELSE SET @actual = 1;
	EXEC tSQLt.AssertEquals 1,@actual;
END;
GO