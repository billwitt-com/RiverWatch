SELECT  distinct      tblOrganization.OrganizationName, tblStatusStation.StationID,  tblStation.StationNumber, tblStation.StationName
FROM            tblStatus INNER JOIN
                         tblStatusStation ON tblStatus.StatusID = tblStatusStation.StatusID INNER JOIN
                         tblOrganization ON tblStatus.OrganizationID = tblOrganization.OrganizationID INNER JOIN
                         tblStation ON tblStatusStation.StationID = tblStation.StationID
						 where OrganizationName like 'fake%'