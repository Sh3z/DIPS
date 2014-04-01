EXEC tSQLt.NewTestClass 'testCheckPatientExist';
GO

CREATE PROCEDURE testCheckPatientExist
AS
BEGIN
	DECLARE @table TABLE(tableID int, modality varchar(15), bodyPart varchar(20), study varchar(50), series varchar(50), seriesID int, name varchar(100));
    DECLARE @table2 TABLE(tableID int, modality varchar(15), bodyPart varchar(20), study varchar(50), series varchar(50), seriesID int, name varchar(100));
	DECLARE @birthdate varchar(10), @age varchar(10), @sex char(1);
	SET @birthdate = '19320923'; SET @age = '89_Y'; SET @sex = 'M';

	EXEC tSQLt.FakeTable 'patient';
	EXEC tSQLt.FakeTable 'name';
	EXEC tSQLt.FakeTable 'imageProperties';

	DECLARE @name varchar(15) = 'tcf38NNMb==', @study varchar(20) = 'Brain Tumor', @id int = 23;

	INSERT INTO patient (tableID,birthdate,age,sex) VALUES (@id,@birthdate,@age,@sex);
	INSERT INTO name (patientID,patientName) VALUES (@id,@name);
	INSERT INTO imageProperties (patientID,modality,bodyPart,studyDescription,seriesDescription,seriesID) 
	VALUES (@id,'TT','Left Brain',@study,'TT on Brain',89);

    INSERT INTO @table (tableID,modality,bodyPart,study,series,seriesID,name) EXEC [dbo].[spr_CheckPatientExist_v001] @birthdate, @age, @sex;
	INSERT INTO @table2 (tableID,modality,bodyPart,study,series,seriesID,name) EXEC [dbo].[spr_CheckPatientExist_v001] @birthdate, @age,'F';

	DECLARE @actualName varchar(15),@actualStudy varchar(20), @actualID int;

	SET @actualName = (SELECT name FROM @table);
	EXEC tSQLt.AssertEquals @name,@actualName;
	SET @actualStudy = (SELECT study FROM @table);
	EXEC tSQLt.AssertEquals @study,@actualStudy;
	SET @actualID = (SELECT tableID FROM @table);
	EXEC tSQLt.AssertEquals @id,@actualID;

	SET @actualName = (SELECT name FROM @table2);
	EXEC tSQLt.AssertEquals NULL,@actualName;
	SET @actualStudy = (SELECT study FROM @table2);
	EXEC tSQLt.AssertEquals NULL,@actualStudy;
	SET @actualID = (SELECT tableID FROM @table2);
	EXEC tSQLt.AssertEquals NULL,@actualID;
END;
GO