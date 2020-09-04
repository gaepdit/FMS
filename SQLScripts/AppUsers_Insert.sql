USE [FMS]
GO

INSERT INTO [dbo].[AppUsers]
           ([Id]
           ,[UserName]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[GivenName]
           ,[FamilyName]
           ,[SubjectId]
           ,[ObjectId])
     VALUES
           (<Id, uniqueidentifier,>
           ,<UserName, nvarchar(256),>
           ,<NormalizedUserName, nvarchar(256),>
           ,<Email, nvarchar(256),>
           ,<NormalizedEmail, nvarchar(256),>
           ,<EmailConfirmed, bit,>
           ,<PasswordHash, nvarchar(max),>
           ,<SecurityStamp, nvarchar(max),>
           ,<ConcurrencyStamp, nvarchar(max),>
           ,<PhoneNumber, nvarchar(max),>
           ,<PhoneNumberConfirmed, bit,>
           ,<TwoFactorEnabled, bit,>
           ,<LockoutEnd, datetimeoffset(7),>
           ,<LockoutEnabled, bit,>
           ,<AccessFailedCount, int,>
           ,<GivenName, nvarchar(150),>
           ,<FamilyName, nvarchar(150),>
           ,<SubjectId, nvarchar(max),>
           ,<ObjectId, nvarchar(max),>)
GO


