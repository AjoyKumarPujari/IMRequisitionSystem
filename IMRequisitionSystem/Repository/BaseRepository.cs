using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using IMRequisitionSystem.Util;



namespace IMRequisitionSystem.Repository
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        protected BaseRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["IMRequisitionSystem"].ConnectionString;
        }

        protected T ExecuteStoredProcedure<T>(string storedProcedure, DynamicParameters parameters, Func<SqlMapper.GridReader, T> map)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var result = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure))
                    {
                        return map(result);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                LoggingClass.SaveSqlExceptionLog(sqlEx);
                throw;
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
                throw;
            }
        }

    }
}