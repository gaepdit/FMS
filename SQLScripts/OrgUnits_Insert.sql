USE [FMS]
GO

INSERT INTO [dbo].[OrganizationalUnits]
           ([Id]
           ,[Active]
           ,[Name])
     VALUES
           (<Id, uniqueidentifier,>
           ,<Active, bit,>
           ,<Name, nvarchar(max),>)
GO


