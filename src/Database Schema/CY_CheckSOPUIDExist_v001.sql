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
-- Create date: <18/03/2013>
-- Description:	<Check Image Exist By Matching DICOM SOP Instance UID>
-- =============================================
CREATE PROCEDURE spr_CheckSOPUIDExist_v001
	-- Add the parameters for the stored procedure here
	@sopUID varchar(70)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT COUNT(fileID) FROM images WHERE imageUID = @sopUID;
END
GO
