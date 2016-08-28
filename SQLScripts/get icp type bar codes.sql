 SELECT     SampleID , LabID as [Barcode], Code as [Sample Type]  
 FROM      [dbRiverwatchWaterData].[dbo].[tblMetalBarCode] AS a  
 WHERE    a.NumberSample like '9876.%'  
 and   
 NOT EXISTS (SELECT * FROM [dbRiverwatchWaterData].[dbo].[expStnMetal] AS b  
 WHERE b.SampleID = a.SampleID)  

  and 
 Not Exists (Select * From [dbRiverwatchWaterData].[dbo].[tblInboundICP] as c
 Where a.LabID  =  c.CODE)

 order by SampleID desc