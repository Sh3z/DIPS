EXEC tSQLt.NewTestClass 'testInsertPatient';
GO

CREATE PROCEDURE testInsertPatient
AS
BEGIN
	DECLARE @id varchar(30) = 'Sep_021',
	@studyUID varchar(70) = '15273.13.4.1',
	@birthday varchar(10) = NULL,
	@age varchar(10) = NULL,
	@sex varchar(10) = 'FEMALE',
	@series int = NULL

	EXEC tSQLt.FakeTable 'patient';
	EXEC [dbo].[spr_InsertPatient_v001] @id,@studyUID,@birthday,@age,@sex,@series;

	DECLARE @actualID varchar(30), @uid varchar(70), @birthdate varchar(10), @pAge varchar(10), @gender char;
	SET @actualID = (SELECT patientID FROM patient);
	EXEC tSQLt.AssertEquals @id,@actualID;
	SET @uid = (SELECT studyUID FROM patient);
	EXEC tSQLt.AssertEquals @studyUID,@uid;
	SET @birthdate = (SELECT birthdate FROM patient);
	EXEC tSQLt.AssertEquals @birthday,@birthdate;
	SET @pAge = (SELECT age FROM patient);
	EXEC tSQLt.AssertEquals @age,@pAge;
	SET @gender = (SELECT sex FROM patient);
	EXEC tSQLt.AssertEquals 'F',@gender;
END;
GO