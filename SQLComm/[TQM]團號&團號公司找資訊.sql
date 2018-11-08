--團體行程檢查 : line 3
--領隊隨團檢查紀錄
--顧客品質反應[新增]



--團體行程檢查管理
--1.如果有行程檢查單號

SELECT at00_guid, at00_id, at00_prod, at00_prod_comp,
at00_check_stfn_name, at00_check_stfn, at00_status, at00_memo, at00_wmpdoc_pk
FROM qatlc00 a WITH (NOLOCK)
WHERE at00_id = '行程檢查單號'

-- [1]執行上方SQL找到團號跟團號公司

-- [2]拿團號跟團公司執行下面SQL
-- 找到團名，出團日期，線別，副線別，地區，TP，RC，改善單位，改善對象，改善對象姓名
SELECT a.prod_prod, a.prod_prodcomp, a.prod_days, a.prod_name, a.prod_d8, a.prod_local1, a.prod_local2,
a.prod_line, a.prod_dline, c.line_dname, a.prod_area, a.prod_darea, f.area_dname, a.prod_prof, 
a.prod_tp, d.stfn_cname AS prod_tp_name,  
a.prod_rc, e.stfn_cname AS prod_rc_name,  
g.locl_dname AS locl1_dame, 
h.locl_dname AS locl2_dame, 
g.locl_local AS locl1_local,
h.locl_local AS locl2_local, 
i.pd12_apptlsta, i.pd12_apptl, pd12_apptlname,
i.pd12_suggtlsta, i.pd12_suggtl, pd12_suggtlname
FROM Views_tppdm10 AS a WITH (NOLOCK)
LEFT JOIN Views_tppdm12 i ON a.prod_prod = i.pd12_prod AND a.prod_prodcomp = i.pd12_prodcomp
LEFT JOIN Views_istbm10 AS c ON a.prod_line = c.line_line AND a.prod_dline = c.line_dline  
LEFT JOIN eventopagm20 AS d ON a.prod_tp = d.stfn_stfn  
LEFT JOIN eventopagm20 AS e ON a.prod_rc = e.stfn_stfn 
LEFT JOIN Views_istbm20 AS f ON a.prod_area = f.area_area AND a.prod_darea = f.area_darea  
LEFT JOIN eventopagm10 AS g ON a.prod_local1 = g.locl_local 
LEFT JOIN eventopagm10 AS h ON a.prod_local2 = h.locl_local

--[3]拿團號跟團號公司執行下面SQL，找核派或建議領隊
SELECT pd14_tl, pd14_name, pd14_main, pd14_sts  FROM Views_tppdm14 WITH (NOLOCK)
--pd14_sts 給1是核派，給0是建議




