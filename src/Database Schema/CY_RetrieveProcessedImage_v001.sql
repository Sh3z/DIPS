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
-- Description:	<Retrieve processed image by File ID and Process Algorithm>
-- =============================================
CREATE PROCEDURE spr_RetrieveProcessedImage_v001
	-- Add the parameters for the stored procedure here
	@fileID int,
	@processMethod varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select PImg.imageBlob from processedImages Pimg
	JOIN images Img on Pimg.imageUID = Img.imageUID
	where Img.fileID = @fileID and PImg.processMethod = @processMethod
END
GO
