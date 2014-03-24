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
-- Create date: <26/02/2013>
-- Description:	<Insert Image File>
-- =============================================
CREATE PROCEDURE spr_InsertProcessedImages_v001
	-- Add the parameters for the stored procedure here
	@processMethod varchar(100) = NULL,
	@imageUID varchar(70) = NULL,
	@imageBlob varbinary(MAX) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @imageUID = ''
		SET @imageUID = NULL

	DECLARE @series int
	SET @series = (SELECT seriesID FROM images WHERE imageUID = @imageUID)

    -- Insert statements for procedure here
	INSERT INTO processedImages (processMethod, imageUID,imageBlob)
	OUTPUT @series 
	VALUES (@processMethod, @imageUID,@imageBlob)
END
GO
