CREATE TABLE #TEMP_FINAL_DATE (
       tabl_type varchar(8) NOT NULL,
	   tabl_code varchar(8) NOT NULL,
	   COLUMN_NAME NVARCHAR(100) NULL,
	   ORG_DATA NVARCHAR(200) NULL,
	   FIX_DATA NVARCHAR(200) NULL	   
)

select COLUMN_NAME
     , RANK() OVER (ORDER BY COLUMN_NAME) AS ROW_ID
  INTO #TEST
  from INFORMATION_SCHEMA.COLUMNS  A WHERE A.TABLE_NAME = 'ZD223_SOURCE_istbm00'

SELECT *
  INTO #CHECK_DATA
  FROM ZD223_istbm00 EXCEPT SELECT * FROM ZD223_SOURCE_istbm00

DECLARE @SQLSTR AS NVARCHAR(4000);
DECLARE @COLUMN_NAME AS NVARCHAR(100)
DECLARE @MAX_COLUMN_COUNT AS INT;
DECLARE @NOW_COLUMN_COUNT AS INT;
    SET @NOW_COLUMN_COUNT = 1;
    SET @MAX_COLUMN_COUNT = (SELECT COUNT(1) FROM #TEST);

WHILE (@NOW_COLUMN_COUNT <= @MAX_COLUMN_COUNT)
BEGIN
     SET @COLUMN_NAME = NULL;

     SELECT @COLUMN_NAME = A.COLUMN_NAME
	   FROM #TEST A
	  WHERE A.ROW_ID = @NOW_COLUMN_COUNT

     SET @SQLSTR =  (SELECT 'INSERT INTO #TEMP_FINAL_DATE SELECT T.tabl_type AS tabl_type , T.tabl_code AS tabl_code , '+ (SELECT CHAR(39) +@COLUMN_NAME + CHAR(39)) + ' AS 欄位名稱 , I.' + @COLUMN_NAME + ' AS 原始資料 , T.'+@COLUMN_NAME +' AS 修改資料 FROM (SELECT * FROM ZD223_istbm00 EXCEPT SELECT * FROM ZD223_SOURCE_istbm00) T JOIN ZD223_SOURCE_istbm00 I ON T.tabl_type = I.tabl_type AND T.tabl_code = I.tabl_code WHERE I.' + @COLUMN_NAME + ' <> ' + 'T.'+ @COLUMN_NAME) + ' AND EXISTS ( SELECT 1 FROM #CHECK_DATA XX WHERE T.tabl_type = I.tabl_type AND T.tabl_code = I.tabl_code)'
	 
	 --PRINT @SQLSTR	 
	 EXEC sp_executesql @SQLSTR;
     SET @NOW_COLUMN_COUNT = @NOW_COLUMN_COUNT + 1;	 
END
;


--48s
--select *  from INFORMATION_SCHEMA.COLUMNS  A WHERE A.TABLE_NAME = 'ZD223_SOURCE_istbm00'
--SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS A WHERE A.TABLE_NAME = 'ZD223_SOURCE_istbm00'
--SELECT * FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'ZD223_SOURCE_istbm00'
SELECT T.tabl_type AS tabl_type
     , T.tabl_code AS tabl_code
     , 'tabl_desc' AS '欄位名稱'
     , I.tabl_desc AS '原始資料'
     , T.tabl_desc AS '修改資料'
  FROM (SELECT * FROM ZD223_istbm00 EXCEPT SELECT * FROM ZD223_SOURCE_istbm00) T
  LEFT JOIN ZD223_SOURCE_istbm00 I
    ON T.tabl_type = I.tabl_type
   AND T.tabl_code = I.tabl_code
--
SELECT tabl_stfn FROM ZD223_istbm00 A WHERE A.tabl_type = 'B2EAGENT' AND A.tabl_code = '13E00500'
SELECT tabl_stfn FROM ZD223_SOURCE_istbm00 A WHERE A.tabl_type = 'B2EAGENT' AND A.tabl_code = '13E00500'

SELECT DISTINCT * FROM #TEMP_FINAL_DATE
DROP TABLE #TEMP_FINAL_DATE
DROP TABLE #TEST
DROP TABLE #CHECK_DATA
SELECT *
  INTO #TEST
  FROM (
SELECT T.tabl_type AS tabl_type
     , T.tabl_code AS tabl_code
     , 'tabl_desc' AS '欄位名稱'
     , I.tabl_desc AS '原始資料'
     , T.tabl_desc AS '修改資料'
  FROM (SELECT * FROM ZD223_istbm00 EXCEPT SELECT * FROM ZD223_SOURCE_istbm00) T
  LEFT JOIN ZD223_SOURCE_istbm00 I
    ON T.tabl_type = I.tabl_type
   AND T.tabl_code = I.tabl_code 
 WHERE I.tabl_desc != T.tabl_desc
 UNION
SELECT T.tabl_type AS tabl_type
     , T.tabl_code AS tabl_code
     , 'tabl_stfn' AS '欄位名稱' 
     , I.tabl_stfn AS '原始tabl_dname'
     , T.tabl_stfn AS '修改資料'
  FROM (SELECT * FROM ZD223_istbm00 EXCEPT SELECT * FROM ZD223_SOURCE_istbm00) T
  LEFT JOIN ZD223_SOURCE_istbm00 I
    ON T.tabl_type = I.tabl_type
   AND T.tabl_code = I.tabl_code
 WHERE I.tabl_stfn != T.tabl_stfn
  UNION
SELECT T.tabl_type AS tabl_type
     , T.tabl_code AS tabl_code
     , 'tabl_ename' AS '欄位名稱' 
     , I.tabl_ename AS '原始tabl_dname'
     , T.tabl_ename AS '修改資料'
  FROM (SELECT * FROM ZD223_istbm00 EXCEPT SELECT * FROM ZD223_SOURCE_istbm00) T
  LEFT JOIN ZD223_SOURCE_istbm00 I
    ON T.tabl_type = I.tabl_type
   AND T.tabl_code = I.tabl_code
 WHERE I.tabl_ename != T.tabl_ename

) B

SELECT DISTINCT A.欄位名稱
 FROM #TEST A

