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
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE spr_CustomList_v001
	-- Add the parameters for the stored procedure here
	@Sex varchar(3),
	@SexNotEquals varchar(3),
	@IDcontains varchar(20),
	@IDStartsWith varchar(12),
	@IDEndsWith varchar(12),
	@IDEquals varchar(15),
	@IDNotEquals varchar(15),
	@AcquireDays int,
	@AcquireWeeks int,
	@AcquireMonths int,
	@AcquireYears int,
	@AcquireBetweenFrom date,
	@AcquireBetweenTo date
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


	SELECT * FROM patient P inner join imageProperties IP on P.tableID = IP.patientID join images I on IP.seriesID = I.seriesID
	WHERE sex = ISNULL(@Sex, sex)
	and sex <> ISNULL(@SexNotEquals,'NA')
	and P.patientID = ISNULL(@IDEquals,P.patientID)
	and P.patientID <> ISNULL(@IDNotEquals,'N/A')
	and P.patientID like CONCAT('%',ISNULL(@IDcontains,P.patientID),'%')
	and P.patientID like CONCAT(ISNULL(@IDStartsWith,P.patientID),'%')
	and P.patientID like CONCAT('%',ISNULL(@IDEndsWith,P.patientID))
	and CAST(imageAcquisitionDate as DATE) >= @AcquireWithinDays
	and CAST(imageAcquisitionDate as DATE) >= @AcquireWithinWeeks
	and CAST(imageAcquisitionDate as DATE) >= @AcquireWithinMonths
	and CAST(imageAcquisitionDate as DATE) >= @AcquireWithinYears
	and imageAcquisitionDate between @AcquireBetweenFrom and @AcquireBetweenTo
	
END
GO
