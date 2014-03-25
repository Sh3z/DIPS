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
-- Create date: <18/02/2014>
-- Description:	<Auto delete time log if exceed 20 rows>
-- =============================================
CREATE PROCEDURE spr_DeleteExcessLog_v001
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @rowCount int
	SET @rowCount = (SELECT COUNT(logID) from timeLog)

    -- Insert statements for procedure here
	IF @rowCount > 20
		DELETE TOP(@rowCount-20) FROM timeLog
END
GO
