USE [FMS]
GO

INSERT INTO [dbo].[BudgetCodes]
           ([Id]
           ,[Active]
           ,[EnvironmentalInterestId]
           ,[Code]
           ,[Name]
           ,[OrganizationNumber]
           ,[ProjectNumber])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<EnvironmentalInterestId, uniqueidentifier,>
           ,<Code, nvarchar(20),>
           ,<Name, nvarchar(max),>
           ,<OrganizationNumber, nvarchar(20),>
           ,<ProjectNumber, nvarchar(20),>)
GO


