using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BudgetManagerXame.Models;
using System.Collections.Specialized;
using System.Web;
using System.Web.Http;
using System.Collections.Generic;

namespace BudgetManagerXame.Classes
{
    public class DB
    {
        private static SqlConnection connection = null; // dette gør at vi kan tilgå sqlconnection "connection" i alle de metoder hvor det er nødvendigt.
        public static void OpenDb() // Lavet af Lasse
        {
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static void CloseDb() // Lavet af Lasse
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int CreateBudget(Budget budget) // Lavet af Lasse
        {
            OpenDb();
            object id;
            SqlCommand command = new SqlCommand("INSERT INTO Budget ([Year], [Description], FiscalId) VALUES (@Year, @Description, @FiscalId); SELECT SCOPE_IDENTITY();", connection);
            command.Parameters.Add(CreateParam("@Year", budget.Year, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Description", budget.Description, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FiscalId", budget.Fiscalid, SqlDbType.Int));

            try
            {
                id = command.ExecuteScalar();
                CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(id);
        }

        public static void GetBudgetID(Budget budget) // Lavet af Lasse
        {

            SqlCommand command = new SqlCommand("SELECT Id From Budget WHERE FiscalID = @FiscalId ", connection);
            command.Parameters.AddWithValue("@FiscalId", budget.Fiscalid);
            try
            {
                OpenDb();
                budget.Id = int.Parse(command.ExecuteScalar().ToString());
                CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetFiscalId(int id) // Lavet af Lasse
        {
            OpenDb();
            SqlCommand command = new SqlCommand("SELECT FiscalId From Budget WHERE Id = @id ", connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {

                string fiscal = command.ExecuteScalar().ToString();
                CloseDb();
                return fiscal;
            }
            catch (Exception ex)
            {
                Console.WriteLine(id);
                throw ex;
            }
        }
        public static void CreateFinanceAccounts(int id, string name, string ledgerAccount, Budget budget) // Lavet af Lasse
        {
            OpenDb();
            SqlCommand command = new SqlCommand("IF NOT EXISTS (SELECT * FROM FinanceAccount WHERE AccountId = @AccountId AND BudgetId = @BudgetId) INSERT INTO FinanceAccount (AccountId, Name, FinancegroupName, BudgetId) VALUES (@AccountId, @Name, @FinancegroupName, @BudgetId)", connection);

            command.Parameters.Add(CreateParam("@AccountId", id, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Name", name, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FinancegroupName", ledgerAccount, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@BudgetId", budget.Id, SqlDbType.Int));
            try
            {

                command.ExecuteNonQuery();
                CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void CreateFinanceAccountsPeriod(int Accountid, int PeriodId, Budget budget) // Lavet af Lasse
        {
            OpenDb();
            SqlCommand command = new SqlCommand("INSERT INTO FinanceAccountPeriod(AccountId, BudgetId, PeriodId, Estimate) VALUES (@AccountId, @BudgetId, @PeriodId, @Estimate)", connection);

            command.Parameters.Add(CreateParam("@AccountId", Accountid, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@BudgetId", budget.Id, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@PeriodId", PeriodId, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Estimate", 0, SqlDbType.Float));
            try
            {

                command.ExecuteNonQuery();
                CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateFinanceAccountsPeriod(int Accountid, int PeriodId, int budgetId, double estimate) // Lavet af Lasse
        {
            OpenDb();
            SqlCommand command = new SqlCommand("UPDATE FinanceAccountPeriod SET Estimate = @Estimate WHERE BudgetId = @BudgetId AND AccountId = @AccountId AND PeriodId = @PeriodId", connection);

            command.Parameters.Add(CreateParam("@AccountId", Accountid, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@BudgetId", budgetId, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@PeriodId", PeriodId, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Estimate", estimate, SqlDbType.Float));
            try
            {

                command.ExecuteNonQuery();
                CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsertDataInBudget(Budget budget, Period period, FinanceAccount account, FinanceAccountPeriod FAP) // Lavet af Lasse
        {
            OpenDb();
            SqlCommand command = new SqlCommand("INSERT INTO FinanceAccountPeriod (AccountId, BudgetId, PeriodId, Estimate) VALUES (@AccountId, @BudgetId, @PeriodId, @Estimate)", connection);

            command.Parameters.Add(CreateParam("@AccountId", account.AccountId, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@PeriodId", period.Id, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@Estimate", FAP.Estimate, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@BudgetId", budget.Id, SqlDbType.Int));
            try
            {

                command.ExecuteNonQuery();
                CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DeleteBudget(int? id)
        {
            OpenDb();
            SqlCommand command = new SqlCommand("DELETE FROM Budget WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {

                command.ExecuteNonQuery();
                CloseDb();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static Budget GetBudget(int? id) // Lavet af Lasse
        {
            Budget budget = new Budget();
            OpenDb();
            SqlCommand command = new SqlCommand("SELECT * FROM Budget WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    budget.Id = Convert.ToInt32(reader["Id"]);
                    budget.Year = Convert.ToInt32(reader["Year"]);
                    budget.Description = reader["Description"].ToString();
                    budget.Fiscalid = reader["FiscalId"].ToString();

                }
                CloseDb();
                return budget;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetAllBudgets(string fiscalId) // Lavet af Lasse
        {
            OpenDb();
            DataTable dt = new DataTable();
            SqlDataAdapter command = new SqlDataAdapter("SELECT * From Budget WHERE FiscalID = @FiscalId ", connection);
            command.SelectCommand.Parameters.AddWithValue("@FiscalId", fiscalId);

            try
            {
                command.Fill(dt);
                CloseDb();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Period> GetAllPeriods() // Lavet af Lasse
        {
            OpenDb();
            List<Period> Periods = new List<Period>();
            SqlCommand command = new SqlCommand("SELECT * From Period", connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Period p = new Period();
                    p.Id = (int)reader["Id"];
                    p.Name = (string)reader["Name"];
                    Periods.Add(p);
                }
                CloseDb();
                return Periods;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<FinanceAccount> GetAllFinanceAccounts(int id) // Lavet af Lasse
        {
            OpenDb();
            List<FinanceAccount> Accounts = new List<FinanceAccount>();
            SqlCommand command = new SqlCommand("SELECT * From FinanceAccount WHERE BudgetId = @BudgetId", connection);
            command.Parameters.AddWithValue("@BudgetId", id);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FinanceAccount p = new FinanceAccount();
                    p.AccountId = (int)reader["AccountId"];
                    p.Name = (string)reader["Name"];
                    p.FinanceGroup = (string)reader["FinancegroupName"];
                    p.BudgetId = (int)reader["BudgetId"];
                    Accounts.Add(p);
                }
                CloseDb();
                return Accounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<FinanceAccountPeriod> GetAllFinanceAccountsEstimates(int BudgetId, int PeriodeId) // Lavet af Lasse
        {
            OpenDb();
            List<FinanceAccountPeriod> Accounts = new List<FinanceAccountPeriod>();
            SqlCommand command = new SqlCommand("SELECT * From FinanceAccountPeriod WHERE BudgetId = @BudgetId AND PeriodId = @PeriodId", connection);
            command.Parameters.AddWithValue("@BudgetId", BudgetId);
            command.Parameters.AddWithValue("@PeriodId", PeriodeId);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FinanceAccountPeriod p = new FinanceAccountPeriod();
                    p.AccountId = Convert.ToInt32(reader["AccountId"]);
                    p.BudgetId = Convert.ToInt32(reader["BudgetId"]);
                    p.PeriodId = Convert.ToInt32(reader["PeriodId"]);
                    p.Estimate = Convert.ToInt32(reader["Estimate"]);
                    Accounts.Add(p);
                }
                CloseDb();
                return Accounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<FinanceAccountPeriod> GetAllFinanceAccountsEstimates(int BudgetId) // Lavet af Lasse
        {
            OpenDb();
            List<FinanceAccountPeriod> Accounts = new List<FinanceAccountPeriod>();
            SqlCommand command = new SqlCommand("SELECT * From FinanceAccountPeriod WHERE BudgetId = @BudgetId", connection);
            command.Parameters.AddWithValue("@BudgetId", BudgetId);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FinanceAccountPeriod p = new FinanceAccountPeriod();
                    p.AccountId = Convert.ToInt32(reader["AccountId"]);
                    p.BudgetId = Convert.ToInt32(reader["BudgetId"]);
                    p.PeriodId = Convert.ToInt32(reader["PeriodId"]);
                    p.Estimate = Convert.ToInt32(reader["Estimate"]);
                    Accounts.Add(p);
                }
                CloseDb();
                return Accounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<FinanceAccountPeriod> GetAllFinanceAccountsEstimateForAccount(int BudgetId, int period) // Lavet af Lasse
        {
            OpenDb();
            List<FinanceAccountPeriod> Accounts = new List<FinanceAccountPeriod>();
            SqlCommand command = new SqlCommand("SELECT * From FinanceAccountPeriod WHERE BudgetId = @BudgetId AND PeriodId = @Period", connection);
            command.Parameters.AddWithValue("@BudgetId", BudgetId);
            command.Parameters.AddWithValue("@Period", period);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FinanceAccountPeriod p = new FinanceAccountPeriod();
                    p.AccountId = Convert.ToInt32(reader["AccountId"]);
                    p.BudgetId = Convert.ToInt32(reader["BudgetId"]);
                    p.PeriodId = Convert.ToInt32(reader["PeriodId"]);
                    p.Estimate = Convert.ToInt32(reader["Estimate"]);
                    Accounts.Add(p);
                }
                CloseDb();
                return Accounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<FinanceGroup> GetAllFinanceGroups() // Lavet af Lasse
        {
            OpenDb();
            List<FinanceGroup> FinanceGroups = new List<FinanceGroup>();
            SqlCommand command = new SqlCommand("SELECT * From FinanceGroup", connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FinanceGroup p = new FinanceGroup();
                    p.Name = (string)reader["Name"];
                    FinanceGroups.Add(p);
                }
                CloseDb();
                return FinanceGroups;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string GetFinanceGroupName(string LedgerAccount) // Lavet af Lasse
        {
            OpenDb();
            DataTable dt = new DataTable();
            string name = "";
            SqlDataAdapter command = new SqlDataAdapter("SELECT Name FROM FinanceGroup WHERE LedgerAccount = @LedgerAccount", connection);

            command.SelectCommand.Parameters.AddWithValue("@LedgerAccount", LedgerAccount);

            try
            {
                command.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        name = row["Name"].ToString();
                    }
                }
                else
                {
                    name = "";
                }


                CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return name;
        }


        private static SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
    }
}