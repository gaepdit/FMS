USE [FMS]
GO

INSERT INTO [dbo].[FileCabinets]
           ([Id]
           ,[Active]
           ,[Name]
           ,[StartCountyId]
           ,[EndCountyId]
           ,[StartSequence]
           ,[EndSequence]
           ,[FileId])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<Name, nvarchar(5),>
           ,<StartCountyId, int,>
           ,<EndCountyId, int,>
           ,<StartSequence, int,>
           ,<EndSequence, int,>
           ,<FileId, uniqueidentifier,>)
GO


