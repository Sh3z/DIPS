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
-- Create date: <19/02/2013>
-- Description:	<Return List of patients sorted in particular order>
-- =============================================
CREATE PROCEDURE spr_SortTreeView_v001
	-- Add the parameters for the stored procedure here
	@OrderBy varchar(20) = NULL,
	@OrderDirection varchar(5) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @OrderBy IS NULL
		SET @OrderBy = 'ModifiedDate'

	IF @OrderDirection IS NULL
		SET @OrderDirection = 'DESC'

    -- Insert statements for procedure here

	SELECT distinct P.patientID
	FROM patient P
	order by P.patientID desc

	SELECT distinct P.patientID,P.seriesAvailable
	FROM patient P
	order by P.seriesAvailable desc

	SELECT P.patientID
	FROM patient P join imageProperties IP on P.tableID = IP.patientID
	group by P.patientID
	order by avg(CONVERT(FLOAT,IP.sliceThickness)) desc

	SELECT IP.patientID
	FROM patient P join imageProperties IP on P.tableID = IP.patientID
	group by IP.patientID, P.seriesAvailable 
	ORDER BY CASE WHEN @OrderBy = 'PatientID' AND @OrderDirection = 'ASC' THEN P.patientID END,
			 CASE WHEN @OrderBy = 'PatientID' THEN P.patientID END DESC,
			 CASE WHEN @OrderBy = 'ModifiedDate' AND @OrderDirection = 'ASC' THEN IP.lastModifiedDate END,
			 CASE WHEN @OrderBy = 'ModifiedDate' THEN IP.lastModifiedDate END DESC,
			 CASE WHEN @OrderBy = 'SliceThickness' AND @OrderDirection = 'ASC' THEN AVG(CONVERT(FLOAT,IP.sliceThickness)) END,
			 CASE WHEN @OrderBy = 'SliceThickness' THEN AVG(CONVERT(FLOAT,IP.sliceThickness)) END DESC
END
GO
