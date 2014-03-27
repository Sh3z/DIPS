EXEC tSQLt.NewTestClass 'testInsertImageProperties';
GO

CREATE PROCEDURE testInsertImageProperties
AS
BEGIN
	DECLARE @id int = 9,
	@seriesUID varchar(70) = '12.45.14',
	@modality varchar(15) = 'MR',
	@bodyPart varchar(20) = 'Leg',
	@studyDesc varchar(50) = NULL,
	@seriesDesc varchar(50) = NULL,
	@sliceThick varchar(20) = '5.009';

	EXEC tSQLt.FakeTable 'imageProperties';
	EXEC [dbo].[spr_InsertImageProperties_v001] @id,@seriesUID,@modality,@bodyPart,@studyDesc,@seriesDesc,@sliceThick;

	DECLARE @actualUID varchar(70), @study varchar(20);
	SET @actualUID = (SELECT seriesUID FROM imageProperties);
	EXEC tSQLt.AssertEquals @seriesUID,@actualUID;
	SET @study = (SELECT studyDescription FROM imageProperties);
	EXEC tSQLt.AssertEquals NULL,@study;
END;
GO