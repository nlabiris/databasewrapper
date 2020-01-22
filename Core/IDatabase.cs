using System;
using System.Collections.Generic;
using System.Data;

namespace DatabaseWrapper.Core {
    public interface IDatabase : IDisposable {
        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        IDbConnection CreateConnection(string connectionString);

        /// <summary>
        /// Creates the open connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateOpenConnection();

        /// <summary>
        /// Creates the open connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        IDbConnection CreateOpenConnection(string connectionString);

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        void OpenConnection(IDbConnection connection);

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        void CloseConnection(IDbConnection connection);

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        IDbCommand CreateCommand();

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        IDbCommand CreateCommand(string commandText);

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        IDbCommand CreateCommand(string commandText, IDbConnection connection);

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IDbCommand CreateCommand(string commandText, IDbConnection connection, Dictionary<string, object> parameters);

        /// <summary>
        /// Creates the stored proc command.
        /// </summary>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        IDataParameter CreateParameter(string parameterName, object parameterValue);

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(IDbConnection connection);

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        void CommitTransaction(IDbTransaction transaction);

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <returns></returns>
        IDbDataAdapter CreateDataAdapter();

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        IDbDataAdapter CreateDataAdapter(IDbCommand command);

        /// <summary>
        /// Fills the data set.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        DataSet FillDataSet(DataSet dataset, IDbCommand command);
    }
}
