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
	@imageUID varchar(70)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT imgP.name AS 'Algorithm', imgP.technique AS 'XML', imgP.ID AS 'ID'
	FROM processedImages pImg join imageProcessing imgP on pImg.processMethod = imgP.ID
	WHERE imageUID = @imageUID
END
GO
