DECLARE @Property VARCHAR(MAX) ='

            public DBVarChar asm10_id { get; set; }
            public DBChar IsProRataBasis { get; set; }
            public DBVarChar QueryDate { get; set; }
            public DBVarChar stfn_stfn { get; set; }
            public DBChar prm22_comp { get; set; }
            public DBVarChar prm22_dept { get; set; }
            public DBChar pp01_workcomp { get; set; }
            public DBVarChar pp01_dept { get; set; }
            public EnumSort Sort { get; set; }
            public DBVarChar ShiftEventType { get; set; }
            public DBChar IncludeStaffLeave { get; set; }
            public DBChar DeferredLeaveHoursNotSetYet { get; set; }

';

SELECT (SELECT top 1 Myvalues FROM [dbo].[tb_split](t.Obj, ' ') WHERE Sid = 2) + ' =  new '+
       (SELECT top 1 Myvalues FROM [dbo].[tb_split](t.Obj, ' ') WHERE Sid = 1) + '(),'
  FROM (
        SELECT LTRIM(RTRIM(REPLACE(REPLACE(Myvalues, 'public', ''), '{ get; set; }', ''))) AS Obj
          FROM [dbo].[tb_split](@Property ,CHAR(10))
         WHERE REPLACE(Myvalues,CHAR(13), '') <> ''
       ) t