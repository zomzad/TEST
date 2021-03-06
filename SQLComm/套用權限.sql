DECLARE @SYS_ID VARCHAR(6) = 'HCMAP';
DECLARE @ROLE_CONDITION_ID VARCHAR(20) = 'HCMTEST3'
DECLARE @UPD_USER_ID VARCHAR(50) = '更新者';

CREATE TABLE #USER (
    USER_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN
);

CREATE TABLE #USER_ROLE (
    USER_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN,
	ROLE_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN
);

CREATE TABLE #SYS_USER_FUN (
    USER_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN,
	SYS_ID VARCHAR(6) COLLATE Chinese_Taiwan_Stroke_BIN,
	FUN_CONTROLLER_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN,
	FUN_ACTION_NAME VARCHAR(50) COLLATE Chinese_Taiwan_Stroke_BIN,
	IS_ASSIGN CHAR(1) COLLATE Chinese_Taiwan_Stroke_BIN,
	UPD_USER_ID VARCHAR(50) COLLATE Chinese_Taiwan_Stroke_BIN,
	UPD_DT DATETIME
);

--建立符合條件的使用者清單
INSERT INTO #USER(
	USER_ID
)
SELECT O.USER_ID
  FROM RAW_CM_USER U
  JOIN RAW_CM_USER_ORG O
	ON O.USER_ID = U.USER_ID
 WHERE U.IS_LEFT = 'N' 
   AND O.USER_BIZ_TITLE = N'O001' OR U.USER_UNIT_ID = N'T1'

--取得符合條件者的新增角色清單
INSERT INTO #USER_ROLE (
	USER_ID,
	ROLE_ID
)
SELECT S.USER_ID
     , R.ROLE_ID
  FROM SYS_SYSTEM_ROLE_CONDITION_COLLECT R
  JOIN #USER S
    ON 1 = 1
 WHERE R.SYS_ID = @SYS_ID
   AND R.ROLE_CONDITION_ID = @ROLE_CONDITION_ID
EXCEPT
SELECT R.USER_ID ,R.ROLE_ID
  FROM SYS_USER_SYSTEM_ROLE R
  JOIN #USER S
    ON R.USER_ID = S.USER_ID
 WHERE R.SYS_ID = @SYS_ID

--新增使用者角色
--INSERT INTO SYS_USER_SYSTEM_ROLE(
--	USER_ID,
--	SYS_ID,
--	ROLE_ID,
--	UPD_USER_ID,
--	UPD_DT
--)
SELECT USER_ID
     , @SYS_ID
	 , ROLE_ID
	 , @UPD_USER_ID
	 , GETDATE()
  FROM #USER_ROLE

--新增使用者可用應用系統
--DELETE FROM dbo.SYS_USER_SYSTEM
-- WHERE SYS_ID = @SYS_ID
--    AND USER_ID IN (SELECT USER_ID FROM #USER)

--INSERT INTO dbo.SYS_USER_SYSTEM (
--       USER_ID
--     , SYS_ID
--     , UPD_USER_ID
--     , UPD_DT
--)
SELECT USER_ID
     , @SYS_ID
	 , @UPD_USER_ID
	 , GETDATE()
 FROM #USER

--新增使用者功能
INSERT INTO #SYS_USER_FUN (
       USER_ID
     , SYS_ID
     , FUN_CONTROLLER_ID
     , FUN_ACTION_NAME
     , IS_ASSIGN
     , UPD_USER_ID
     , UPD_DT
)
SELECT U.USER_ID 
     , F.SYS_ID
     , F.FUN_CONTROLLER_ID
     , F.FUN_ACTION_NAME 
     , 'N' AS IS_ASSIGN 
     , @UPD_USER_ID
     , GETDATE() 
 FROM #USER_ROLE U 
 JOIN SYS_SYSTEM_MAIN S
   ON S.SYS_ID = @SYS_ID
 JOIN SYS_SYSTEM_ROLE_FUN R
   ON R.SYS_ID = @SYS_ID
  AND U.ROLE_ID = R.ROLE_ID 
 JOIN SYS_SYSTEM_FUN F
   ON R.SYS_ID = F.SYS_ID
  AND R.FUN_CONTROLLER_ID = F.FUN_CONTROLLER_ID
  AND R.FUN_ACTION_NAME = F.FUN_ACTION_NAME 
 LEFT JOIN (
         SELECT N.SYS_ID, N.FUN_MENU, N.FUN_CONTROLLER_ID, N.FUN_ACTION_NAME, M.DEFAULT_MENU_ID 
           FROM SYS_SYSTEM_MENU_FUN N 
           JOIN SYS_SYSTEM_FUN_MENU M
             ON N.FUN_MENU_SYS_ID = M.SYS_ID
            AND N.FUN_MENU = M.FUN_MENU 
      ) Z
   ON F.SYS_ID = Z.SYS_ID
  AND F.FUN_CONTROLLER_ID = Z.FUN_CONTROLLER_ID
  AND F.FUN_ACTION_NAME = Z.FUN_ACTION_NAME 
 LEFT JOIN SYS_USER_FUN O
   ON U.USER_ID = O.USER_ID 
  AND F.SYS_ID = O.SYS_ID 
  AND F.FUN_CONTROLLER_ID = O.FUN_CONTROLLER_ID 
  AND F.FUN_ACTION_NAME = O.FUN_ACTION_NAME 
