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
-- Create date: <28/01/2013>
-- Description:	<Retrieve patients with custom requirement to produce treeview>
-- =============================================
ALTER PROCEDURE spr_CustomList_v001
	-- Add the parameters for the stored procedure here
	@Sex varchar(1) = NULL,
	@IDContains varchar(20) = NULL,
	@Batch int = NULL,
	@modality varchar(15) = NULL,
	@AcquireBetweenFrom date = NULL,
	@AcquireBetweenTo date = NULL,
	@OrderBy varchar(25) = NULL
	
AS
BEGIN
	
	DECLARE @BatchTime datetime = cast('1753-1-1' as datetime)

	SET NOCOUNT ON;
	IF @Batch IS NOT NULL
	BEGIN
		DECLARE @BatchAvailable int
		SET @BatchAvailable = (select count(*) from timeLog)
		IF @BatchAvailable < @Batch
			SET @Batch = @BatchAvailable
		ELSE IF @Batch > 0 
			SET @BatchTime = (select beginTime from (select ROW_NUMBER() over(order by logID desc) as 'Row', * from timeLog) sorty where Row = @Batch)
	END

	IF @AcquireBetweenFrom IS NULL
		SET @AcquireBetweenFrom = DATEADD(YEAR,-1000,CAST(current_timestamp as DATE))

	IF @AcquireBetweenTo IS NULL
		SET @AcquireBetweenTo = DATEADD(YEAR,1000,CAST(current_timestamp as DATE))

	IF @OrderBy IS NULL
		SET @OrderBy = 'Modified DESC'

	SELECT P.patientID as 'Patient ID', N.patientName as 'Patient Name', 
		IP.seriesDescription as 'Series', I.fileID as 'File ID'
	FROM patient P join name N on P.tableID = N.patientID join
		imageProperties IP on P.tableID = IP.patientID join 
		images I on IP.seriesID = I.seriesID
	WHERE (sex = @Sex OR @Sex IS NULL)
		and (@IDContains IS NULL OR P.patientID like CONCAT('%',@IDContains,'%'))
		and (modality = @modality OR @modality IS NULL)
		and imageAcquisitionDate between @AcquireBetweenFrom and @AcquireBetweenTo
		and lastModifiedDate between @BatchTime and current_timestamp
	ORDER BY 
		CASE WHEN @OrderBy = 'PatientID ASC' THEN P.patientID END,
		CASE WHEN @OrderBy = 'PatientID DESC' THEN P.patientID END DESC,
		CASE WHEN @OrderBy = 'Modified ASC' THEN IP.lastModifiedDate END,
		CASE WHEN @OrderBy = 'Modified DESC' THEN IP.lastModifiedDate END DESC,
		CASE WHEN @OrderBy = 'Series ASC' THEN P.seriesAvailable END,
		CASE WHEN @OrderBy = 'Series DESC' THEN P.seriesAvailable END DESC

END
GO
