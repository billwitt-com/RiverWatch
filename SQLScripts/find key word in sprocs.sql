
SELECT OBJECT_NAME(object_id), 
       OBJECT_DEFINITION(object_id)
FROM sys.procedures
WHERE OBJECT_DEFINITION(object_id) LIKE '%TBLINBOUNDICP%'
 