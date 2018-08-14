EXEC DropTables;

CREATE TABLE [dbo].[Organisation] 
(  
    [OrganisationID]	INT			 NOT NULL	IDENTITY (1, 1),
	[Name]				VARCHAR (MAX)  NOT NULL,
	CONSTRAINT [AK_OrganisationID]	UNIQUE([OrganisationID]),
    CONSTRAINT [PK_Organisation]	PRIMARY KEY CLUSTERED ([OrganisationID] ASC)
);

CREATE TABLE [dbo].[Site] 
(  
    [SiteID]			INT				NOT NULL	IDENTITY (1, 1),
	[StreetAddress]		VARCHAR(100)	NOT NULL,
	[City]				VARCHAR(50)		NOT NULL,
	[State]				VARCHAR(25)		NOT NULL,
	[PostCode]			VARCHAR(10)		NOT NULL,
	[Phone]				VARCHAR(50)		NULL,
	[Email]				VARCHAR(320)	NULL,
    [OrganisationID]	INT				NOT NULL, 
    CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED ([SiteID] ASC), 
	CONSTRAINT [AK_SiteAddress] Unique ([StreetAddress], [City], [State], [PostCode]),
    CONSTRAINT [FK_Site_OrganisationID] FOREIGN KEY ([OrganisationID]) REFERENCES [dbo].[Organisation](OrganisationID)
);

CREATE TABLE [dbo].[Area] 
(  
    [AreaID]			INT				NOT NULL	IDENTITY (1, 1),
	[Floor]				VARCHAR(10)		NOT NULL,
	[Description]		VARCHAR(50)		NULL,
	[Classification]	VARCHAR(25)		NOT NULL,
	[SiteID]			INT				NOT NULL,
	CONSTRAINT [AK_AreaIdentification] Unique ([SiteID], [Floor], [Description]),
    CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED ([AreaID] ASC), 
	CONSTRAINT [FK_Area_SiteID] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[Site](SiteID)
);
CREATE TABLE [dbo].[Staff] 
(  
	[StaffID]	INT				NOT NULL	IDENTITY (1, 1),
	[FirstName]	VARCHAR(20)		NOT NULL,
	[LastName]	VARCHAR(20)		NOT NULL,
	[Position]	VARCHAR(50)		NOT NULL,
	[Phone]		VARCHAR(50)		NOT NULL,
	[Email]		VARCHAR(320)	NOT NULL,
	CONSTRAINT [AK_StaffPhone]	UNIQUE([Phone]),
	CONSTRAINT [AK_StaffEmail]	UNIQUE([Email]),
    CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED ([StaffID] ASC)
);
create table [dbo].[StaffAreaAccess]
(
	[StaffID]	INT	NOT NULL,
	[AreaID]	INT NOT NULL,
	CONSTRAINT [PK_StaffAreaAccess] PRIMARY KEY (StaffID, AreaID),
	CONSTRAINT [FK_StaffAreaAccess_StaffID] FOREIGN KEY (StaffID) REFERENCES [dbo].[Staff](StaffID),
	CONSTRAINT [FK_StaffAreaAccess_AreaID] FOREIGN KEY (AreaID) REFERENCES [dbo].[Area](AreaID)
);

CREATE TABLE [dbo].[Visitor] 
(
    [VisitorID]            INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]            VARCHAR (20) NOT NULL,
    [LastName]             VARCHAR (20) NOT NULL,
    [IdentificationNumber] VARCHAR (20) NOT NULL,
    [OrganisationID]       INT          NOT NULL,
    CONSTRAINT [PK_VisitorID] PRIMARY KEY CLUSTERED ([VisitorID] ASC),
    CONSTRAINT [AK_Visitor_IdentificationNumber] UNIQUE NONCLUSTERED ([IdentificationNumber] ASC),
    CONSTRAINT [FK_Visitor_OrganisationID] FOREIGN KEY ([OrganisationID]) REFERENCES [dbo].[Organisation] ([OrganisationID])
);

create table [dbo].[VisitorBadge]
(
	[BadgeID]	INT				NOT NULL	IDENTITY (1, 1),
	[BadgeType]	VARCHAR(20)		NOT NULL,
	[SiteID]	INT				NOT NULL, 
    CONSTRAINT [PK_BadgeID] PRIMARY KEY CLUSTERED ([BadgeID] ASC), 
    CONSTRAINT [FK_VisitorBadge_SiteID] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[Site](SiteID)
);

create table [dbo].[Visit]
(
	[VisitID]		INT				NOT NULL	IDENTITY (1, 1),
	[Description]	VARCHAR(50)		NULL,
	[SignInTime]    DATETIME2 (7)   NULL,
	[SignOutTime]	DATETIME2 (7)   NULL,
	[VehicleID]		VARCHAR(10)		NULL,
	[VisitorID]		INT				NOT NULL,
	[BadgeID]		INT           NULL,
	[SignedInByID]  INT           NULL,
	[StaffEscortID] INT           NULL,
	[UnescortedApprovedByID] INT  NULL,
	[SiteID]        INT           NOT NULL,
	[AreaID]        INT           NULL,
	[ApprovalStatus] BIT          NOT NULL,
	CONSTRAINT [PK_VisitID] PRIMARY KEY CLUSTERED ([VisitID] ASC), 
	CONSTRAINT [FK_Visit_VisitorID]		FOREIGN KEY ([VisitorID])		REFERENCES [dbo].[Visitor]([VisitorID]),
	CONSTRAINT [FK_Visit_BadgeID]		FOREIGN KEY ([BadgeID])			REFERENCES [dbo].[VisitorBadge]([BadgeID]),
	CONSTRAINT [FK_Visit_SignedInByID]	FOREIGN KEY ([SignedInByID])	REFERENCES [dbo].[Staff] ([StaffID]),
	CONSTRAINT [FK_Visit_StaffEscortID] FOREIGN KEY ([StaffEscortID])	REFERENCES [dbo].[Staff] ([StaffID]),
	CONSTRAINT [FK_Visit_UnescortedApprovedByID] FOREIGN KEY ([UnescortedApprovedByID]) REFERENCES Staff ([StaffID]),
	CONSTRAINT [FK_Visit_SiteID]		FOREIGN KEY ([SiteID])		REFERENCES [dbo].[Site]([SiteID]),
	CONSTRAINT [FK_Visit_AreaID]		FOREIGN KEY ([AreaID])		REFERENCES [dbo].[Area] ([AreaID])
);





