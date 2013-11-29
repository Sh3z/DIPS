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
-- Description:	<Retrieve id, body part, study and series description>
-- =============================================
CREATE PROCEDURE spr_SelectPatient_v001
	-- Add the parameters for the stored procedure here
	@birthdate varchar(10),
	@age varchar(10),
	@sex char(1),
	@fname varchar(30),
	@lname varchar(30)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select p.id, i.bodyPart, i.studyDescription, i.seriesDescription 
	from patient p inner join name n on p.id = n.id join imageVariables i on p.id = i.id 
	where ((p.birthdate = @birthdate and p.age = @age) and p.sex = @sex) and (n.firstName = @fname and n.lastName = @lname);
END
GO
