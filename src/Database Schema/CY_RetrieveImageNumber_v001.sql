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
-- Create date: <22/01/2014>
-- Description:	<Retrieve Image Number by specific patient>
-- =============================================
CREATE PROCEDURE spr_RetrieveImageNumber_v001
	-- Add the parameters for the stored procedure here
	@databaseID int,
	@classID int,
	@number varchar(5)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select COUNT(i.imageNumber) as 'Image Number'
	from imageProperties ip inner join images i on ip.seriesID = i.seriesID
	where ip.patientID = @databaseID and i.seriesID = @classID
	and i.imageNumber = @number
END
GO
