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
-- Create date: <13/03/2013>
-- Description:	<Retrieve All Process Technique For The Series>
-- =============================================
CREATE PROCEDURE spr_RetreiveTechniqueUsed_v001
	-- Add the parameters for the stored procedure here
	@seriesID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT(processMethod) AS 'Algorithm' 
	FROM processedImages PImg
	JOIN images Img ON PImg.imageUID = Img.imageUID
	WHERE Img.seriesID = @seriesID
END
GO
