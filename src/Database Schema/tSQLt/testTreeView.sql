EXEC tSQLt.NewTestClass 'testTreeView';
GO

CREATE PROCEDURE testTreeView
AS
BEGIN
	DECLARE @table TABLE(id varchar(20),name varchar(20),tableID int,series varchar(10),seriesID int);
	DECLARE @table2 TABLE(id varchar(20),name varchar(20),tableID int,series varchar(10),seriesID int);
	DECLARE @tableID int = 1, @patientID varchar(10) = 'Aug_098', @seriesID int = 3, @patientName varchar(20) = 'cf128HBNHk';
	DECLARE @series varchar(10) = 'Foot MRI', @modified datetime = cast('1753-1-1' as datetime);

	EXEC tSQLt.FakeTable 'patient';
	EXEC tSQLt.FakeTable 'name';
	EXEC tSQLt.FakeTable 'imageProperties';
	INSERT INTO patient (tableID,patientID) VALUES (@tableID,@patientID);
	INSERT INTO name (patientID,patientName) VALUES (@tableID,@patientName);
	INSERT INTO imageProperties (patientID,seriesID,seriesDescription,lastModifiedDate) 
	VALUES (@tableID,@seriesID,@series,@modified);
	INSERT INTO @table EXEC [dbo].[spr_TreeView_v001];

	DECLARE @actual varchar(20);
	SET @actual = (SELECT name FROM @table);
	EXEC tSQLt.AssertEquals @patientName,@actual;

	SET @modified = cast('2013-1-1' as datetime)
	INSERT INTO patient (tableID,patientID) VALUES (33,@patientID);
	INSERT INTO name (patientID,patientName) VALUES (33,'HeheHaha');
	INSERT INTO imageProperties (patientID,seriesID,seriesDescription,lastModifiedDate) 
	VALUES (33,@seriesID,@series,@modified);
	INSERT INTO @table2 EXEC [dbo].[spr_TreeView_v001];

	SET @actual = (SELECT TOP(1) name FROM @table2);
	EXEC tSQLt.AssertEquals 'HeheHaha',@actual;
END;
GO