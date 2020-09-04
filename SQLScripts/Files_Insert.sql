USE [FMS]
GO

INSERT INTO [dbo].[Files]
           ([Id]
           ,[Active]
           ,[FileLabel])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<FileLabel, nvarchar(9),>)
GO


