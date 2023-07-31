USE GESTAO_CLIENTE
GO

IF OBJECTPROPERTY(object_id('dbo.SPS_CLIENTE'), N'IsProcedure') = 1
BEGIN
	DROP PROCEDURE dbo.SPS_CLIENTE
	PRINT '** A PROC SPS_CLIENTE FOI APAGADA COM SUCESSO **'
END
GO
CREATE PROCEDURE dbo.SPS_CLIENTE
(
	@CD_CLIENTE INT = NULL,
	@NM_CLIENTE VARCHAR(200) = NULL,
	@NR_CPF BIGINT = NULL,
	@DC_SEXO VARCHAR(3) = NULL,
	@CD_SITUACAO_CLIENTE INT = NULL
)
AS
BEGIN
	SET NOCOUNT ON
----------------------
	
	DECLARE @SQL VARCHAR(MAX)


	SET @SQL = 'SELECT A.CD_CLIENTE,
					   A.NM_CLIENTE,
					   A.NR_CPF,
					   A.DC_SEXO,
					   A.DT_NASCIMENTO,
					   B.CD_SITUACAO_CLIENTE,
					   B.DC_SITUACAO,
					   A.DT_ALTERACAO 
	              FROM TB_CLIENTE A
				  JOIN TB_SITUACAO_CLIENTE B
				    ON A.CD_SITUACAO_CLIENTE = B.CD_SITUACAO_CLIENTE
				 WHERE 1=1
				 '


	--ADICIONA O FILTRO SE A VARIAVEL NAO ESTIVER NULA
	IF (@CD_CLIENTE IS NOT NULL)
	BEGIN
		SET @SQL = @SQL + 'AND CD_CLIENTE = '+CONVERT(VARCHAR(40),@CD_CLIENTE)+' '
	END

	IF (@NM_CLIENTE IS NOT NULL)
	BEGIN
		SET @SQL = @SQL + 'AND NM_CLIENTE LIKE (''%'+@NM_CLIENTE+'%'') '
	END

	IF (@NR_CPF IS NOT NULL)
	BEGIN
		SET @SQL = @SQL + 'AND NR_CPF = '+@NR_CPF+' '
	END

	IF (@DC_SEXO IS NOT NULL)
	BEGIN
		SET @SQL = @SQL + 'AND DC_SEXO = '+@DC_SEXO+' '
	END


	IF (@CD_SITUACAO_CLIENTE IS NOT NULL)
	BEGIN
		SET @SQL = @SQL + 'AND B.CD_SITUACAO_CLIENTE = '+CONVERT(VARCHAR(40),@CD_SITUACAO_CLIENTE)+' '
	END

	--EXECUTA A QUERY CRIADA DINAMCIAMENTE
	EXEC (@SQL)
----------------------
END
GO
IF OBJECTPROPERTY(object_id('dbo.SPS_CLIENTE'), N'IsProcedure') = 1
BEGIN
	PRINT '** A PROC SPS_CLIENTE FOI CRIADA COM SUCESSO **'
END
GO