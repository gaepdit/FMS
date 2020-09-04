USE [FMS]
GO

INSERT INTO [dbo].[ComplianceOfficers]
           ([Id]
           ,[Active]
           ,[Name]
           ,[UnitId])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<Name, nvarchar(max),>
           ,<UnitId, uniqueidentifier,>)
GO


