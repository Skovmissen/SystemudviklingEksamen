USE BudgetManagerExam

GO

CREATE TABLE Budget
(
	
	Id INT PRIMARY KEY IDENTITY (1,1),
	[Year] INT,
	[Description] NVARCHAR(max),
	FiscalId NVARCHAR(100)
)

CREATE TABLE FinanceGroup
(
	[Name] NVARCHAR(100) PRIMARY KEY
)

CREATE TABLE FinanceAccount
(
	AccountId INT,
	[Name] NVARCHAR(100),
	FK_FinancegroupName NVARCHAR(100),
	FK_BudgetId INT,
	FOREIGN KEY (FK_FinancegroupName) REFERENCES FinanceGroup([Name]),
	FOREIGN KEY (FK_BudgetId) REFERENCES Budget(Id),
	PRIMARY KEY (AccountId, FK_BudgetId),
	
)
CREATE TABLE [Period]
(
	Id INT PRIMARY KEY IDENTITY (1,1),
	[Name] NVARCHAR(100)
)
CREATE TABLE FinanceAccountPeriod
(
	FK_AccountId INT,
	FK_BudgetId INT,
	FK_PeriodId INT,
	Estimate INT,
	FOREIGN KEY (FK_PeriodId) REFERENCES [Period](Id),
	FOREIGN KEY (FK_AccountId, FK_BudgetId) REFERENCES FinanceAccount (AccountId, FK_BudgetId)	
)

