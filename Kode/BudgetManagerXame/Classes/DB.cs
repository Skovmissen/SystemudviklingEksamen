﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BudgetManagerXame.Models;
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
        public static void CreateFinanceAccounts(Budget budget, FinanceAccount FA) // Lavet af Lasse
        {
            SqlCommand command = new SqlCommand("INSERT INTO FinanceAccount (Name, FinancegroupName, AccountId) VALUES (@Name, @FinancegroupName, @AccountId) WHERE BudgetId = @BudgetId", connection);
            command.Parameters.Add(CreateParam("@Name", budget.Year, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FinancegroupName", budget.Description, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@AccountId", budget.Fiscalid, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@BudgetId", budget.Fiscalid, SqlDbType.Int));
            try
            {
                command.ExecuteNonQuery();
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