-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Chuo Yeh Poo>
-- Create date: <01/11/2013>
-- Description:	<Insert Image Information>
-- =============================================
CREATE PROCEDURE spr_InsertImageProperties_v001
	-- Add the parameters for the stored procedure here
	@id int,
	@seriesUID varchar(70),
	@modality varchar(15),
	@imgDateTime datetime = NULL,
	@bodyPart varchar(20),
	@studyDesc varchar(50),
	@seriesDesc varchar(50),
	@sliceThick varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @seriesUID = ''
		SET @seriesUID = NULL
	IF @modality = ''
		SET @modality = NULL
	IF @bodyPart = ''
		SET @bodyPart = NULL
	IF @studyDesc = ''
		SET @studyDesc = NULL
	IF @seriesDesc = ''
		SET @seriesDesc = NULL
	IF @sliceThick = ''
		SET @sliceThick = NULL

    -- Insert statements for procedure here
	INSERT INTO imageProperties (patientID,seriesUID,modality,imageAcquisitionDate,bodyPart,studyDescription,seriesDescription,sliceThickness)
	OUTPUT INSERTED.seriesID 
	VALUES (@id,@seriesUID,@modality,@imgDateTime,@bodyPart,@studyDesc,@seriesDesc,@sliceThick)
END
GO
