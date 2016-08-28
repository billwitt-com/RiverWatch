


DELETE FROM NEWexpWater 
WHERE id IN ( 
     SELECT MIN(id) FROM NEWexpWater 
     GROUP BY SampleNumber
     -- could add a WHERE clause here to further filter
     HAVING count(*) > 1)