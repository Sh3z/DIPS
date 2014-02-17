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
USE master
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Chuo Yeh Poo>
-- Create date: <12/02/2013>
-- Description:	<Restore database from backup>
-- =============================================
CREATE PROCEDURE spr_RestoreBackup_v001 
	-- Add the parameters for the stored procedure here
	@filePath varchar(150) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	IF @filePath IS NOT NULL
		RESTORE DATABASE medicalImaging
		FROM DISK = @filePath
END
GO
