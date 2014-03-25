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
-- Create date: <25/02/2014>
-- Description:	<Update Selected Image Processing Technique (name and xml)>
-- =============================================
CREATE PROCEDURE spr_UpdateXmlTechnique_v001
	-- Add the parameters for the stored procedure here
	@id int,
	@name varchar(100),
	@technique xml
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update imageProcessing set name = @name, technique = @technique where ID = @id; 
END
GO