WHERE S.IS_DISABLE = 'N'
  AND F.IS_DISABLE = 'N'
  AND Z.FUN_MENU IS NOT NULL 
  AND O.IS_ASSIGN IS NULL 

--INSERT INTO dbo.SYS_USER_FUN (
--       USER_ID
--     , SYS_ID
--     , FUN_CONTROLLER_ID
--     , FUN_ACTION_NAME
--     , IS_ASSIGN
--     , UPD_USER_ID
--     , UPD_DT
--)
SELECT USER_ID 
     , SYS_ID
     , FUN_CONTROLLER_ID
     , FUN_ACTION_NAME 
     , 'N' AS IS_ASSIGN 
     , @UPD_USER_ID
     , GETDATE() 
 FROM #SYS_USER_FUN 

-- 新增使用者功能選單
--INSERT INTO dbo.SYS_USER_FUN_MENU (
--       USER_ID
--     , SYS_ID
--     , FUN_MENU
--     , MENU_ID
--     , SORT_ORDER
--     , UPD_USER_ID
--     , UPD_DT
--)
SELECT DISTINCT
       U.USER_ID
     , Z.FUN_MENU_SYS_ID
     , Z.FUN_MENU
     , Z.DEFAULT_MENU_ID
     , Z.SORT_ORDER
     , @UPD_USER_ID
     , GETDATE()
  FROM #SYS_USER_FUN U
  JOIN (
           SELECT N.SYS_ID
                , N.FUN_MENU_SYS_ID, N.FUN_MENU
                , N.FUN_CONTROLLER_ID, N.FUN_ACTION_NAME
                , M.DEFAULT_MENU_ID, M.SORT_ORDER
           FROM SYS_SYSTEM_MENU_FUN N
           JOIN SYS_SYSTEM_FUN_MENU M
             ON N.FUN_MENU_SYS_ID = M.SYS_ID
            AND N.FUN_MENU = M.FUN_MENU
       ) Z
    ON U.SYS_ID = Z.SYS_ID
   AND U.FUN_CONTROLLER_ID = Z.FUN_CONTROLLER_ID
   AND U.FUN_ACTION_NAME=Z.FUN_ACTION_NAME
  LEFT JOIN SYS_USER_FUN_MENU M
    ON U.USER_ID = M.USER_ID
   AND Z.FUN_MENU_SYS_ID = M.SYS_ID
   AND Z.FUN_MENU = M.FUN_MENU
 WHERE M.USER_ID IS NULL
   AND M.SYS_ID IS NULL
   AND M.FUN_MENU IS NULL;


SELECT R.USER_ID
     , U.USER_NM
     , SM.SYS_ID
     , SM.SYS_NM_ZH_TW
	 , 'HCMTEST3'
	 , '' 
	 , R.ROLE_ID
	 , UR.ROLE_NM_ZH_TW
	 , '條件'
 FROM #USER_ROLE R
 JOIN RAW_CM_USER U
   ON U.USER_ID = R.USER_ID
 JOIN SYS_SYSTEM_ROLE UR
   ON UR.SYS_ID = 'HCMAP'
  AND UR.ROLE_ID = R.ROLE_ID
JOIN SYS_SYSTEM_MAIN SM
  ON SM.SYS_ID = 'HCMAP'

--清除暫存TABLE
DROP TABLE #USER;
DROP TABLE #USER_ROLE;
DROP TABLE #SYS_USER_FUN;
--///////////////////////////////////////////////////////////////////////////////////////