USE dbRiverwatchWaterData;

--CREATE LOGIN RWUser WITH PASSWORD = 'Password'
--DEFAULT_DATABASE = dbRiverwatchWaterData

CREATE USER RWUser FOR LOGIN RWUser ;

EXEC sp_addrolemember 'db_datareader', 'RWUser'
EXEC sp_addrolemember 'db_datawriter', 'RWUser'
-- Add User to second database
