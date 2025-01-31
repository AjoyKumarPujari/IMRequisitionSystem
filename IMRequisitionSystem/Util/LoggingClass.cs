using System;
using System.Data.SqlClient;

namespace IMRequisitionSystem.Util
{
    public static class LoggingClass
    {
        public static void SaveExceptionLog(Exception ex)
        {
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        
        public static void SaveSqlExceptionLog(SqlException ex)
        {
            //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

    }
}