USE GESTAO_CLIENTE
GO

IF OBJECTPROPERTY(object_id('dbo.SPU_SITUACAO'), N'IsProcedure') = 1
BEGIN
	DROP PROCEDURE dbo.SPU_SITUACAO
	PRINT '** A PROC SPU_SITUACAO FOI APAGADA COM SUCESSO **'
END
GO
CREATE PROCEDURE dbo.SPU_SITUACAO
(	
	@CD_SITUACAO_CLIENTE INT,
	@DC_SITUACAO VARCHAR(200)
)
AS
BEGIN
	SET NOCOUNT ON
----------------------

	IF EXISTS(SELECT TOP 1 * FROM TB_SITUACAO_CLIENTE WHERE DC_SITUACAO = @DC_SITUACAO AND CD_SITUACAO_CLIENTE <> @CD_SITUACAO_CLIENTE)
	BEGIN
		RAISERROR ('A Situa��o informada ja est� cadastrada.', 16, 1)
		return
	END

	UPDATE TB_SITUACAO_CLIENTE SET DC_SITUACAO = @DC_SITUACAO,
								   DT_ALTERACAO = GETDATE()

	 WHERE CD_SITUACAO_CLIENTE = @CD_SITUACAO_CLIENTE

----------------------
END
GO
IF OBJECTPROPERTY(object_id('dbo.SPU_SITUACAO'), N'IsProcedure') = 1
BEGIN
	PRINT '** A PROC SPU_SITUACAO FOI CRIADA COM SUCESSO **'
END
GO