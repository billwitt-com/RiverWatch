


(SELECT     BarCode   , COUNT(*) AS dupes  
FROM		[LocalCooler]

GROUP BY BarCode  
HAVING      (COUNT(*) > 1) )

select count(*) 
FROM		[LocalCooler]