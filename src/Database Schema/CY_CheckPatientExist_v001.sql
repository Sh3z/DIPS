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
-- Create date: <03/11/2013>
-- Description:	<Retrieve ID of matched patient>
-- =============================================
CREATE PROCEDURE spr_CheckPatientExist_v001
	-- Add the parameters for the stored procedure here
	@birthdate varchar(10),
	@age varchar(10),
	@sex char(1),
	@pName varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ISNULL(p.tableID,'') as 'Patient ID', ISNULL(ip.modality,'') as 'Modality',
	ISNULL(ip.bodyPart,'') as 'Body Parts', ISNULL(ip.studyDescription,'') as 'Study Description',
	ISNULL(ip.seriesDescription,'') as 'Series Description', ISNULL(ip.seriesID,'') as 'Series ID'
	from patient p inner join name n on p.tableID = n.patientID join imageProperties ip on p.tableID = ip.patientID 
	where (p.birthdate IS NULL OR p.birthdate = @birthdate) 
	and (p.age IS NULL OR p.age = @age)
	and (p.sex IS NULL OR p.sex = @sex)
	and (n.patientName IS NULL OR n.patientName = @pName)
END
GO
