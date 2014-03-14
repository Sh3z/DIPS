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
-- Description:	<Retrieve patients with custom requirement to produce treeview (Patient ID/Name and Series Description)>
-- =============================================
ALTER PROCEDURE spr_CustomList_v001
	-- Add the parameters for the stored procedure here
	@Sex varchar(1) = NULL,
	@IDContains varchar(20) = NULL,
	@Batch int = NULL,
	@modality varchar(15) = NULL,
	@AcquireBetweenFrom date = NULL,
	@AcquireBetweenTo date = NULL
	
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


	SELECT P.patientID as 'Patient ID', N.patientName as 'Patient Name', 
		N.patientID as 'Table ID', IP.seriesDescription as 'Series', IP.seriesID as 'Series ID'
	FROM patient P join name N on P.tableID = N.patientID join
		imageProperties IP on P.tableID = IP.patientID
	WHERE (sex = @Sex OR @Sex IS NULL)
		and (@IDContains IS NULL OR P.patientID like CONCAT('%',@IDContains,'%'))
		and (modality = @modality OR @modality IS NULL)
		and imageAcquisitionDate between @AcquireBetweenFrom and @AcquireBetweenTo
		and lastModifiedDate between @BatchTime and current_timestamp
	ORDER BY IP.lastModifiedDate DESC

END
GO
