--�����{�ˬd : line 3
--�ⶤ�H���ˬd����
--�U�ȫ~�����[�s�W]



--�����{�ˬd�޲z
--1.�p�G����{�ˬd�渹

SELECT at00_guid, at00_id, at00_prod, at00_prod_comp,
at00_check_stfn_name, at00_check_stfn, at00_status, at00_memo, at00_wmpdoc_pk
FROM qatlc00 a WITH (NOLOCK)
WHERE at00_id = '��{�ˬd�渹'

-- [1]����W��SQL���θ���θ����q

-- [2]���θ���Τ��q����U��SQL
-- ���ΦW�A�X�Τ���A�u�O�A�ƽu�O�A�a�ϡATP�ARC�A�ﵽ���A�ﵽ��H�A�ﵽ��H�m�W
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

--[3]���θ���θ����q����U��SQL�A��֬��Ϋ�ĳ�ⶤ
SELECT pd14_tl, pd14_name, pd14_main, pd14_sts  FROM Views_tppdm14 WITH (NOLOCK)
--pd14_sts ��1�O�֬��A��0�O��ĳ




--�ⶤ�H���ˬd����
-- �|����M�W������{�ˬd�ۦP�@���{���X [Controller:QATourCheck   Action: GetGroup']




--�U�ȫ~�����
--[�s�W]
--���θ���θ����q����U���y�k [�ɮ� : ErpInformationRepositorySql.cs]
--��RC�A�u�O�A�ƽu�O
--�ⶤ(�o�䪺�ⶤ�M�e�����P�A��Views_tppdm12��pd12_apptlsta�Apd12_apptl�Apd12_apptlname)
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

--�A�ΤW����쪺RC�A����U��SQL�y�k��D��
--��ac93_prod_line�Bac93_prod_dline�Bac93_rc_prof
--���D�ު����u�s��ac93_stfn
SELECT 
ac93_prod_line, ac93_prod_dline, ac93_rc_prof, ac93_stfn, 
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate 
FROM qaqcs93 WITH (NOLOCK) 

--���θ���θ����q�A�h��ȶD��ҡA�j�H�p�� ����H��
SELECT pd11_prod, pd11_prodcomp, pd11_act1, pd11_act2, pd11_act3, pd11_tl 
FROM Views_tppdm11 a WITH (NOLOCK)



--�U�ȫ~�����
--[�s��]

--�θ���θ����q�H�έq��~���B�q�渹�X�h�I�sCRM��API
--����U��SQL��ȫȸ�T
--�o��VIP��T
SELECT qc40_order_sn, qc40_order_year, qc40_order_ordr, qc40_order_seq, qc40_order_name,
qc40_crmvip_fg, qc40_crm_pk, qc40_qcsvip_fg, qc40_memo,
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate
FROM qaqcs40 

--����'/QACustomerComplaints/GetGroupData' SQL
--���oRC �u�O �ƽu�O
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


--�A��RC��T  prod_prof  ��D��
--�p�G�θ��]�tFT�A��������U��SQL
SELECT 
ac93_prod_line, ac93_prod_dline, ac93_rc_prof, ac93_stfn, 
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate 
FROM qaqcs93 WITH (NOLOCK) 

--�p�G���]�tFT�A��RC��s�A����U���y�k

IF NOT EXISTS(SELECT * FROM Views_opagm20_dept WHERE stfn_stfn = 'RC��s')
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

--���stfn_prof�A�f�t�u�O�ƽu�O�A����U���y�k��D��
SELECT 
ac93_prod_line, ac93_prod_dline, ac93_rc_prof, ac93_stfn, 
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate 
FROM qaqcs93 WITH (NOLOCK) 

--�θ��θ����q��j�H�p������H�ơA�ȶD���
SELECT pd11_prod, pd11_prodcomp, pd11_act1, pd11_act2, pd11_act3, pd11_tl 
FROM Views_tppdm11 a WITH (NOLOCK)