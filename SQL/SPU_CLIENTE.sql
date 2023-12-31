USE GESTAO_CLIENTE
GO

IF OBJECTPROPERTY(object_id('dbo.SPU_CLIENTE'), N'IsProcedure') = 1
BEGIN
	DROP PROCEDURE dbo.SPU_CLIENTE
	PRINT '** A PROC SPU_CLIENTE FOI APAGADA COM SUCESSO **'
END
GO
CREATE PROCEDURE dbo.SPU_CLIENTE
(
	@CD_CLIENTE INT,
	@NM_CLIENTE VARCHAR(200),
	@NR_CPF BIGINT,
	@DC_SEXO VARCHAR(3),
	@DT_NASCIMENTO DATE,
	@CD_SITUACAO_CLIENTE INT
)
AS
BEGIN
	SET NOCOUNT ON
----------------------

	IF EXISTS(SELECT TOP 1 * FROM TB_CLIENTE WHERE NR_CPF = @NR_CPF AND CD_CLIENTE <> @CD_CLIENTE)
	BEGIN
		RAISERROR ('O CPF informado ja est� cadastrado.', 16, 1)
		return
	END

	UPDATE TB_CLIENTE SET NM_CLIENTE = @NM_CLIENTE,
						  NR_CPF = @NR_CPF,
						  DC_SEXO = @DC_SEXO,
						  DT_NASCIMENTO = @DT_NASCIMENTO,
						  CD_SITUACAO_CLIENTE = @CD_SITUACAO_CLIENTE,
						  DT_ALTERACAO = GETDATE()

	 WHERE CD_CLIENTE = @CD_CLIENTE

----------------------
END
GO
IF OBJECTPROPERTY(object_id('dbo.SPU_CLIENTE'), N'IsProcedure') = 1
BEGIN
	PRINT '** A PROC SPU_CLIENTE FOI CRIADA COM SUCESSO **'
END
GO