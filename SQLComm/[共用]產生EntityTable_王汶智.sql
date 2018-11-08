DECLARE @TABLE_NAME VARCHAR(MAX) = 'opagm20';
DECLARE @IS_ENUM BIT = 1;
DECLARE @DATA_TYPE_LIST TABLE(DATA_TYPE VARCHAR(MAX), CDATA_TYPE VARCHAR(MAX));
 INSERT @DATA_TYPE_LIST VALUES ('BIGINT','DBBigInt')
 INSERT @DATA_TYPE_LIST VALUES ('BIT','DBBit')
 INSERT @DATA_TYPE_LIST VALUES ('CHAR','DBChar')
 INSERT @DATA_TYPE_LIST VALUES ('DATE','DBDateTime')
 INSERT @DATA_TYPE_LIST VALUES ('DATETIME','DBDateTime')
 INSERT @DATA_TYPE_LIST VALUES ('DECIMAL','DBNumeric')
 INSERT @DATA_TYPE_LIST VALUES ('FLOAT','DBNumeric')
 INSERT @DATA_TYPE_LIST VALUES ('INT','DBInt')
 INSERT @DATA_TYPE_LIST VALUES ('NVARCHAR','DBNVarChar')
 INSERT @DATA_TYPE_LIST VALUES ('SMALLDATETIME','DBDateTime')
 INSERT @DATA_TYPE_LIST VALUES ('SMALLINT','DBSmallInt')
 INSERT @DATA_TYPE_LIST VALUES ('TINYINT','DBTinyInt')
 INSERT @DATA_TYPE_LIST VALUES ('VARBINARY','')
 INSERT @DATA_TYPE_LIST VALUES ('VARCHAR','DBVarChar')

    SELECT 'public enum ParaField'
	 UNION ALL
    SELECT '{'
	 UNION ALL
	SELECT t.COLUMN_NAME + ','
	  FROM INFORMATION_SCHEMA.COLUMNS t
	  JOIN @DATA_TYPE_LIST d
		ON UPPER(t.DATA_TYPE) = d.DATA_TYPE
	 WHERE t.TABLE_NAME = @TABLE_NAME
	 UNION ALL
    SELECT '}'
	 UNION ALL
	select ''
	 UNION ALL
	SELECT 'public ' + d.CDATA_TYPE + ' ' + t.COLUMN_NAME + ';'
	  FROM INFORMATION_SCHEMA.COLUMNS t
	  JOIN @DATA_TYPE_LIST d
		ON UPPER(t.DATA_TYPE) = d.DATA_TYPE
	 WHERE t.TABLE_NAME = @TABLE_NAME
