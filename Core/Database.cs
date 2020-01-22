using System.Collections.Generic;
using System.Data;

namespace DatabaseWrapper.Core {
    public abstract class Database : IDatabase {
        /// <summary>
        /// The connection
        /// </summary>
        private static IDbConnection _connection;

        /// <summary>
        /// The configuration
        /// </summary>
        private DatabaseConfiguration _configuration;

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public static IDbConnection Connection {
            internal set {
                _connection = value;
            }
            get {
                return _connection;
            }
        }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public DatabaseConfiguration Configuration {
            internal set {
                _configuration = value;
            }
            get {
                return _configuration;
            }
        }

        /// <summary>
        /// Prepares the parameters.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameters">The parameters.</param>
        protected void PrepareParams(IDbCommand command, Dictionary<string, object> parameters) {
            command.Prepare();
            foreach (KeyValuePair<string, object> kvp in parameters) {
                command.AddWithValue($"@{kvp.Key}", kvp.Value);
            }
        }

        #region Abstract methods

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnection CreateConnection();

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public abstract IDbConnection CreateConnection(string connectionString);

        /// <summary>
        /// Creates the open connection.
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnection CreateOpenConnection();

        /// <summary>
        /// Creates the open connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public abstract IDbConnection CreateOpenConnection(string connectionString);

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <param name="connection"></param>
        public abstract void OpenConnection(IDbConnection connection);

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <param name="connection"></param>
        public abstract void CloseConnection(IDbConnection connection);

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public abstract IDbCommand CreateCommand();

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        public abstract IDbCommand CreateCommand(string commandText);

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public abstract IDbCommand CreateCommand(string commandText, IDbConnection connection);

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract IDbCommand CreateCommand(string commandText, IDbConnection connection, Dictionary<string, object> parameters);

        /// <summary>
        /// Creates the stored proc command.
        /// </summary>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public abstract IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameter(string parameterName, object parameterValue);

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public abstract IDbTransaction BeginTransaction(IDbConnection connection);

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="transaction"></param>
        public abstract void CommitTransaction(IDbTransaction transaction);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <returns></returns>
        public abstract IDbDataAdapter CreateDataAdapter();

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public abstract IDbDataAdapter CreateDataAdapter(IDbCommand command);

        /// <summary>
        /// Fills the data set.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public abstract DataSet FillDataSet(DataSet dataset, IDbCommand command);

        #endregion
    }
}
