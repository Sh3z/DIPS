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
-- Create date: <09/02/2013>
-- Description:	<Retrieve the total count of patients with specific keyword>
-- =============================================
CREATE PROCEDURE spr_RetrieveNextPatientID_v001
	-- Add the parameters for the stored procedure here
	@keyword varchar(10) = 'N/A'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ISNULL(MAX(RIGHT(patientID,LEN(patientID)-CHARINDEX('_',patientID)) + 1),1) 
	from patient where patientID like CONCAT(@keyword,'%')
END
GO
