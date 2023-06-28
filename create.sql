USE master;
GO

IF DB_ID (N'$(DATABASE_NAME)') IS NOT NULL
DROP DATABASE $(DATABASE_NAME);
GO

CREATE DATABASE $(DATABASE_NAME);
GO
     
CREATE TABLE [__EFMigrationsHistory] (
          [MigrationId] nvarchar(150) NOT NULL,
          [ProductVersion] nvarchar(32) NOT NULL,
          CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
      );
      SELECT 1
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');

      SELECT [MigrationId], [ProductVersion]
      FROM [__EFMigrationsHistory]
      ORDER BY [MigrationId];

      CREATE TABLE [Employee] (
          [Id] int NOT NULL IDENTITY,
          [Nome] nvarchar(50) NOT NULL,
          [Sobrenome] nvarchar(50) NOT NULL,
          [CPF] bigint NOT NULL,
          [Setor] nvarchar(50) NOT NULL,
          [SalarioBruto] decimal(10,2) NOT NULL,
          [DataAdmicao] datetime2 NOT NULL,
          [DescontoSaude] bit NOT NULL,
          [DescontoDental] bit NOT NULL,
          [DescontoVale] bit NOT NULL,
          [UserName] nvarchar(max) NULL,
          [UpdateData] datetime2 NOT NULL,
          [Active] bit NOT NULL,
          CONSTRAINT [PK_Employee] PRIMARY KEY ([Id])
      );
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20230619134943_initial', N'7.0.7'); 