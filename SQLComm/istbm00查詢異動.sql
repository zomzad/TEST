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
     , I.tabl_stfn AS '原始資料'
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
     , I.tabl_ename AS '原始資料'
     , T.tabl_ename AS '修改資料'
  FROM (SELECT * FROM ZD223_istbm00 EXCEPT SELECT * FROM ZD223_SOURCE_istbm00) T
  LEFT JOIN ZD223_SOURCE_istbm00 I
    ON T.tabl_type = I.tabl_type
   AND T.tabl_code = I.tabl_code
 WHERE I.tabl_ename != T.tabl_ename