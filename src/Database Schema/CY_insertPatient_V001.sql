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
-- Description:	<Insert Patient Information>
-- =============================================
CREATE PROCEDURE spr_InsertPatient_v001
	-- Add the parameters for the stored procedure here
	@id varchar(30),
	@birthday varchar(10),
	@age varchar(10),
	@sex char,
	@series int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF @birthday = ''
		SET @birthday = NULL
	IF @age = ''
		SET @age = NULL
	IF @sex = ''
		SET @sex = NULL

    -- Insert statements for procedure here
	INSERT INTO patient (patientID,birthdate,age,sex,seriesAvailable) OUTPUT INSERTED.tableID VALUES (@id,@birthday,@age,@sex,@series)

END
GO
