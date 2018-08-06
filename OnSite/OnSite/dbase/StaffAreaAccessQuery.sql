-- get a list of staff that have access to a particular area
Select Staff.FirstName, Staff.LastName
FROM [StaffAreaAccess] AA
JOIN Staff ON AA.StaffID = Staff.StaffID 
JOIN Area ON AA.AreaID = Area.AreaID
JOIN Site ON Area.SiteID = Site.SiteID
JOIN Organisation ON Site.OrganisationID=Organisation.OrganisationID
WHERE Organisation.Name='Geostorm Research Facility'
AND [Site].[StreetAddress]='1-99 Storm Street' AND [Site].[City]='Flooded Lakes' AND [Site].[State]='SA' AND [Site].[PostCode]=5066
AND Area.Floor='Ground' AND Area.Description='Area A' AND Area.Classification='Commercial (UNCLASSIFIED)';


