/*    ==Scripting Parameters==

    Source Database Engine Edition : Microsoft Azure SQL Database Edition
    Source Database Engine Type : Microsoft Azure SQL Database

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [BudgetManagerExam]
GO
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'
Finansielle poster', N'Xena_Domain_Income_Accounts_Financial_Costs')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Administrationsomkostninger', N'Xena_Domain_Income_Accounts_Administration_Costs')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Afskrivninger', N'Xena_Domain_Income_Accounts_Depreciation')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Koncernresultat', N'Xena_Domain_Income_Accounts_Consolidation_Result')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Lokaleomkostninger', N'Xena_Domain_Income_Accounts_Housing_Costs')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Nettoomsætning', N'NULLXena_Domain_Income_Accounts_Net_Turn_Over')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Personaleomkostninger', N'Xena_Domain_Income_Accounts_Wages')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Produktionsomkostninger', N'Xena_Domain_Income_Accounts_Production_Costs')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Salgsomkostninger', N'Xena_Domain_Income_Accounts_Sales_Costs')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Skat', N'Xena_Domain_Income_Accounts_Taxes')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Systemdifference', N'Xena_Domain_Income_Accounts_System_Differences')
INSERT [dbo].[FinanceGroup] ([Name], [LedgerAccount]) VALUES (N'Vareforbrug', N'Xena_Domain_Income_Accounts_Product_Consumption')
