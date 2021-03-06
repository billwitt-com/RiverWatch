--/****** Script for SelectTopNRows command from SSMS  ******/

-- script to change decimal size to larger number of digits to allow 7+ digits from ICP
-- B Witt 06/09/2016

		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN AL_D DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN AL_T DECIMAL(28,10)	  
	  
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN AS_D DECIMAL(28,10)	  
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN AS_T DECIMAL(28,10)	
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN  CA_D DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN  CA_T DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN CD_D  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN CD_T  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN CU_D  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN  CU_T DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN FE_D  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN FE_T  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN  PB_D DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN  PB_T DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN MG_D  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN  MG_T DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN MN_D  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN  MN_T DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN SE_D  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN SE_T  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN ZN_D   DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN ZN_T  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN NA_D  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN NA_T  DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN  K_D DECIMAL(28,10)
		ALTER TABLE  [dbRiverwatchWaterData].[dbo].[tblInboundICP] ALTER COLUMN K_T  DECIMAL(28,10)

