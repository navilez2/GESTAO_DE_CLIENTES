USE GESTAO_CLIENTE
GO

IF OBJECTPROPERTY(object_id('dbo.SPD_CLIENTE'), N'IsProcedure') = 1
BEGIN
	DROP PROCEDURE dbo.SPD_CLIENTE
	PRINT '** A PROC SPD_CLIENTE FOI APAGADA COM SUCESSO **'
END
GO
CREATE PROCEDURE dbo.SPD_CLIENTE
(
	@CD_CLIENTE INT
)
AS
BEGIN
	SET NOCOUNT ON
----------------------

	DELETE TB_CLIENTE WHERE CD_CLIENTE = @CD_CLIENTE

----------------------
END
GO
IF OBJECTPROPERTY(object_id('dbo.SPD_CLIENTE'), N'IsProcedure') = 1
BEGIN
	PRINT '** A PROC SPD_CLIENTE FOI CRIADA COM SUCESSO **'
END
GO