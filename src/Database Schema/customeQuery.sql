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
	@Sex varchar(3) = NULL,
	@SexNotEquals varchar(3) = NULL,
	@IDcontains varchar(20) = NULL,
	@IDEquals varchar(15) = NULL,
	@AcquireDays int = NULL,
	@AcquireWeeks int = NULL,
	@AcquireMonths int = NULL,
	@AcquireYears int = NULL,
	@AcquireBetweenFrom date = NULL,
	@AcquireBetweenTo date = NULL
AS
BEGIN

	DECLARE @AcquireWithinDays date = DATEADD(YEAR,-1000,CAST(current_timestamp as DATE))
	DECLARE @AcquireWithinWeeks date = DATEADD(YEAR,-1000,CAST(current_timestamp as DATE))
	DECLARE @AcquireWithinMonths date = DATEADD(YEAR,-1000,CAST(current_timestamp as DATE))
	DECLARE @AcquireWithinYears date = DATEADD(YEAR,-1000,CAST(current_timestamp as DATE))

	SET NOCOUNT ON;
	IF @AcquireDays IS NOT NULL
		SET @AcquireWithinDays = DATEADD(DAY,-@AcquireDays,CAST(current_timestamp as DATE))

	IF @AcquireWeeks IS NOT NULL
		SET @AcquireWithinWeeks = DATEADD(WEEK,-@AcquireWeeks,CAST(current_timestamp as DATE))

	IF @AcquireMonths IS NOT NULL
		SET @AcquireWithinMonths = DATEADD(MONTH,-@AcquireMonths,CAST(current_timestamp as DATE))

	IF @AcquireYears IS NOT NULL
		SET @AcquireWithinYears = DATEADD(YEAR,-@AcquireYears,CAST(current_timestamp as DATE))

	IF @AcquireBetweenFrom IS NULL
		SET @AcquireBetweenFrom = DATEADD(YEAR,-1000,CAST(current_timestamp as DATE))

	IF @AcquireBetweenTo IS NULL
		SET @AcquireBetweenTo = DATEADD(YEAR,1000,CAST(current_timestamp as DATE))


	SELECT p.patientID as 'Patient ID', i.fileID as 'File ID'
	FROM patient P inner join imageProperties IP on P.tableID = IP.patientID join images I on IP.seriesID = I.seriesID
	WHERE sex = ISNULL(@Sex, sex)
	and sex <> ISNULL(@SexNotEquals,'NA')
	and P.patientID = ISNULL(@IDEquals,P.patientID)
	and (@IDcontains IS NULL OR P.patientID like CONCAT('%',@IDcontains,'%'))
	and CAST(imageAcquisitionDate as DATE) >= @AcquireWithinDays
	and CAST(imageAcquisitionDate as DATE) >= @AcquireWithinWeeks
	and CAST(imageAcquisitionDate as DATE) >= @AcquireWithinMonths
	and CAST(imageAcquisitionDate as DATE) >= @AcquireWithinYears
	and imageAcquisitionDate between @AcquireBetweenFrom and @AcquireBetweenTo

END
GO
