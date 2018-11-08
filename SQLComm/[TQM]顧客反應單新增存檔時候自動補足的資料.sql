--顧客反應單新增時候，自動補齊的一些資料
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
WHERE a.prod_prod = '15JH930CIK' AND a.prod_prodcomp = 'K'