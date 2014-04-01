USE [medicalImaging]
GO

EXEC tSQLt.Run 'dbo.testCheckPatientExist';
EXEC tSQLt.Run 'dbo.testCheckSeriesUID';
EXEC tSQLt.Run 'dbo.testCheckSOPUID';
EXEC tSQLt.Run 'dbo.testCheckStudyUID';
EXEC tSQLt.Run 'dbo.testCreateLog';
EXEC tSQLt.Run 'dbo.testCustomList';
EXEC tSQLt.Run 'dbo.testDeleteExcessLog';
EXEC tSQLt.Run 'dbo.testInsertImageProperties';
EXEC tSQLt.Run 'dbo.testInsertImage';
EXEC tSQLt.Run 'dbo.testInsertName';
EXEC tSQLt.Run 'dbo.testInsertPatient';
EXEC tSQLt.Run 'dbo.testInsertProcessedImages';
EXEC tSQLt.Run 'dbo.testInsertTechnique';
EXEC tSQLt.Run 'dbo.testRetreiveTechniqueUsed';
EXEC tSQLt.Run 'dbo.testRetrieveAllTechnique';
EXEC tSQLt.Run 'dbo.testRetrieveImage';
EXEC tSQLt.Run 'dbo.testRetrieveImageNumber';
EXEC tSQLt.Run 'dbo.testRetrieveNextPatientID';
EXEC tSQLt.Run 'dbo.testRetrieveProcessedImage';
EXEC tSQLt.Run 'dbo.testRetrieveSelectedTechnique';
EXEC tSQLt.Run 'dbo.testSelectProperties';
EXEC tSQLt.Run 'dbo.testSubTreeView';
EXEC tSQLt.Run 'dbo.testTreeView';
EXEC tSQLt.Run 'dbo.testUpdateModifiedDate';
EXEC tSQLt.Run 'dbo.testUpdateSeriesAvailable';
EXEC tSQLt.Run 'dbo.testUpdateXmlTechnique';