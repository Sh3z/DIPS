EXEC tSQLt.NewTestClass 'testSelectProperties';
GO

CREATE PROCEDURE testSelectProperties
AS
BEGIN
	DECLARE @table TABLE(patientID varchar(30),birthdate varchar(10),age varchar(10),sex char(1),
	bodyPart varchar(20),study varchar(20),series varchar(20),sliceThickness varchar(20));

	DECLARE @id varchar(10) = 'FEB_034', @birth varchar(10) = '1923-09-19', @pAge varchar(10) = '89_Y';
	DECLARE @gender char(1) = 'M', @sID int = 1, @body varchar(10) = 'Arm', @study varchar(10) = 'Armstrong';
	DECLARE @series varchar(10) = 'Bone Crack', @thickness varchar(10) = '5.099', @fID int = 3, @tableID int = 5;

	EXEC tSQLt.FakeTable 'patient';
	EXEC tSQLt.FakeTable 'imageProperties';
	EXEC tSQLt.FakeTable 'images';
	INSERT INTO patient (tableID,patientID,birthdate,age,sex) VALUES (@tableID,@id,@birth,@pAge,@gender);
	INSERT INTO imageProperties (patientID,seriesID,bodyPart,studyDescription,seriesDescription,sliceThickness) 
	VALUES (@tableID,@sID,@body,@study,@series,@thickness);
	INSERT INTO images (seriesID,fileID) VALUES (@sID,@fID);

    INSERT INTO @table EXEC [dbo].[spr_SelectProperties_v001] @fID;

	DECLARE @actualID varchar(10),@actualSeries varchar(10);
	SET @actualID = (SELECT patientID FROM @table);
	SET @actualSeries = (SELECT series FROM @table);
	EXEC tSQLt.AssertEquals @id,@actualID;
	EXEC tSQLt.AssertEquals @series,@actualSeries;

	DECLARE @table2 TABLE(patientID varchar(30),birthdate varchar(10),age varchar(10),sex char(1),
	bodyPart varchar(20),study varchar(20),series varchar(20),sliceThickness varchar(20));

	DECLARE @id2 varchar(10) = 'Aug_039', @birth2 varchar(10) = NULL, @pAge2 varchar(10) = NULL;
	DECLARE @gender2 char(1) = NULL, @sID2 int = 2, @body2 varchar(10) = NULL, @study2 varchar(10) = NULL;
	DECLARE @series2 varchar(10) = NULL, @thickness2 varchar(10) = NULL, @fID2 int = 5, @tableID2 int = 8;

	INSERT INTO patient (tableID,patientID,birthdate,age,sex) VALUES (@tableID2,@id2,@birth2,@pAge2,@gender2);
	INSERT INTO imageProperties (patientID,seriesID,bodyPart,studyDescription,seriesDescription,sliceThickness) 
	VALUES (@tableID2,@sID2,@body2,@study2,@series2,@thickness2);
	INSERT INTO images (seriesID,fileID) VALUES (@sID2,@fID2);

	INSERT INTO @table2 EXEC [dbo].[spr_SelectProperties_v001] @fID2;

	SET @actualID = (SELECT patientID FROM @table2);
	EXEC tSQLt.AssertEquals @id2,@actualID;
END;
GO