--領隊隨團檢查紀錄
-- 會執行和上方團體行程檢查相同一塊程式碼 [Controller:QATourCheck   Action: GetGroup']




--顧客品質反應
--[新增]
--拿團號跟團號公司執行下面語法 [檔案 : ErpInformationRepositorySql.cs]
--找RC，線別，副線別
--領隊(這邊的領隊和前面不同，找Views_tppdm12的pd12_apptlsta，pd12_apptl，pd12_apptlname)
SELECT a.prod_prod, a.prod_prodcomp, a.prod_days, a.prod_name, a.prod_d8, a.prod_local1, a.prod_local2,
a.prod_line, a.prod_dline, c.line_dname, a.prod_area, a.prod_darea, f.area_dname, a.prod_prof, 
a.prod_tp, d.stfn_cname AS prod_tp_name,  
a.prod_rc, e.stfn_cname AS prod_rc_name,  
g.locl_dname AS locl1_dame, 
h.locl_dname AS locl2_dame, 
g.locl_local AS locl1_local,
h.locl_local AS locl2_local, 
i.pd12_apptlsta, i.pd12_apptl, pd12_apptlname,
i.pd12_suggtlsta, i.pd12_suggtl, pd12_suggtlname
FROM Views_tppdm10 AS a WITH (NOLOCK)
LEFT JOIN Views_tppdm12 i ON a.prod_prod = i.pd12_prod AND a.prod_prodcomp = i.pd12_prodcomp
LEFT JOIN Views_istbm10 AS c ON a.prod_line = c.line_line AND a.prod_dline = c.line_dline  
LEFT JOIN eventopagm20 AS d ON a.prod_tp = d.stfn_stfn  
LEFT JOIN eventopagm20 AS e ON a.prod_rc = e.stfn_stfn 
LEFT JOIN Views_istbm20 AS f ON a.prod_area = f.area_area AND a.prod_darea = f.area_darea  
LEFT JOIN eventopagm10 AS g ON a.prod_local1 = g.locl_local 
LEFT JOIN eventopagm10 AS h ON a.prod_local2 = h.locl_local

--再用上面找到的RC，執行下面SQL語法找主管
--給ac93_prod_line、ac93_prod_dline、ac93_rc_prof
--抓到主管的員工編號ac93_stfn
SELECT 
ac93_prod_line, ac93_prod_dline, ac93_rc_prof, ac93_stfn, 
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate 
FROM qaqcs93 WITH (NOLOCK) 

--拿團號跟團號公司再去抓客訴比例，大人小孩 嬰兒人數
SELECT pd11_prod, pd11_prodcomp, pd11_act1, pd11_act2, pd11_act3, pd11_tl 
FROM Views_tppdm11 a WITH (NOLOCK)



--顧客品質反應
--[編輯]

--團號跟團號公司以及訂單年分、訂單號碼去呼叫CRM的API
--執行下面SQL抓旅客資訊
--得到VIP資訊
SELECT qc40_order_sn, qc40_order_year, qc40_order_ordr, qc40_order_seq, qc40_order_name,
qc40_crmvip_fg, qc40_crm_pk, qc40_qcsvip_fg, qc40_memo,
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate
FROM qaqcs40 

--執行'/QACustomerComplaints/GetGroupData' SQL
--取得RC 線別 副線別
SELECT a.prod_prod, a.prod_prodcomp, a.prod_days, a.prod_name, a.prod_d8, a.prod_local1, a.prod_local2,
a.prod_line, a.prod_dline, c.line_dname, a.prod_area, a.prod_darea, f.area_dname, a.prod_prof, 
a.prod_tp, d.stfn_cname AS prod_tp_name,  
a.prod_rc, e.stfn_cname AS prod_rc_name,  
g.locl_dname AS locl1_dame, 
h.locl_dname AS locl2_dame, 
g.locl_local AS locl1_local,
h.locl_local AS locl2_local, 
i.pd12_apptlsta, i.pd12_apptl, pd12_apptlname,
i.pd12_suggtlsta, i.pd12_suggtl, pd12_suggtlname
FROM Views_tppdm10 AS a WITH (NOLOCK)
LEFT JOIN Views_tppdm12 i ON a.prod_prod = i.pd12_prod AND a.prod_prodcomp = i.pd12_prodcomp
LEFT JOIN Views_istbm10 AS c ON a.prod_line = c.line_line AND a.prod_dline = c.line_dline  
LEFT JOIN eventopagm20 AS d ON a.prod_tp = d.stfn_stfn  
LEFT JOIN eventopagm20 AS e ON a.prod_rc = e.stfn_stfn 
LEFT JOIN Views_istbm20 AS f ON a.prod_area = f.area_area AND a.prod_darea = f.area_darea  
LEFT JOIN eventopagm10 AS g ON a.prod_local1 = g.locl_local 
LEFT JOIN eventopagm10 AS h ON a.prod_local2 = h.locl_local


--再用RC資訊  prod_prof  抓主管
--如果團號包含FT，直接執行下面SQL
SELECT 
ac93_prod_line, ac93_prod_dline, ac93_rc_prof, ac93_stfn, 
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate 
FROM qaqcs93 WITH (NOLOCK) 

--如果不包含FT，用RC原編，執行下面語法

IF NOT EXISTS(SELECT * FROM Views_opagm20_dept WHERE stfn_stfn = 'RC原編')
BEGIN
SELECT 'N'
END
ELSE
BEGIN
SELECT a.*,b.tabl_dname AS Job1Name, c.pp01_area, c.pp01_area_name  
FROM Views_opagm20_dept AS a WITH (NOLOCK) 
LEFT JOIN eventistbm00 AS b WITH (NOLOCK) ON b.tabl_type='JOB1' AND b.tabl_code = a.stfn_job1 
LEFT JOIN eventpsppm01 AS c WITH (NOLOCK) ON c.pp01_stfn = a.stfn_stfn 
END

--找到stfn_prof，搭配線別副線別，執行下面語法找主管
SELECT 
ac93_prod_line, ac93_prod_dline, ac93_rc_prof, ac93_stfn, 
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate 
FROM qaqcs93 WITH (NOLOCK) 

--團號團號公司找大人小孩嬰兒人數，客訴比例
SELECT pd11_prod, pd11_prodcomp, pd11_act1, pd11_act2, pd11_act3, pd11_tl 
FROM Views_tppdm11 a WITH (NOLOCK)