IF DB_ID (N'$(DATABASE_NAME)') IS NOT NULL
DROP DATABASE $(DATABASE_NAME);
GO

CREATE DATABASE $(DATABASE_NAME);
GO

USE $(DATABASE_NAME);
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230629041528_inicio')
BEGIN
    CREATE TABLE [Employee] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [CPF] bigint NOT NULL,
        [Sector] nvarchar(50) NOT NULL,
        [GrossSalary] decimal(10,2) NOT NULL,
        [AdmissionDate] datetime2 NOT NULL,
        [DiscountSaude] bit NOT NULL,
        [DiscountDental] bit NOT NULL,
        [DiscountVale] bit NOT NULL,
        [UserName] nvarchar(max) NULL,
        [UpdateData] datetime2 NOT NULL,
        [Active] bit NOT NULL,
        CONSTRAINT [PK_Employee] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230629041528_inicio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230629041528_inicio', N'7.0.7');
END;
GO

COMMIT;
GO

