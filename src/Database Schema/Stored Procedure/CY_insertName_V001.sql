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
-- Create date: <01/11/2013>
-- Description:	<Insert Patient Name>
-- =============================================
CREATE PROCEDURE spr_InsertName_v001 
	-- Add the parameters for the stored procedure here
	@id int,
	@pName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @pName = ''
		SET @pName = NULL

    -- Insert statements for procedure here
	INSERT INTO name (patientID,patientName) VALUES (@id,@pName)
END
GO
