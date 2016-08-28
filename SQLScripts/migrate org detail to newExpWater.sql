USE [RiverWatch]
GO

update  [dbo].[NEWexpWater] 
          set  [NEWexpWater].[KitNumber] = [Organization].[KitNumber]
           ,  [NEWexpWater].[WaterShed] = [Organization].KitNumber
           ,[NEWexpWater].[OrganizationName] = [Organization].OrganizationName       

  FROM [dbo].[NEWexpWater] ,  [RiverWatch].[dbo].[Organization] 

  where 
  [NEWexpWater].[OrganizationID] = .[Organization].[ID]

  
GO
