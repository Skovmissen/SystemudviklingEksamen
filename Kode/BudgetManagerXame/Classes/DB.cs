using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BudgetManagerXame.Models;
using System.Collections.Specialized;
using System.Web;
using System.Web.Http;

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
        public static void CreateBudget(Budget budget) // Lavet af Lasse
        {

            SqlCommand command = new SqlCommand("INSERT INTO Budget (Year, Description, FiscalId) VALUES (@Year, @Description, @FiscalId)", connection);
            command.Parameters.Add(CreateParam("@Year", budget.Year, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Description", budget.Description, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FiscalId", budget.Fiscalid, SqlDbType.Int));
            try
            {
                OpenDb();
                command.ExecuteNonQuery();
                CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public static void CreateFinanceAccounts(Budget budget, FinanceAccount FA) // Lavet af Lasse
        {
            SqlCommand command = new SqlCommand("INSERT INTO FinanceAccount (Name, FinancegroupName, AccountId) VALUES (@Name, @FinancegroupName, @AccountId) WHERE BudgetId = @BudgetId", connection);
            command.Parameters.Add(CreateParam("@Name", FA.Name, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FinancegroupName", FA.FinanceGroup, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@AccountId", FA.AccountId, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@BudgetId", FA.BudgetId, SqlDbType.Int));
            try
            {
                OpenDb();
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


        private static SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Value = value;
            return param;
        }
    }
}