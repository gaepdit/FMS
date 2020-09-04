USE [FMS]
GO

INSERT INTO [dbo].[FacilityTypes]
           ([Id]
           ,[Active]
           ,[Code]
           ,[Name])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<Code, int,>
           ,<Name, nvarchar(20),>)
GO


