/*-----------------------------------------------------------------------------
??:�@�Ө���i�H�P���ݩ�h�Ӹs��? ����

[ERPAPI]
1.�P�ˬO�w�]���⵹�ϥΪ�
Authorization/ERPUserAccountCreateEvent��Authorization/ERPUserRoleResetEvent���檺SP�n�ק�
SP�����Ⱖ,�e�@���]�w�n���|�Q�~��,�n�@���]�w��

[ERPAP]
1.Sys/SystemRoleGroupJoin(�[�J����s��)
--Ū���e�����--
SELECT CASE WHEN T.ROLE_GROUP_CONDITION_ID IS NULL THEN 'N' ELSE 'Y' END AS IS_THIS_GROUP
     , C.ROLE_GROUP_CONDITION_ID
     , ROLE_GROUP_CONDITION_NM_ZH_TW
  FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION C
  LEFT JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT T
    ON C.ROLE_GROUP_CONDITION_ID = T.ROLE_GROUP_CONDITION_ID
   AND T.SYS_ID = 'PUBAP'
   AND T.ROLE_ID = 'IT' 

--�s��--
(1)���R��#SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
   DELETE #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
    WHERE SYS_ID = 'PUBAP'
	  AND ROLE_ID = 'IT'
(2)�A�s��


2.���ʰO���sMongo
Sys/SystemRoleGroupJoin(�[�J����s��)
Sys/SystemRoleGroupCollect(����s�ղM��)
Sys/SystemRoleGroupDetail(����s���ɩ���)
�T�����n�O��

--Mongo��ƪ�W--
LOG_SYS_SYSTEM_ROLE_GROUP_CONDTION

--Mongo���--
RecordLogSystemRoleGroupCondotionPara = {
    RoleGroupConditionID,
    SysRoleList = new List<SysRoleInfo>
    {
	    new{
		       SysID = '',
			   RoleID = ''
		   }
    },
    RoleGroupConditionNMZHTW,
    RoleGroupConditionNMZHCN,
    RoleGroupConditionNMENUS,
    RoleGroupConditionNMTHTH,
    RoleGroupConditionNMJAJP,
    RoleGroupConditionSynTax,
    SortOrder,
    Remark,
    RoleGroupConditionRules (SystemRoleGroupJoin�MSystemRoleGroupDetail�줣��),
	ModifyType
}

�������ɭԥ��qmongo���̷s���X��,�����ʪ������A��s��

3.Sys/SystemRecord(�t�ά���),�W�[����s�լ������d��
-------------------------------------------------------------------------------*/

/*---���m���------------------------------------------------------------------
SELECT * FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION
SELECT * FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT

DELETE #SYS_SYSTEM_ROLE_GROUP_CONDITION
DELETE #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT

DROP TABLE #SYS_ROLE;
DROP TABLE #SYS_ROLE_RESULT;
DROP TABLE #SYS_SYSTEM_ROLE_GROUP_CONDITION;
DROP TABLE #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT;
-------------------------------------------------------------------------------*/

--����s����
CREATE TABLE #SYS_SYSTEM_ROLE_GROUP_CONDITION
(
   ROLE_GROUP_CONDITION_ID VARCHAR(20) PRIMARY KEY,
   ROLE_GROUP_CONDITION_NM_ZH_TW NVARCHAR(150) NOT NULL,
   ROLE_GROUP_CONDITION_NM_ZH_CN NVARCHAR(150) NOT NULL,
   ROLE_GROUP_CONDITION_NM_EN_US NVARCHAR(150) NOT NULL,
   ROLE_GROUP_CONDITION_NM_TH_TH NVARCHAR(150) NOT NULL,
   ROLE_GROUP_CONDITION_NM_JA_JP NVARCHAR(150) NOT NULL,
   ROLE_GROUP_CONDITION_SYNTAX NVARCHAR(1000),
   SORT_ORDER VARCHAR(6),
   REMARK NVARCHAR(600),
   UPD_USER_ID VARCHAR(50) NOT NULL,
   UPD_DT DATETIME NOT NULL
)

--����s���ɩ���
CREATE TABLE #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
(
   ROLE_GROUP_CONDITION_ID VARCHAR(20), --PRIMARY KEY,
   SYS_ID VARCHAR(12) NOT NULL,
   ROLE_ID VARCHAR(20) NOT NULL,
   REMARK NVARCHAR(600),
   UPD_USER_ID VARCHAR(50) NOT NULL,
   UPD_DT DATETIME NOT NULL
)

--�g�J���ո��
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION
VALUES('OPGroup','OP','OP','OP','OP','OP','O.USER_COM_ID = N''T'' AND O.USER_DEPT = N''D624''',NULL,'','System',GETDATE())
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION
VALUES('Program','�{���]�p�v','�{���]�p�v','�{���]�p�v','�{���]�p�v','�{���]�p�v','U.USER_UNIT_ID = N''87'' OR O.USER_COM_ID = N''TT'' OR (O.USER_BIZ_TITLE >= N''O039'' OR O.USER_DEPT = N''0078'' OR (O.USER_LEVEL <> N''010'' OR U.USER_WORK_ID < N''10''))',NULL,'','System',GETDATE())
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION
VALUES('SysAnalysis','�t�Τ��R�v','�t�Τ��R�v','�t�Τ��R�v','�t�Τ��R�v','�t�Τ��R�v','O.USER_BIZ_TITLE = N''S003'' AND O.USER_TEAM = N''S020'' AND(U.USER_UNIT_ID = N''36'')',NULL,'','System',GETDATE())

INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('OPGroup','ERPAP','OP',null,'System','2018-11-07 17:50:17.599')
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('OPGroup','ERPAP','SYS',null,'System','2018-11-07 17:50:17.599')
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('OPGroup','TOURAP','MRK',null,'System','2018-11-07 17:50:17.599')
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('OPGroup','TKTAP','RC',null,'System','2018-11-07 17:50:17.599')

INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('Program','PUBAP','IT',null,'System','2018-11-07 17:50:17.599')
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('Program','ERPAP','GRANTOR',null,'System','2018-11-07 17:50:17.599')

INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('SysAnalysis','TOURAP','BuTour',null,'System','2018-11-07 17:50:17.599')
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('SysAnalysis','TOURAP','ProdGit',null,'System','2018-11-07 17:50:17.599')
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('SysAnalysis','LOCAP','IT',null,'System','2018-11-07 17:50:17.599')
INSERT INTO #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
VALUES('SysAnalysis','ORDRAP','ProdGit',null,'System','2018-11-07 17:50:17.599')

--�w�s�{��
    DECLARE @UnUseSys TABLE (SYS_ID VARCHAR(12));

    INSERT INTO @UnUseSys VALUES
    ('AC2AP'),
    --('ACCAP'),
    ('ADSAP'),
    ('BA2AP'),
    ('BUSAP'),
    ('CBMAP'),
    --('CRMAP'),
    --('CRSAP'),
    ('DWHAP'),
    --('ERPAP'),
    ('ETKTAP'),
    ('FITAP'),
    ('FNAP'),
    ('GITAP'),
    --('GITPCM'),
    --('HCMAP'),
    --('HTLAP'),
    ('HTLMRP'),
    ('LOCAP'),
    ('MELAP'),
    --('MKTAP'),
    ('MTMRP'),
    ('ORDRAP'),
    ('PDMAP'),
    ('POIAP'),
    --('PUBAP'),
    --('QAMAP'),
    --('RPMAP'),
    ('SCMAP'),
    ('TGUAP'),
    --('TKTAP'),
    ('TOURAP'),
    ('WMP2AP');

DECLARE @USER_ID VARCHAR(20) = '00ZZZZ'; 
DECLARE @UPD_USER_ID VARCHAR(20) = '00ZZZZ'; 

DECLARE @IS_LEFT CHAR(1);
DECLARE @SqlExecuteString NVARCHAR(MAX);

SELECT @IS_LEFT = IS_LEFT FROM RAW_CM_USER WHERE USER_ID = @USER_ID;

    ;WITH main AS (
        SELECT
            'IF EXISTS(SELECT *
                         FROM RAW_CM_USER U' +
						 CASE WHEN SSRC.ROLE_CONDITION_SYNTAX LIKE '%C.%'
						      THEN ' JOIN RAW_CM_ORG_COM C ON U.USER_COM_ID = C.COM_ID ' ELSE '' END + 
						 CASE WHEN SSRC.ROLE_CONDITION_SYNTAX LIKE '%O.%'  
							  THEN ' JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID ' ELSE '' END + 
						 CASE WHEN SSRC.ROLE_CONDITION_SYNTAX LIKE '%S.%'  
							  THEN ' LEFT JOIN RAW_CM_ORG_COM S ON S.IS_SALARY_COM = ''Y'' AND S.COM_ID = U.USER_SALARY_COM_ID ' ELSE '' END +
                      + ' WHERE U.USER_ID = '''+ @USER_ID +''''
            + CASE WHEN ROLE_CONDITION_SYNTAX > ''
	               THEN ' AND (' + ROLE_CONDITION_SYNTAX + ')'
                   ELSE '' END +
            ') BEGIN '
            +
            STUFF((
                SELECT 'INSERT #SYS_ROLE VALUES('''
                     + M.SYS_ID + ''',''' 
                     + SM.SYS_NM_ZH_TW + ''',''' 
                     + M.ROLE_CONDITION_ID + ''',''' 
                     + M.ROLE_CONDITION_NM_ZH_TW + ''',''' 
                     + D.ROLE_ID + ''','''
                     + R.ROLE_NM_ZH_TW + ''',NULL);'
                  FROM SYS_SYSTEM_ROLE_CONDITION M
                  JOIN SYS_SYSTEM_ROLE_CONDITION_COLLECT D
                    ON M.SYS_ID = D.SYS_ID
                   AND M.ROLE_CONDITION_ID = D.ROLE_CONDITION_ID
                  JOIN SYS_SYSTEM_MAIN SM
                    ON SM.SYS_ID = M.SYS_ID
                  JOIN SYS_SYSTEM_ROLE R
                    ON M.SYS_ID = R.SYS_ID
                   AND D.ROLE_ID = R.ROLE_ID
                 WHERE M.ROLE_CONDITION_ID = SSRC.ROLE_CONDITION_ID
                   AND M.SYS_ID = SSRC.SYS_ID
                FOR XML PATH(''),TYPE
            ).value('(./text())[1]','VARCHAR(MAX)'), 1, 0, '')
            +
            'END;'
          AS SqlSyntax
          FROM SYS_SYSTEM_ROLE_CONDITION SSRC
          JOIN SYS_SYSTEM_MAIN SM
            ON SM.SYS_ID = SSRC.SYS_ID
         WHERE SSRC.ROLE_CONDITION_SYNTAX > ''
           AND SSRC.SYS_ID NOT IN (SELECT SYS_ID FROM @UnUseSys)
    )
SELECT @SqlExecuteString = STUFF((SELECT SqlSyntax + '' FROM main FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)'),1,0,'');

--Group(�s�W)
;WITH groupmain AS(
        SELECT
            'IF EXISTS(SELECT *
                         FROM RAW_CM_USER U' +
						 CASE WHEN C.ROLE_GROUP_CONDITION_SYNTAX LIKE '%C.%'
						      THEN ' JOIN RAW_CM_ORG_COM C ON U.USER_COM_ID = C.COM_ID ' ELSE '' END + 
						 CASE WHEN C.ROLE_GROUP_CONDITION_SYNTAX LIKE '%O.%'  
							  THEN ' JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID ' ELSE '' END + 
						 CASE WHEN C.ROLE_GROUP_CONDITION_SYNTAX LIKE '%S.%'  
							  THEN ' LEFT JOIN RAW_CM_ORG_COM S ON S.IS_SALARY_COM = ''Y'' AND S.COM_ID = U.USER_SALARY_COM_ID ' ELSE '' END +
                      + ' WHERE U.USER_ID = '''+ @USER_ID +''''
            + CASE WHEN ROLE_GROUP_CONDITION_SYNTAX > ''
	               THEN ' AND (' + ROLE_GROUP_CONDITION_SYNTAX + ')'
                   ELSE '' END +
            ') BEGIN '
            +
            STUFF((
                SELECT 'INSERT #SYS_ROLE VALUES('''
                     + D.SYS_ID + ''',''' 
                     + SM.SYS_NM_ZH_TW + ''',''' 
                     + M.ROLE_GROUP_CONDITION_ID + ''',''' 
                     + M.ROLE_GROUP_CONDITION_NM_ZH_TW + ''',''' 
                     + D.ROLE_ID + ''','''
                     + R.ROLE_NM_ZH_TW + ''',NULL);'
                  FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION M
                  JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT	D
                    ON D.ROLE_GROUP_CONDITION_ID = M.ROLE_GROUP_CONDITION_ID
				   AND D.SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN NOT IN (SELECT SYS_ID FROM @UnUseSys) --�����\���t�δN��insert
                  JOIN SYS_SYSTEM_MAIN SM
                    ON SM.SYS_ID = D.SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN
                  JOIN SYS_SYSTEM_ROLE R
                    ON SM.SYS_ID = R.SYS_ID
                   AND D.ROLE_ID COLLATE Chinese_Taiwan_Stroke_BIN = R.ROLE_ID
                 WHERE M.ROLE_GROUP_CONDITION_ID = C.ROLE_GROUP_CONDITION_ID
                FOR XML PATH(''),TYPE
            ).value('(./text())[1]','VARCHAR(MAX)'), 1, 0, '')
            +
            'END;'
          AS SqlSyntax
      FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION C
     WHERE C.ROLE_GROUP_CONDITION_SYNTAX COLLATE Chinese_Taiwan_Stroke_BIN > ''
)
--(�s�W)
SELECT @SqlExecuteString = @SqlExecuteString + STUFF((SELECT SqlSyntax + '' FROM groupmain FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)'),1,0,'');
SELECT @SqlExecuteString

--BEGIN TRANSACTION
--    BEGIN TRY
            CREATE TABLE #SYS_ROLE (
                SYS_ID VARCHAR(12) COLLATE Chinese_Taiwan_Stroke_BIN,
                SYS_NM NVARCHAR(150) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_CONDITION_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_CONDITION_NM NVARCHAR(150) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_NM NVARCHAR(150) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_CONDITION_SYNTAX NVARCHAR(1000) COLLATE Chinese_Taiwan_Stroke_BIN
            );
            CREATE TABLE #SYS_ROLE_RESULT (
                SYS_ID VARCHAR(12) COLLATE Chinese_Taiwan_Stroke_BIN,
                SYS_NM NVARCHAR(150) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_CONDITION_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_CONDITION_NM NVARCHAR(150) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_ID VARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_NM NVARCHAR(150) COLLATE Chinese_Taiwan_Stroke_BIN,
                ROLE_CONDITION_SYNTAX NVARCHAR(1000) COLLATE Chinese_Taiwan_Stroke_BIN
            );
            
            EXECUTE (@SqlExecuteString);

			--SELECT * 
   --           FROM #SYS_ROLE SR
   --           JOIN SYS_SYSTEM_ROLE_CONDITION SSRC
   --             ON SR.SYS_ID = SSRC.SYS_ID
   --            AND SR.ROLE_CONDITION_ID = SSRC.ROLE_CONDITION_ID
			--  LEFT JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION C
			--    ON SR.ROLE_CONDITION_ID = C.ROLE_GROUP_CONDITION_ID COLLATE Chinese_Taiwan_Stroke_BIN
			--	SELECT * FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION

			--SQL�y�k�^�gRoleCondition
			UPDATE #SYS_ROLE
               SET ROLE_CONDITION_SYNTAX = SSRC.ROLE_CONDITION_SYNTAX
              FROM #SYS_ROLE SR
              JOIN SYS_SYSTEM_ROLE_CONDITION SSRC
                ON SR.SYS_ID = SSRC.SYS_ID
               AND SR.ROLE_CONDITION_ID = SSRC.ROLE_CONDITION_ID
			  JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION C
			    ON SR.ROLE_CONDITION_ID = C.ROLE_GROUP_CONDITION_ID COLLATE Chinese_Taiwan_Stroke_BIN

			-- SQL�y�k�^�gRoleGroupCondition(�s�W)
            UPDATE #SYS_ROLE
               SET ROLE_CONDITION_SYNTAX = C.ROLE_GROUP_CONDITION_SYNTAX
              FROM #SYS_ROLE SR
			  JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION C
			    ON SR.ROLE_CONDITION_ID = C.ROLE_GROUP_CONDITION_ID COLLATE Chinese_Taiwan_Stroke_BIN

-- ///////////////////�y�k�^�g���᳣�S��//////////////////////////////////////////////////////
            -- �R���ϥΪ̨���
            DELETE FROM SYS_USER_SYSTEM_ROLE
             WHERE USER_ID = @USER_ID
               AND SYS_ID NOT IN (SELECT SYS_ID FROM @UnUseSys);
            
            -- �R���ϥΪ̤����t��
            DELETE FROM SYS_USER_SYSTEM
             WHERE USER_ID = @USER_ID
               AND SYS_ID NOT IN (SELECT SYS_ID FROM SYS_SYSTEM_MAIN WHERE IS_OUTSOURCING = 'Y');
               
            -- ��s���u�D�ɪ��A
            UPDATE SYS_USER_MAIN
               SET ROLE_GROUP_ID = NULL
                 , IS_DISABLE = @IS_LEFT
                 , UPD_USER_ID = @UPD_USER_ID
                 , UPD_DT = GETDATE()
             WHERE USER_ID = @USER_ID;
            
            -- �g�JUSER����
            INSERT INTO #SYS_ROLE (SYS_ID, SYS_NM, ROLE_ID, ROLE_NM)
            SELECT SM.SYS_ID
                 , SM.SYS_NM_ZH_TW
                 , R.ROLE_ID
                 , R.ROLE_NM_ZH_TW
              FROM SYS_SYSTEM_ROLE R
              JOIN SYS_SYSTEM_MAIN SM
                ON R.SYS_ID = SM.SYS_ID
             WHERE R.ROLE_ID = 'USER'
               AND SM.SYS_ID NOT IN (SELECT UR.SYS_ID FROM dbo.SYS_USER_SYSTEM_ROLE UR WHERE UR.USER_ID = @USER_ID AND UR.ROLE_ID = 'USER');

            -- �s�W�ϥΪ̨���
            INSERT INTO dbo.SYS_USER_SYSTEM_ROLE (
                   USER_ID
                 , SYS_ID
                 , ROLE_ID
                 , UPD_USER_ID
                 , UPD_DT
            )
            SELECT DISTINCT @USER_ID
                 , SYS_ID
                 , ROLE_ID
                 , @UPD_USER_ID
                 , GETDATE()
              FROM #SYS_ROLE;

            INSERT INTO #SYS_ROLE_RESULT (SYS_ID, SYS_NM, ROLE_ID, ROLE_NM, ROLE_CONDITION_ID, ROLE_CONDITION_NM, ROLE_CONDITION_SYNTAX)
            SELECT SM.SYS_ID
                 , SM.SYS_NM_ZH_TW
                 , R.ROLE_ID
                 , R.ROLE_NM_ZH_TW
				 , ROLE_CONDITION_ID
                 , ROLE_CONDITION_NM
                 , ROLE_CONDITION_SYNTAX
              FROM SYS_USER_SYSTEM_ROLE UR
              JOIN SYS_SYSTEM_ROLE R
                ON R.SYS_ID = UR.SYS_ID
               AND R.ROLE_ID = UR.ROLE_ID
              JOIN SYS_SYSTEM_MAIN SM
                ON R.SYS_ID = SM.SYS_ID
              LEFT JOIN #SYS_ROLE SR
                ON R.SYS_ID = SR.SYS_ID
               AND R.ROLE_ID = SR.ROLE_ID
             WHERE UR.USER_ID = @USER_ID;
             
            TRUNCATE TABLE #SYS_ROLE;

            -- �s�W�ϥΪ̥i�ϥ����Ψt��
            INSERT INTO dbo.SYS_USER_SYSTEM (
                   USER_ID
                 , SYS_ID
                 , UPD_USER_ID
                 , UPD_DT
            )
            SELECT USER_ID
                 , SYS_ID
                 , @UPD_USER_ID
                 , GETDATE()
              FROM SYS_USER_SYSTEM_ROLE
             WHERE USER_ID = @USER_ID
             GROUP BY USER_ID, SYS_ID;
             
            -- �s�W�ϥΪ̥\��
            INSERT INTO dbo.SYS_USER_FUN (
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
              FROM SYS_USER_SYSTEM_ROLE U 
              JOIN SYS_SYSTEM_MAIN S
                ON U.SYS_ID = S.SYS_ID 
              JOIN SYS_SYSTEM_ROLE_FUN R
                ON U.SYS_ID = R.SYS_ID
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
             WHERE U.USER_ID = @USER_ID
               AND S.IS_DISABLE = 'N'
               AND F.IS_DISABLE = 'N'
               AND Z.FUN_MENU IS NOT NULL 
               AND O.IS_ASSIGN IS NULL 
             GROUP BY U.USER_ID, F.SYS_ID, F.FUN_CONTROLLER_ID, F.FUN_ACTION_NAME 
             ORDER BY F.SYS_ID, F.FUN_CONTROLLER_ID, F.FUN_ACTION_NAME;
             
            -- �s�W�ϥΪ̥\����
            INSERT INTO dbo.SYS_USER_FUN_MENU (
                   USER_ID
                 , SYS_ID
                 , FUN_MENU
                 , MENU_ID
                 , SORT_ORDER
                 , UPD_USER_ID
                 , UPD_DT
            )
            SELECT DISTINCT
                   U.USER_ID
                 , Z.FUN_MENU_SYS_ID
                 , Z.FUN_MENU
                 , Z.DEFAULT_MENU_ID
                 , Z.SORT_ORDER
                 , @UPD_USER_ID
                 , GETDATE()
              FROM SYS_USER_FUN U
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
             WHERE U.USER_ID = @USER_ID
               AND M.USER_ID IS NULL
               AND M.SYS_ID IS NULL
               AND M.FUN_MENU IS NULL;
               
            -- ����ϥΪ̥\����
            DELETE SYS_USER_FUN_MENU
              FROM SYS_USER_FUN_MENU U
              LEFT JOIN (
                       SELECT DISTINCT N.FUN_MENU_SYS_ID, N.FUN_MENU
                         FROM SYS_USER_FUN F
                         JOIN SYS_SYSTEM_MENU_FUN N
                           ON F.SYS_ID = N.SYS_ID
                          AND F.FUN_CONTROLLER_ID = N.FUN_CONTROLLER_ID
                          AND F.FUN_ACTION_NAME = N.FUN_ACTION_NAME
                        WHERE F.USER_ID = @USER_ID
                   ) M
                ON U.SYS_ID = M.FUN_MENU_SYS_ID
               AND U.FUN_MENU = M.FUN_MENU
             WHERE U.USER_ID = @USER_ID
               AND M.FUN_MENU_SYS_ID IS NULL
               AND M.FUN_MENU IS NULL;

            SELECT SYS_ID
                 , SYS_NM
                 , ROLE_CONDITION_ID
                 , ROLE_CONDITION_NM
                 , ROLE_ID
                 , ROLE_NM
                 , ROLE_CONDITION_SYNTAX
              FROM #SYS_ROLE_RESULT;

            IF OBJECT_ID('tempdb..#SYS_ROLE') IS NOT NULL
            BEGIN
                DROP TABLE #SYS_ROLE;
            END
            
            IF OBJECT_ID('tempdb..#SYS_ROLE_RESULT') IS NOT NULL
            BEGIN
                DROP TABLE #SYS_ROLE_RESULT;
            END

        --    COMMIT;
        --END TRY
        --BEGIN CATCH
        --    ROLLBACK TRANSACTION;

        --    DECLARE @ErrorMessage NVARCHAR(4000);  
        --    DECLARE @ErrorSeverity INT;  
        --    DECLARE @ErrorState INT;

        --    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        --    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        --END CATCH;




--////////////���դ���//////////////////////////////////////////////////
SELECT CT.SYS_ID
     , SM.SYS_NM_ZH_TW
	 , CT.ROLE_ID
	 , C.ROLE_GROUP_CONDITION_ID
	 , C.ROLE_GROUP_CONDITION_NM_ZH_TW
  FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION C
  JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT CT
    ON C.ROLE_GROUP_CONDITION_ID = CT.ROLE_GROUP_CONDITION_ID
  JOIN SYS_SYSTEM_MAIN SM
    ON SM.SYS_ID = CT.SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN
 WHERE C.ROLE_GROUP_CONDITION_SYNTAX COLLATE Chinese_Taiwan_Stroke_BIN > ''

 SELECT * FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT CT
 JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION C
   ON C.ROLE_GROUP_CONDITION_ID = CT.ROLE_GROUP_CONDITION_ID

--��ۦPROLE_GROUP_CONDITION_ID����������X�֦b�@�����
SELECT DISTINCT condition.ROLE_GROUP_CONDITION_ID
     , condition.ROLE_GROUP_CONDITION_NM_ZH_TW
     , condition.ROLE_GROUP_CONDITION_SYNTAX
     , STUFF((SELECT ',' + CT.SYS_ID + '-' + CT.ROLE_ID
                FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION C
                JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT CT
                  ON C.ROLE_GROUP_CONDITION_ID = CT.ROLE_GROUP_CONDITION_ID
               WHERE ROLE_GROUP_CONDITION_SYNTAX = condition.ROLE_GROUP_CONDITION_SYNTAX
                 FOR XML PATH('')),1,1,'') AS ROLE
				 INTO #GROUP_CONDITION
  FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION condition
SELECT * FROM #GROUP_CONDITION

--SP����
DECLARE @SqlExecuteString NVARCHAR(MAX);
DECLARE @UnUseSys TABLE (SYS_ID VARCHAR(12));

INSERT INTO @UnUseSys VALUES
('AC2AP'),
--('ACCAP'),
('ADSAP'),
('BA2AP'),
('BUSAP'),
('CBMAP'),
--('CRMAP'),
--('CRSAP'),
('DWHAP'),
--('ERPAP'),
('ETKTAP'),
('FITAP'),
('FNAP'),
('GITAP'),
--('GITPCM'),
--('HCMAP'),
--('HTLAP'),
('HTLMRP'),
('LOCAP'),
('MELAP'),
--('MKTAP'),
('MTMRP'),
('ORDRAP'),
('PDMAP'),
('POIAP'),
--('PUBAP'),
--('QAMAP'),
--('RPMAP'),
('SCMAP'),
('TGUAP'),
('TKTAP'),
('TOURAP'),
('WMP2AP');

--�u�d�U���\���t�Ϊ�����
--SELECT DISTINCT ROLE_GROUP_CONDITION_ID FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
--WHERE SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN NOT IN (SELECT SYS_ID FROM @UnUseSys)

--SP������X�ninsert���⪺���q�y�k
--SELECT D.SYS_ID
--      , SM.SYS_NM_ZH_TW
--      , M.ROLE_GROUP_CONDITION_ID
--      , M.ROLE_GROUP_CONDITION_NM_ZH_TW
--      , D.ROLE_ID
--      , R.ROLE_NM_ZH_TW
-- FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION M
-- JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT	D
--   ON D.ROLE_GROUP_CONDITION_ID = M.ROLE_GROUP_CONDITION_ID
--  AND D.SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN NOT IN (SELECT SYS_ID FROM @UnUseSys)
-- JOIN SYS_SYSTEM_MAIN SM
--   ON SM.SYS_ID = D.SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN
-- JOIN SYS_SYSTEM_ROLE R
--   ON SM.SYS_ID = R.SYS_ID
--  AND D.ROLE_ID COLLATE Chinese_Taiwan_Stroke_BIN = R.ROLE_ID


;WITH groupmain AS(
        SELECT
            'IF EXISTS(SELECT *
                         FROM RAW_CM_USER U' +
						 CASE WHEN C.ROLE_GROUP_CONDITION_SYNTAX LIKE '%C.%'
						      THEN ' JOIN RAW_CM_ORG_COM C ON U.USER_COM_ID = C.COM_ID ' ELSE '' END + 
						 CASE WHEN C.ROLE_GROUP_CONDITION_SYNTAX LIKE '%O.%'  
							  THEN ' JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID ' ELSE '' END + 
						 CASE WHEN C.ROLE_GROUP_CONDITION_SYNTAX LIKE '%S.%'  
							  THEN ' LEFT JOIN RAW_CM_ORG_COM S ON S.IS_SALARY_COM = ''Y'' AND S.COM_ID = U.USER_SALARY_COM_ID ' ELSE '' END +
                      + ' WHERE U.USER_ID = '''+ '00D223' +''''
            + CASE WHEN ROLE_GROUP_CONDITION_SYNTAX > ''
	               THEN ' AND (' + ROLE_GROUP_CONDITION_SYNTAX + ')'
                   ELSE '' END +
            ') BEGIN '
            +
            STUFF((
                SELECT 'INSERT #SYS_ROLE VALUES('''
                     + D.SYS_ID + ''',''' 
                     + SM.SYS_NM_ZH_TW + ''',''' 
                     + M.ROLE_GROUP_CONDITION_ID + ''',''' 
                     + M.ROLE_GROUP_CONDITION_NM_ZH_TW + ''',''' 
                     + D.ROLE_ID + ''','''
                     + R.ROLE_NM_ZH_TW + ''',NULL);'
                  FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION M
                  JOIN #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT	D
                    ON D.ROLE_GROUP_CONDITION_ID = M.ROLE_GROUP_CONDITION_ID
				   AND D.SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN NOT IN (SELECT SYS_ID FROM @UnUseSys)
                  JOIN SYS_SYSTEM_MAIN SM
                    ON SM.SYS_ID = D.SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN
                  JOIN SYS_SYSTEM_ROLE R
                    ON SM.SYS_ID = R.SYS_ID
                   AND D.ROLE_ID COLLATE Chinese_Taiwan_Stroke_BIN = R.ROLE_ID
                 WHERE M.ROLE_GROUP_CONDITION_ID = C.ROLE_GROUP_CONDITION_ID
                FOR XML PATH(''),TYPE
            ).value('(./text())[1]','VARCHAR(MAX)'), 1, 0, '')
            +
            'END;'
          AS SqlSyntax
      FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION C
     WHERE C.ROLE_GROUP_CONDITION_SYNTAX COLLATE Chinese_Taiwan_Stroke_BIN > ''
	   AND C.ROLE_GROUP_CONDITION_ID IN (SELECT DISTINCT ROLE_GROUP_CONDITION_ID 
	                                       FROM #SYS_SYSTEM_ROLE_GROUP_CONDITION_COLLECT
                                          WHERE SYS_ID COLLATE Chinese_Taiwan_Stroke_BIN NOT IN (SELECT SYS_ID FROM @UnUseSys))
)
SELECT @SqlExecuteString = STUFF((SELECT SqlSyntax + '' FROM groupmain FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)'),1,0,'');
SELECT @SqlExecuteString;

--Result
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_ORG_COM C ON U.USER_COM_ID = C.COM_ID  WHERE U.USER_ID = '00D223' AND (C.COM_BU = N'T')) BEGIN INSERT #SYS_ROLE VALUES('CRMAP','�Ȥ�P��A�Ⱥ޲z���x','AgentDefault','�k�H�w�]','AgentEditor','�k�H�s���',NULL);INSERT #SYS_ROLE VALUES('CRMAP','�Ȥ�P��A�Ⱥ޲z���x','AgentDefault','�k�H�w�]','AgentUser','�k�H�ϥΪ�',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00D223' AND (O.USER_DEPT = N'D624' AND( U.USER_UNIT_ID = N'T1' OR U.USER_UNIT_ID = N'T2' ))) BEGIN INSERT #SYS_ROLE VALUES('ERPAP','����ERP���x','SERPDEV','SERP�}�o�H��','GRANTOR','������v�H',NULL);INSERT #SYS_ROLE VALUES('ERPAP','����ERP���x','SERPDEV','SERP�}�o�H��','OP','�t�ξާ@�H��',NULL);INSERT #SYS_ROLE VALUES('ERPAP','����ERP���x','SERPDEV','SERP�}�o�H��','SIT','�޳N�����ϥΪ�',NULL);INSERT #SYS_ROLE VALUES('ERPAP','����ERP���x','SERPDEV','SERP�}�o�H��','SYS','�t�κ޲z��',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00D223' AND (( O.USER_COM_ID = N'!' AND( O.USER_DEPT = N'D583' OR O.USER_DEPT = N'D582' ))OR( O.USER_TITLE > N'G056' AND( O.USER_COM_ID = N'ZA' AND( O.USER_DEPT = N'D015' ))))) BEGIN INSERT #SYS_ROLE VALUES('HCMAP','�H�ꥭ�x','HCMAPTEST','HCMAPTEST','OVERTIME','�[�Z',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00D223' AND (( O.USER_TITLE > N'G056' )OR(( O.USER_COM_ID = N'ZA' AND( O.USER_DEPT = N'D003' OR O.USER_DEPT = N'D016' OR O.USER_DEPT = N'D019' ))OR( O.USER_PLACE = N'B029' AND( O.USER_DEPT = N'D004' OR O.USER_DEPT = N'D006' OR O.USER_DEPT = N'D543' OR O.USER_DEPT = N'D011' OR O.USER_DEPT = N'D003' OR O.USER_DEPT = N'D013' OR O.USER_DEPT = N'D005' OR O.USER_DEPT = N'D049' OR O.USER_DEPT = N'D007' ))OR( O.USER_PLACE = N'B058' AND O.USER_DEPT = N'D593' )OR( O.USER_PLACE = N'B062' )OR( O.USER_PLACE = N'B015' AND O.USER_DEPT = N'D080' )OR( O.USER_PLACE = N'B011' AND O.USER_DEPT = N'D012' AND O.USER_COM_ID = N'TT' )))) BEGIN INSERT #SYS_ROLE VALUES('HCMAP','�H�ꥭ�x','HCMTEST2','HCMTEST2','HRRecruit','�H��-�l��',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00D223' AND (O.USER_BIZ_TITLE = N'O001' OR U.USER_UNIT_ID = N'T1')) BEGIN INSERT #SYS_ROLE VALUES('HCMAP','�H�ꥭ�x','HCMTEST3','HCMTEST3','APPDveloper','���APP�}�o�H��',NULL);INSERT #SYS_ROLE VALUES('HCMAP','�H�ꥭ�x','HCMTEST3','HCMTEST3','HR','�H��',NULL);INSERT #SYS_ROLE VALUES('HCMAP','�H�ꥭ�x','HCMTEST3','HCMTEST3','HRRecruit','�H��-�l��',NULL);END;IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00D223' AND (O.USER_JOB_TITLE = N'T046' AND O.USER_JOB_TITLE = N'T047')) BEGIN INSERT #SYS_ROLE VALUES('HCMAP','�H�ꥭ�x','Scheduling','�ƯZ�v��','Scheduling','�ƯZ',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00D223' AND (O.USER_COM_ID = N'T' AND U.USER_UNIT_ID = N'D2')) BEGIN INSERT #SYS_ROLE VALUES('HTLAP','�q�Х��x','D2OP','�ۥѦ� OP','D2OP','�ۥѦ� OP',NULL);INSERT #SYS_ROLE VALUES('HTLAP','�q�Х��x','D2OP','�ۥѦ� OP','USER','�в��ϥΪ�',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00D223' AND (O.USER_COM_ID = N'!' AND O.USER_PLACE = N'B015' AND O.USER_DEPT = N'D624' AND O.USER_TEAM = N'S741')) BEGIN INSERT #SYS_ROLE VALUES('MCMAP','��P���ʺ޲z�t��','22B0IT','�M�ת��Ѳ�IT','IT','IT',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00D223' AND (O.USER_COM_ID = N'T' AND O.USER_COM_ID = N'TT' AND( O.USER_BIZ_TITLE >= N'O039' AND O.USER_DEPT = N'0078' AND( O.USER_LEVEL <> N'010' AND U.USER_WORK_ID < N'10' )))) BEGIN INSERT #SYS_ROLE VALUES('PUBAP','���ऽ�Υ��x','test','test','ITWEB','��T�B���ʨt�γ�',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U WHERE U.USER_ID = '00D223' AND (U.USER_WORK_ID = N'31')) BEGIN INSERT #SYS_ROLE VALUES('RPMAP','�����ӱ��ʺ޲z���x','JOB31CostValuation','�β��H���q�{�»P�ζO����','GitCostValuation','�ζO����',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U WHERE U.USER_ID = '00D223' AND (U.USER_UNIT_ID = N'98' AND U.USER_WORK_ID <> N'26')) BEGIN INSERT #SYS_ROLE VALUES('VISAAP','ñ�ҥ��x','VSAP002','ñ����','VISA','ñ����',NULL);END;

IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00ZZZZ' AND (O.USER_COM_ID = N'T' AND O.USER_DEPT = N'D624')) BEGIN INSERT #SYS_ROLE VALUES('ERPAP','����ERP���x','OPGroup','OP','OP','�t�ξާ@�H��',NULL);INSERT #SYS_ROLE VALUES('ERPAP','����ERP���x','OPGroup','OP','SYS','�t�κ޲z��',NULL);INSERT #SYS_ROLE VALUES('TOURAP','�ȹC���~���Υ��x','OPGroup','OP','MRK','��P�H��',NULL);INSERT #SYS_ROLE VALUES('TKTAP','�������x','OPGroup','OP','RC','RC',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00ZZZZ' AND (U.USER_UNIT_ID = N'87' OR O.USER_COM_ID = N'TT' OR (O.USER_BIZ_TITLE >= N'O039' OR O.USER_DEPT = N'0078' OR (O.USER_LEVEL <> N'010' OR U.USER_WORK_ID < N'10')))) BEGIN INSERT #SYS_ROLE VALUES('PUBAP','���ऽ�Υ��x','Program','�{���]�p�v','IT','IT',NULL);INSERT #SYS_ROLE VALUES('ERPAP','����ERP���x','Program','�{���]�p�v','GRANTOR','������v�H',NULL);END;
IF EXISTS(SELECT *                           FROM RAW_CM_USER U JOIN RAW_CM_USER_ORG O ON U.USER_ID = O.USER_ID  WHERE U.USER_ID = '00ZZZZ' AND (O.USER_BIZ_TITLE = N'S003' AND O.USER_TEAM = N'S020' AND(U.USER_UNIT_ID = N'36'))) BEGIN INSERT #SYS_ROLE VALUES('TOURAP','�ȹC���~���Υ��x','SysAnalysis','�t�Τ��R�v','BuTour','����BU�H��',NULL);INSERT #SYS_ROLE VALUES('TOURAP','�ȹC���~���Υ��x','SysAnalysis','�t�Τ��R�v','ProdGit','�β��H��',NULL);INSERT #SYS_ROLE VALUES('LOCAP','�a�����~���x','SysAnalysis','�t�Τ��R�v','IT','IT',NULL);INSERT #SYS_ROLE VALUES('ORDRAP','�ȹC�q�業�x','SysAnalysis','�t�Τ��R�v','ProdGit','�β��H��',NULL);END;