
SELECT     [SampleNumber]    , COUNT(*) AS dupes  
FROM         [dbRiverwatchWaterData].[dbo].[expStnMetal]  
where 
ProjectID = 1
and rep = 1
and 
AL_T is not null
and AS_T is not null
and CA_T is not null
GROUP BY [SampleNumber]  
HAVING      (COUNT(*) > 1) 
order by SampleNumber

select *
FROM         [dbRiverwatchWaterData].[dbo].[expStnMetal]  
where 
--ProjectID = 1
--and (rep = 1 or rep = 2)
-- and 
AL_T is not null
and AS_T is not null
and CA_T is not null
order by rep desc, SampleNumber



SELECT     [SampleNumber]    , COUNT(*) AS dupes  , Date
FROM         [dbRiverwatchWaterData].[dbo].[expStnMetal]  
GROUP BY [SampleNumber]   , Date
HAVING      (COUNT(*) >2  ) 
order by Date asc



SELECT     [SampleNumber]    , COUNT(*) AS dupes  
FROM         [dbRiverwatchWaterData].[dbo].[expStnMetal]  
GROUP BY [SampleNumber]  
HAVING      (COUNT(*) > 1) 
order by SampleNumber

select * 
from  [dbRiverwatchWaterData].[dbo].[expStnMetal]  
where
SampleNumber = '104201601081200'
