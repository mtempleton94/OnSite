-- Create Organisations
INSERT INTO dbo.Organisation (Name) 
VALUES ('Geostorm Research Facility');
INSERT INTO dbo.Organisation (Name) 
VALUES ('Lobster Delivery');

-- Add sites for the Organisations
INSERT INTO dbo.Site(StreetAddress, City, State, PostCode, OrganisationID)
VALUES ('1-99 Storm Street', 'Flooded Lakes', 'SA', 5066, (SELECT OrganisationID from dbo.Organisation WHERE Organisation.Name='Geostorm Research Facility')),
('18 Lobster Lane', 'Port Adelaide', 'SA', 5015, (SELECT OrganisationID from dbo.Organisation WHERE Organisation.Name='Lobster Delivery'));

-- Add areas for the sites
INSERT INTO [dbo].[Area](Floor, Description, Classification, SiteID)
VALUES ('Ground', 'Area A', 'Commercial (UNCLASSIFIED)', (SELECT SiteID from [dbo].[Site] s 
			WHERE s.StreetAddress='1-99 Storm Street' AND s.City='Flooded Lakes' AND s.State='SA' AND s.PostCode=5066)),
		('Ground', 'Area B', 'Commercial (UNCLASSIFIED)', (SELECT SiteID from [dbo].[Site] s 
			WHERE s.StreetAddress='1-99 Storm Street' AND s.City='Flooded Lakes' AND s.State='SA' AND s.PostCode=5066)),
		('1', 'Secure', 'CLASSIFIED', (SELECT SiteID from [dbo].[Site] s 
			WHERE s.StreetAddress='1-99 Storm Street' AND s.City='Flooded Lakes' AND s.State='SA' AND s.PostCode=5066)),
		('ALL FLOORS', 'ALL AREAS', 'CLASSIFIED', (SELECT SiteID from [dbo].[Site] s 
			WHERE s.StreetAddress='1-99 Storm Street' AND s.City='Flooded Lakes' AND s.State='SA' AND s.PostCode=5066));

-- Add Staff
INSERT INTO dbo.Staff(FirstName, LastName, Position, Phone, Email)
VALUES ('Slade', 'Wilson', 'Software Engineer', '555-678-045', 'swilson@company.com'),
('Jason', 'Todd', 'Security', '555-677-397', 'jtodd@company.com');

-- Give staff access to areas
INSERT INTO [dbo].[StaffAreaAccess](StaffID, AreaID)
VALUES ((SELECT StaffID from [dbo].[Staff] where Staff.Email='swilson@company.com'), (1));