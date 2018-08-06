CREATE PROCEDURE DropTables
AS

	-- Drop Area Table Constraints
	IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND name = 'Area')
	BEGIN
		ALTER TABLE [dbo].[Area] DROP CONSTRAINT [FK_Area_SiteID];
	END

	-- Drop Site Table Constraints
	IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND name = 'Site')
	BEGIN
		ALTER TABLE [dbo].[Site] DROP CONSTRAINT [FK_Site_OrganisationID];
	END

	-- Drop StaffAreaAccess Table Constraints
	IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND name = 'StaffAreaAccess')
	BEGIN
		ALTER TABLE [dbo].[StaffAreaAccess] DROP CONSTRAINT [FK_StaffAreaAccess_StaffID];
		ALTER TABLE [dbo].[StaffAreaAccess] DROP CONSTRAINT [FK_StaffAreaAccess_AreaID];
	END

	-- Drop Visit Table Constraints
	IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND name = 'Visit')
	BEGIN
		ALTER TABLE [dbo].[Visit] DROP CONSTRAINT [FK_Visit_VisitorID];
		ALTER TABLE [dbo].[Visit] DROP CONSTRAINT [FK_Visit_BadgeID];
		ALTER TABLE [dbo].[Visit] DROP CONSTRAINT [FK_Visit_SignedInByID];
		ALTER TABLE [dbo].[Visit] DROP CONSTRAINT [FK_Visit_StaffEscortID];
		ALTER TABLE [dbo].[Visit] DROP CONSTRAINT [FK_Visit_UnescortedApprovedByID];
		ALTER TABLE [dbo].[Visit] DROP CONSTRAINT [FK_Visit_SiteID];
		ALTER TABLE [dbo].[Visit] DROP CONSTRAINT [FK_Visit_AreaID];
	END

	-- Drop Visitor Table Constraints
	IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND name = 'Visitor')
	BEGIN
		ALTER TABLE [dbo].[Visitor] DROP CONSTRAINT [FK_Visitor_OrganisationID];
	END

	-- Drop VisitorBadge Table Constraints
	IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND name = 'VisitorBadge')
	BEGIN
		ALTER TABLE [dbo].[VisitorBadge] DROP CONSTRAINT [FK_VisitorBadge_SiteID];
	END

	-- Drop all tables
	DROP TABLE IF EXISTS [dbo].[Area];
	DROP TABLE IF EXISTS [dbo].[Organisation];
	DROP TABLE IF EXISTS [dbo].[Site];
	DROP TABLE IF EXISTS [dbo].[Staff];
	DROP TABLE IF EXISTS [dbo].[StaffAreaAccess];
	DROP TABLE IF EXISTS [dbo].[Visit];
	DROP TABLE IF EXISTS [dbo].[Visitor];
	DROP TABLE IF EXISTS [dbo].[VisitorBadge];
