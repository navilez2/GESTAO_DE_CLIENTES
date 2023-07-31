USE GESTAO_CLIENTE
GO

IF OBJECTPROPERTY(object_id('dbo.SPI_SITUACAO'), N'IsProcedure') = 1
BEGIN
	DROP PROCEDURE dbo.SPI_SITUACAO
	PRINT '** A PROC SPI_SITUACAO FOI APAGADA COM SUCESSO **'
END
GO
CREATE PROCEDURE dbo.SPI_SITUACAO
(
	@DC_SITUACAO VARCHAR(200)
)
AS
BEGIN
	SET NOCOUNT ON
----------------------


	IF EXISTS(SELECT TOP 1 * FROM TB_SITUACAO_CLIENTE WHERE DC_SITUACAO = @DC_SITUACAO)
	BEGIN
		RAISERROR ('A Situa��o informada ja est� cadastrada.', 16, 1)
		return
	END

	INSERT INTO TB_SITUACAO_CLIENTE 
				(
				  DC_SITUACAO,
				  DT_ALTERACAO
				)
		 VALUES (
				  @DC_SITUACAO,
				  GETDATE()
				)

----------------------
END
GO
IF OBJECTPROPERTY(object_id('dbo.SPI_SITUACAO'), N'IsProcedure') = 1
BEGIN
	PRINT '** A PROC SPI_SITUACAO FOI CRIADA COM SUCESSO **'
END
GO