EXEC tSQLt.NewTestClass 'testRetrieveImage';
GO

CREATE PROCEDURE testRetrieveImage
AS
BEGIN
	DECLARE @table TABLE(imageBlob varbinary(Max),imageUID varchar(70));
	DECLARE @fID int = 2;

	EXEC tSQLt.FakeTable 'images';
	INSERT INTO images (fileID,imageBlob,imageUID) VALUES (1,0xFFD8FFE,'1.1.1.1');
	INSERT INTO images (fileID,imageBlob,imageUID) VALUES (2,0xFFD7FFE,'3.3.0.3');
	INSERT INTO @table EXEC [dbo].[spr_RetrieveImage_v001] @fID;

	DECLARE @uid VARCHAR(70), @blob VARBINARY(MAX)
	SET @uid = (SELECT imageUID FROM @table);
	EXEC tSQLt.AssertEquals '3.3.0.3',@uid;
	SET @blob = (SELECT imageBlob FROM @table);
	DECLARE @actual int;
	IF @blob IS NULL SET @actual = 0;
	ELSE SET @actual = 1;
	EXEC tSQLt.AssertEquals 1,@actual;
END;
GO