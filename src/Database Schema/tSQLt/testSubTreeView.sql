EXEC tSQLt.NewTestClass 'testSubTreeView';
GO

CREATE PROCEDURE testSubTreeView
AS
BEGIN
	DECLARE @table TABLE(result int);
	DECLARE @seriesID int = 2,
	@fileID int = 3;

	EXEC tSQLt.FakeTable 'images';
	INSERT INTO images (fileID,seriesID) VALUES (@fileID,@seriesID);
	INSERT INTO images (fileID,seriesID) VALUES (@fileID,3);
	INSERT INTO @table EXEC [dbo].[spr_SubTreeView_v001] @seriesID;

	DECLARE @actual int;
	SET @actual = (SELECT result FROM @table);
	EXEC tSQLt.AssertEquals @fileID,@actual;
END;
GO