USE [FMS]
GO

INSERT INTO [dbo].[Facilities]
           ([Id]
           ,[Active]
           ,[FacilityNumber]
           ,[FileId]
           ,[EnvironmentalInterestId]
           ,[FacilityTypeId]
           ,[OrganizationalUnitId]
           ,[BudgetCodeId]
           ,[Name]
           ,[ComplianceOfficerId]
           ,[FacilityStatusId]
           ,[Location]
           ,[Address]
           ,[City]
           ,[State]
           ,[PostalCode]
           ,[Latitude]
           ,[Longitude]
           ,[CountyId])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<FacilityNumber, nvarchar(max),>
           ,<FileId, uniqueidentifier,>
           ,<EnvironmentalInterestId, uniqueidentifier,>
           ,<FacilityTypeId, uniqueidentifier,>
           ,<OrganizationalUnitId, uniqueidentifier,>
           ,<BudgetCodeId, uniqueidentifier,>
           ,<Name, nvarchar(max),>
           ,<ComplianceOfficerId, uniqueidentifier,>
           ,<FacilityStatusId, uniqueidentifier,>
           ,<Location, nvarchar(max),>
           ,<Address, nvarchar(100),>
           ,<City, nvarchar(30),>
           ,<State, nvarchar(2),>
           ,<PostalCode, nvarchar(10),>
           ,<Latitude, decimal(8,6),>
           ,<Longitude, decimal(9,6),>
           ,<CountyId, int,>)
GO


