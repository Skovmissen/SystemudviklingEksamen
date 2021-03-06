/*    ==Scripting Parameters==

    Source Database Engine Edition : Microsoft Azure SQL Database Edition
    Source Database Engine Type : Microsoft Azure SQL Database

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [BudgetManagerExam]
GO
INSERT [dbo].[FinanceGroup] ([ID], [Name], [LedgerAccount]) VALUES (1, N'Nettoomsætning', N'Xena_Domain_Income_Accounts_Net_Turn_Over')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (2, N'Vareforbrug', N'Xena_Domain_Income_Accounts_Product_Consumption')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (3, N'Personaleomkostninger', N'Xena_Domain_Income_Accounts_Wages')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (4, N'Salgsomkostninger', N'Xena_Domain_Income_Accounts_Sales_Costs')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (5, N'Produktionsomkostninger', N'Xena_Domain_Income_Accounts_Production_Costs')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (6, N'Systemdifference', N'Xena_Domain_Income_Accounts_System_Differences')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (7, N'Lokaleomkostninger', N'Xena_Domain_Income_Accounts_Housing_Costs')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (8, N'Administrationsomkostninger', N'Xena_Domain_Income_Accounts_Administration_Costs')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (9, N'Afskrivninger', N'Xena_Domain_Income_Accounts_Depreciation')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (10, N'Koncernresultat', N'Xena_Domain_Income_Accounts_Consolidation_Result')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (11, N'Finansielle poster', N'Xena_Domain_Income_Accounts_Financial_Costs')
INSERT [dbo].[FinanceGroup] ([ID],[Name], [LedgerAccount]) VALUES (12, N'Skat', N'Xena_Domain_Income_Accounts_Taxes')


