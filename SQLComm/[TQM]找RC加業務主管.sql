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
FROM Views_tppdm12 i WITH (NOLOCK)
LEFT JOIN Views_tppdm10 AS a ON a.prod_prod = i.pd12_prod AND a.prod_prodcomp = i.pd12_prodcomp
LEFT JOIN Views_istbm10 AS c ON a.prod_line = c.line_line AND a.prod_dline = c.line_dline  
LEFT JOIN eventopagm20 AS d ON a.prod_tp = d.stfn_stfn  
LEFT JOIN eventopagm20 AS e ON a.prod_rc = e.stfn_stfn 
LEFT JOIN Views_istbm20 AS f ON a.prod_area = f.area_area AND a.prod_darea = f.area_darea  
LEFT JOIN eventopagm10 AS g ON a.prod_local1 = g.locl_local 
LEFT JOIN eventopagm10 AS h ON a.prod_local2 = h.locl_local
WHERE a.prod_rc = '' OR e.stfn_cname = ''

select distinct a.prod_prod,a.prod_prodcomp, a.prod_rc,d.stfn_cname,a.prod_line,a.prod_dline,c.line_dname 
from eventopagm20 as d 
left join Views_tppdm10 as a on a.prod_rc = d.stfn_stfn 
LEFT JOIN Views_istbm10 AS c ON a.prod_line = c.line_line AND a.prod_dline = c.line_dline  
where d.stfn_stfn = 'A677'
order by a.prod_line, a.prod_dline



SELECT * FROM(
SELECT
           ac00_guid, ac00_date, ac00_no, ac00_status, b.sysref_name_ZH_TW AS QcSstatus , 
		   a.istfn, a.istfnname, ac00_process_stfn, j.stfn_cname AS ProcessStfnName, ac00_prod, ac00_prodcomp, 
		   ac00_year, ac00_order, ac00_leader_type, ac00_leader_name, ac00_cname, ac00_complain, ac00_summary,  
		   ac00_event_type1, ac00_event_type2, ac00_event_type3, ac00_event_type4,
					 (
           				REPLACE(STUFF((
           					SELECT + ' ' + ISNULL(f.langsysref_description,'')
           					+ '、' + ISNULL(g.langsysref_description,'')
           					+ '、' + ISNULL(h.langsysref_description,'')
           					+ '、' + ISNULL(i.langsysref_description,'')
           					FROM qaqcs00 AS e WITH (NOLOCK)
           					LEFT JOIN langsysref00 AS f WITH (NOLOCK) ON f.langsysref_type IN ('QcsAccidentNew') AND f.langsysref_kind = 'DES' AND f.langsysref_culture = 'zh_tw' and e.ac00_event_type1 = f.langsysref_id
           					LEFT JOIN langsysref00 AS g WITH (NOLOCK) ON g.langsysref_type IN ('QcsAccidentNew') AND g.langsysref_kind = 'DES' AND g.langsysref_culture = 'zh_tw' and e.ac00_event_type2 = g.langsysref_id
           					LEFT JOIN langsysref00 AS h WITH (NOLOCK) ON h.langsysref_type IN ('QcsAccidentNew') AND h.langsysref_kind = 'DES' AND h.langsysref_culture = 'zh_tw' and e.ac00_event_type3 = h.langsysref_id
           					LEFT JOIN langsysref00 AS i WITH (NOLOCK) ON i.langsysref_type IN ('QcsAccidentNew') AND i.langsysref_kind = 'DES' AND i.langsysref_culture = 'zh_tw' and e.ac00_event_type4 = i.langsysref_id
           					WHERE e.ac00_guid = a.ac00_guid
           					FOR XML PATH('')
           				),1,1,''), ' ', '')
           			) AS EventType,
		   ac00_qip_status, c.sysref_name_ZH_TW AS QcsQipStatus, 
		   (select count(*) from qaqip20 WITH (NOLOCK) where aq20_link_pk = a.ac00_date + '|' + CONVERT(varchar(10), a.ac00_no)) AS CountQIP,
		   ac00_bpq_status, d.sysref_name_ZH_TW AS QcsBpqStatus,
		   (select count(*) from Views_QualityRecordR WITH (NOLOCK) where RefTP = 'QCS' and RefPK = a.ac00_date + '-' + CONVERT(varchar(10), a.ac00_no)) AS CountBPQ,
		   (
				REPLACE(STUFF((
					SELECT + ',' + k.ac50_qcin_source
					FROM qaqcs50 AS k WITH (NOLOCK)
					WHERE a.ac00_guid = k.ac50_guid
					FOR XML PATH('')
				),1,1,''), ' ', '')
			) AS QcinSource,
			(
				REPLACE(STUFF((
					SELECT + ',' + l.ac60_accident_type
					FROM qaqcs60 AS l WITH (NOLOCK)
					WHERE a.ac00_guid = l.ac60_guid
					FOR XML PATH('')
				),1,1,''), ' ', '')
			) AS AccidentType,
			ac00_response, ac00_internal_date, ac00_external_date, ac00_line, ac00_dline,
			ac00_sales_prof, ac00_prod_prof, ac00_kind, ac00_case_level
		   
FROM qaqcs00 AS a WITH (NOLOCK)
LEFT JOIN sysref10 AS b WITH (NOLOCK) ON b.sysref_type ='QcSstatus' and a.ac00_status = b.sysref_id 
LEFT JOIN sysref10 AS c WITH (NOLOCK) ON c.sysref_type ='QcsQipStatus' and a.ac00_qip_status = c.sysref_id 
LEFT JOIN sysref10 AS d WITH (NOLOCK) ON d.sysref_type ='QcsBpqStatus' and a.ac00_bpq_status = d.sysref_id
LEFT JOIN eventopagm20 AS j WITH (NOLOCK) ON a.ac00_process_stfn = j.stfn_stfn
) AS QCSindex
WHERE 1=1
AND ac00_date between '20150930' and '20150930'
ORDER BY  ac00_date, ac00_no 



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
FROM Views_tppdm12 i WITH (NOLOCK)
LEFT JOIN Views_tppdm10 AS a ON a.prod_prod = i.pd12_prod AND a.prod_prodcomp = i.pd12_prodcomp
LEFT JOIN Views_istbm10 AS c ON a.prod_line = c.line_line AND a.prod_dline = c.line_dline  
LEFT JOIN eventopagm20 AS d ON a.prod_tp = d.stfn_stfn  
LEFT JOIN eventopagm20 AS e ON a.prod_rc = e.stfn_stfn 
LEFT JOIN Views_istbm20 AS f ON a.prod_area = f.area_area AND a.prod_darea = f.area_darea  
LEFT JOIN eventopagm10 AS g ON a.prod_local1 = g.locl_local 
LEFT JOIN eventopagm10 AS h ON a.prod_local2 = h.locl_local
WHERE a.prod_prod = '15ARO05CXA' AND a.prod_prodcomp = 'T'



