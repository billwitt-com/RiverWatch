

SELECT     SampleID , LabID as Barcode, Code as [Sample Type]
FROM      [dbRiverwatchWaterData].[dbo].[tblMetalBarCode] AS a
WHERE    a.NumberSample like '44.%' and   NOT EXISTS (SELECT * FROM[dbRiverwatchWaterData].[dbo].[expStnMetal] AS b 
WHERE b.SampleID = a.SampleID) 
order by SampleID desc

--linq version for reference 
--from t1 in db.Table1
--where db.Table2.All(t2 => t1.cat != t2.cat || t2.julianDte >= t1.julianDte)
--select new
--{
--    t1.appname,
--    t1.julianDte,
--    t1.cat
--};