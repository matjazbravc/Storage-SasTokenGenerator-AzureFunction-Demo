using System;
using System.Data;
using System.Data.Common;

namespace StorageSasTokenGeneratorAzureFunctionDemo.Services.Extensions
{
    // inspired by: https://docs.telerik.com/data-access/developers-guide/low-level-ado-api/executing-stored-procedures/data-access-tasks-adonet-stored-procedures-out-value-params
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Add input parameter to a DbCommand with value
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="parameterName">Name of the parameter</param>
        /// <param name="parameterValue">The parameter value</param>
        public static void AddParameterWithValue(this DbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// Add output parameter to a DbCommand
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="parameterName">Name of the parameter</param>
        /// <param name="dbType">Type of parameter</param>
        public static DbParameter AddOutputParameter(this DbCommand command, string parameterName, DbType dbType = DbType.Int32)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Output;
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Add return value parameter to a DbCommand
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="parameterName">Name of the parameter</param>
        /// <param name="dbType">Type of parameter</param>
        public static DbParameter AddReturnParameter(this DbCommand command, string parameterName, DbType dbType = DbType.Int32)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(parameter);
            return parameter;
        }
    }
}