SELECT ordr_year, ordr_ordr, ordr_prod, ordr_prodcomp, ordr_agent,b.agnt_dname, 
ordr_sales, ordr_act1, ordr_act2, ordr_act3, ordr_prof  
FROM Views_ssorm00 AS a WITH (NOLOCK)
LEFT JOIN Views_opagm00 AS b WITH (NOLOCK) ON a.ordr_agent = b.agnt_agent 
WHERE ordr_ordr = '2031076' AND ordr_year = '2015'


SELECT ordr_year, ordr_ordr, ordr_prod, ordr_prodcomp, ordr_agent,b.agnt_dname, 
ordr_sales, ordr_act1, ordr_act2, ordr_act3, ordr_prof  
FROM Views_ssorm00 AS a WITH (NOLOCK)
LEFT JOIN Views_opagm00 AS b WITH (NOLOCK) ON a.ordr_agent = b.agnt_agent 
WHERE ordr_year = '2015' AND ordr_ordr = '1728859' AND ordr_prod = '15ARO05CXA' AND ordr_prodcomp = 'T' 




--找到相關人員是A115之後
SELECT a.*,b.tabl_dname AS Job1Name, c.pp01_area, c.pp01_area_name  
FROM Views_opagm20_dept AS a WITH (NOLOCK) 
LEFT JOIN eventistbm00 AS b WITH (NOLOCK) ON b.tabl_type='JOB1' AND b.tabl_code = a.stfn_job1 
LEFT JOIN eventpsppm01 AS c WITH (NOLOCK) ON c.pp01_stfn = a.stfn_stfn 
WHERE a.stfn_stfn = 'A041'

--業務主管
SELECT 
ac94_sales_job, ac94_sales_area, ac94_sales_prof, ac94_sales_stfn, 
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate 
FROM qaqcs94 WITH (NOLOCK) 
WHERE 1 = 1
AND ac94_sales_job = '22'
AND ac94_sales_area = 'A001'
AND ac94_sales_prof = 'A2'


--找到RC
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
FROM Views_tppdm12 i WITH (NOLOCK)
LEFT JOIN Views_tppdm10 AS a ON a.prod_prod = i.pd12_prod AND a.prod_prodcomp = i.pd12_prodcomp
LEFT JOIN Views_istbm10 AS c ON a.prod_line = c.line_line AND a.prod_dline = c.line_dline  
LEFT JOIN eventopagm20 AS d ON a.prod_tp = d.stfn_stfn  
LEFT JOIN eventopagm20 AS e ON a.prod_rc = e.stfn_stfn 
LEFT JOIN Views_istbm20 AS f ON a.prod_area = f.area_area AND a.prod_darea = f.area_darea  
LEFT JOIN eventopagm10 AS g ON a.prod_local1 = g.locl_local 
LEFT JOIN eventopagm10 AS h ON a.prod_local2 = h.locl_local
WHERE a.prod_prod = '15ARO05CXA' AND a.prod_prodcomp = 'T'

--SELECT a.*,b.tabl_dname AS Job1Name, c.pp01_area, c.pp01_area_name  
--FROM Views_opagm20_dept AS a WITH (NOLOCK) 
--LEFT JOIN eventistbm00 AS b WITH (NOLOCK) ON b.tabl_type='JOB1' AND b.tabl_code = a.stfn_job1 
--LEFT JOIN eventpsppm01 AS c WITH (NOLOCK) ON c.pp01_stfn = a.stfn_stfn 
--WHERE a.stfn_stfn = '3538'

--產品主管
SELECT 
ac93_prod_line, ac93_prod_dline, ac93_rc_prof, ac93_stfn, 
istfn, istfnname, istfncomp, istfnprof, idate,
mstfn, mstfnname, mstfncomp, mstfnprof, mdate 
FROM qaqcs93 WITH (NOLOCK) 
WHERE 1 = 1
AND ac93_prod_line = '1'
AND ac93_prod_dline = 'C'
AND ac93_rc_prof = '13'

