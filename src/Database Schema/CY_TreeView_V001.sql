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
-- Create date: <23/11/2013>
-- Description:	<Retrieve id, image number to produce treeview>
-- =============================================
CREATE PROCEDURE spr_TreeView_v001
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select p.patientID, i.fileID
	from patient p join imageVariables iv on p.id = iv.id 
	join images i on iv.imageID = i.imageID 
	group by p.patientID,i.fileID
	order by 1, 2;
END
GO