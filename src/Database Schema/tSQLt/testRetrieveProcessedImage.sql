EXEC tSQLt.NewTestClass 'testRetrieveProcessedImage';
GO

CREATE PROCEDURE testRetrieveProcessedImage
AS
BEGIN
	DECLARE @table TABLE(imageBlob varbinary(Max));
	DECLARE @fID int = 3, @processMethod varchar(100) = 'Gamma +3.0';

	EXEC tSQLt.FakeTable 'images';
	EXEC tSQLt.FakeTable 'processedImages';
	INSERT INTO images (fileID,imageUID) VALUES (3,'8.8.7.8');
	INSERT INTO images (fileID,imageUID) VALUES (2,'3.3.0.3');
	INSERT INTO images (fileID,imageUID) VALUES (1,'1.1.1.1');
	INSERT INTO processedImages (imageBlob,imageUID,processMethod) VALUES (0xFFD8FFE,'1.1.1.1',@processMethod);
	INSERT INTO processedImages (imageBlob,imageUID,processMethod) VALUES (0xFFD7FFE,'3.3.0.3','Gamma -2');
	INSERT INTO processedImages (imageBlob,imageUID,processMethod) VALUES (0xFFD9FFE,'8.8.7.8',@processMethod);
	INSERT INTO @table EXEC [dbo].[spr_RetrieveProcessedImage_v001] @fID,@processMethod;

	DECLARE @rowCount int, @blob varbinary(max);
	SET @rowCount = (SELECT COUNT(imageBlob) from @table);
	EXEC tSQLt.AssertEquals 1,@rowCount;

	SET @blob = (SELECT imageBlob FROM @table);
	DECLARE @actual int;
	IF @blob IS NULL SET @actual = 0;
	ELSE SET @actual = 1;
	EXEC tSQLt.AssertEquals 1,@actual;
END;
GO