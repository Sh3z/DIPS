EXEC tSQLt.NewTestClass 'testInsertName';
GO

CREATE PROCEDURE testInsertName
AS
BEGIN
	DECLARE @id int = 99,
	@pName varchar(50) = 'RAfKRbJrD42FUwKLF5W9Hw==';

	EXEC tSQLt.FakeTable 'name';
	EXEC [dbo].[spr_InsertName_v001] @id,@pName;

	DECLARE @actualID int, @actualName varchar(50)
	SET @actualName = (SELECT patientName FROM name);
	EXEC tSQLt.AssertEquals @pName,@actualName;
	SET @actualID = (SELECT patientID FROM name);
	EXEC tSQLt.AssertEquals @id,@actualID;
END;
GO