USE [FMS]
GO

INSERT INTO [dbo].[RetentionRecords]
           ([Id]
           ,[Active]
           ,[FacilityId]
           ,[StartYear]
           ,[EndYear]
           ,[ConsignmentNumber]
           ,[BoxNumber]
           ,[ShelfNumber]
           ,[RetentionSchedule])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<FacilityId, uniqueidentifier,>
           ,<StartYear, int,>
           ,<EndYear, int,>
           ,<ConsignmentNumber, nvarchar(max),>
           ,<BoxNumber, nvarchar(max),>
           ,<ShelfNumber, nvarchar(max),>
           ,<RetentionSchedule, nvarchar(max),>)
GO


