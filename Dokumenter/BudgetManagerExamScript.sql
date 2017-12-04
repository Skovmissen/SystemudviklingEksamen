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
	FinancegroupName NVARCHAR(100),
	BudgetId INT,
	FOREIGN KEY (FinancegroupName) REFERENCES FinanceGroup([Name]),
	FOREIGN KEY (BudgetId) REFERENCES Budget(Id),
	PRIMARY KEY (AccountId, BudgetId),
	
)
CREATE TABLE [Period]
(
	Id INT PRIMARY KEY IDENTITY (1,1),
	[Name] NVARCHAR(100)
)
CREATE TABLE FinanceAccountPeriod
(
	AccountId INT,
	BudgetId INT,
	PeriodId INT,
	Estimate INT,
	FOREIGN KEY (PeriodId) REFERENCES [Period](Id),
	FOREIGN KEY (AccountId, BudgetId) REFERENCES FinanceAccount (AccountId, BudgetId)	
)

