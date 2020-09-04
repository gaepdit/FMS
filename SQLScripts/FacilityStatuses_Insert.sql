USE [FMS]
GO

INSERT INTO [dbo].[FacilityStatuses]
           ([Id]
           ,[Active]
           ,[Status]
           ,[EnvironmentalInterestId])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<Status, nvarchar(max),>
           ,<EnvironmentalInterestId, uniqueidentifier,>)
GO


