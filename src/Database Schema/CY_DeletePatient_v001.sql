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
-- Create date: <09/02/2014>
-- Description:	<Delete patient from the database>
-- =============================================
CREATE PROCEDURE spr_DeletePatient_v001
	-- Add the parameters for the stored procedure here
	@patientID varchar(30) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	delete from patient where patientID = @patientID
END
GO
