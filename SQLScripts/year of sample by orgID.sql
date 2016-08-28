

use [RiverWatch]

select distinct Year(DateCollected) as YR
  FROM [RiverWatch].[dbo].[Samples]
  where Organizationid = 45
 -- where NumberSample like '9876.%' and Organizationid = 574
  order by YR desc


