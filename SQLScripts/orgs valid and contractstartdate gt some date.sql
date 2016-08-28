

SELECT    Organization.OrganizationName,  OrgStatus.ContractStartDate
FROM            Organization 
JOIN
                         OrgStatus
						 on Organization.OrganizationID = OrgStatus.OrganizationID
						 where
						 Organization.Active = 1 and ContractStartDate > '2015-06-30'
						 order by OrganizationName


