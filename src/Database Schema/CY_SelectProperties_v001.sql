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
-- Create date: <29/11/2013>
-- Description:	<Retrieve all properties of the patient>
-- =============================================
CREATE PROCEDURE spr_SelectProperties_v001
	-- Add the parameters for the stored procedure here
	@fileID varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select p.birthdate, p.age, p.sex, iv.imageDateTime, iv.bodyPart, iv.studyDescription, iv.seriesDescription, iv.sliceThickness 
	from patient p inner join imageProperties iv on p.id = iv.id join images i on iv.imageID = i.imageID
	where i.fileID = @fileID;
END
GO
