EXEC tSQLt.NewTestClass 'testCustomList';
GO

CREATE PROCEDURE testCustomList
AS
BEGIN
	DECLARE @table TABLE(id varchar(20),name varchar(100),tableID int,series varchar(50),seriesID int);
    DECLARE @table2 TABLE(id varchar(20),name varchar(100),tableID int,series varchar(50),seriesID int);
	DECLARE @Sex varchar(1) = 'F',
	@IDContains varchar(20) = '99',
	@Batch int = 20,
	@modality varchar(15) = NULL,
	@from date = cast('2009-1-1' as date),
	@to date = cast('2013-1-1' as date);
	DECLARE @creation date = cast('2011-9-1' as date),
	@modified date = cast('2013-1-1' as datetime),
	@modified2 date = cast('2014-1-1' as datetime);

	EXEC tSQLt.FakeTable 'patient';
	EXEC tSQLt.FakeTable 'name';
	EXEC tSQLt.FakeTable 'imageProperties';

	INSERT INTO patient (tableID,patientID,sex) VALUES (122,'Jan_099',@Sex);
	INSERT INTO name (patientID,patientName) VALUES (122,'HelloWorld123');
	INSERT INTO imageProperties (patientID,seriesID,seriesDescription,modality,creationDate,lastModifiedDate) 
	VALUES (122,34,'Testing',@modality,@creation,@modified);

	INSERT INTO patient (tableID,patientID,sex) VALUES (133,'Jan_092',@Sex);
	INSERT INTO name (patientID,patientName) VALUES (133,'GoodBye321');
	INSERT INTO imageProperties (patientID,seriesID,seriesDescription,modality,creationDate,lastModifiedDate) 
	VALUES (133,36,'Testing','CT',@creation,@modified2);

	INSERT INTO @table EXEC [dbo].[spr_CustomList_v001] @Sex,@IDContains,@Batch,@modality,@from,@to;
	INSERT INTO @table2 EXEC [dbo].[spr_CustomList_v001];

	DECLARE @rowCount int
	SET @rowCount = (SELECT COUNT(id) from @table);
	EXEC tSQLt.AssertEquals 1,@rowCount;

	SET @rowCount = (SELECT COUNT(id) from @table2);
	EXEC tSQLt.AssertEquals 2,@rowCount;

	DECLARE @actual varchar(20); 
	SET @actual = (SELECT name FROM @table);
	EXEC tSQLt.AssertEquals 'HelloWorld123',@actual;

	SET @actual = (SELECT TOP(1) name FROM @table2);
	EXEC tSQLt.AssertEquals 'GoodBye321',@actual;
END;
GO