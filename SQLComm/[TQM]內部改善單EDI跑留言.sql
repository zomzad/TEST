--即將逾期
SELECT	aq00_guid, aq00_qcs_date, aq00_qcs_no, aq00_seq,
		aq00_id,aq00_status, aq00_es_propose_date, aq00_cstfn,
		aq00_owner_stfn,aq00_es_improve_date 
FROM	qaqip00 AS a 
WHERE	(aq00_status is null OR aq00_status = '0') 
		AND(SELECT TOP 1 work_date 
FROM( 
		SELECT TOP (3) work_date, row_number() OVER(ORDER BY work_date) AS RN 
		FROM Views_pstbm00 WITH (NOLOCK) 
		WHERE work_sts <> '0' 
		AND work_country = 'TW' 
		AND work_city = 'TPE' 
		AND (work_date > GETDATE()) 
	)
AS		newpstbm00 
ORDER BY RN DESC) = aq00_es_propose_date 



--逾期通知
SELECT * FROM( 
SELECT AQ10.aq10_check_stfn,opag20.stfn_cname,AQ10.aq10_check_type,AQ10.aq10_check_seq,aq00_guid, aq00_qcs_date, aq00_qcs_no, aq00_seq, aq00_id, aq00_notify_stfn_list, 
aq00_status, aq00_es_propose_date, aq00_cstfn, aq00_owner_stfn,aq00_es_improve_date  
,(SELECT COUNT(work_sts) -1   
FROM Views_pstbm00 WITH (NOLOCK)   
WHERE work_sts <> '0'   
AND (work_date BETWEEN aq00_es_propose_date AND GETDATE())   
AND work_country = 'TW'  
AND work_city = 'TPE' ) as CountDelayDay 
FROM qaqip00 AS a  
JOIN qaqip10 AQ10 
ON a.aq00_guid = AQ10.aq10_guid 
JOIN eventopagm20 AS opag20 
ON AQ10.aq10_check_stfn = opag20.stfn_stfn
WHERE 1=1 
AND (aq00_status is null OR aq00_status = '0' OR aq00_status = '2' OR aq00_status = '4')  
AND (SELECT COUNT(work_sts) -1   
FROM Views_pstbm00 WITH (NOLOCK)   
WHERE work_sts <> '0'   
AND (work_date BETWEEN aq00_es_propose_date AND GETDATE())   
AND work_country = 'TW'  
AND work_city = 'TPE' ) % cast (cast( (SELECT sysref_remark FROM sysref10 WHERE sysref_type='QIPSysRefItem' AND sysref_id='AfterDelayDays') as varchar ) as int) =0  
AND CONVERT(char(10), getdate(), 111)  > CONVERT(char(10), aq00_es_propose_date, 111) 
 ) AS List

SELECT * FROM( 
SELECT AQ10.aq10_check_stfn,opag20.stfn_cname,AQ10.aq10_check_type,AQ10.aq10_check_seq,aq00_guid, aq00_qcs_date, aq00_qcs_no, aq00_seq, aq00_id,aq00_notify_stfn_list,  
aq00_status, aq00_es_improve_date, aq00_cstfn, aq00_owner_stfn,aq00_es_propose_date  
,(SELECT COUNT(work_sts) -1   
FROM Views_pstbm00 WITH (NOLOCK)   
WHERE work_sts <> '0'   
AND (work_date BETWEEN aq00_es_improve_date AND GETDATE())   
AND work_country = 'TW'  
AND work_city = 'TPE' ) as CountDelayDay 
FROM qaqip00 AS a  
JOIN qaqip10 AQ10 
ON a.aq00_guid = AQ10.aq10_guid 
JOIN eventopagm20 AS opag20 
ON AQ10.aq10_check_stfn = opag20.stfn_stfn 
WHERE 1=1 
AND (aq00_status = '3' OR aq00_status = '5' OR aq00_status = '6' OR aq00_status = '8')  
AND (SELECT COUNT(work_sts) -1   
FROM Views_pstbm00 WITH (NOLOCK)   
WHERE work_sts <> '0'   
AND (work_date BETWEEN aq00_es_improve_date AND GETDATE())   
AND work_country = 'TW'  
AND work_city = 'TPE' ) % cast (cast( (SELECT sysref_remark FROM sysref10 WHERE sysref_type='QIPSysRefItem' AND sysref_id='AfterDelayDays') as varchar ) as int) =0  
AND CONVERT(char(10), getdate(), 111)  > CONVERT(char(10), aq00_es_improve_date, 111) 
 ) AS List