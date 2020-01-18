﻿using DatabaseWrapper.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWrapper.Core.Providers {
    public class MySqlDatabase : Database {
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void BeginTransaction() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void CloseConnection() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void CommitTransaction() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public override IDbCommand CreateCommand() {
            return new MySqlCommand();
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override IDbCommand CreateCommand(string commandText, IDbConnection connection) {
            return new MySqlCommand(commandText, (MySqlConnection)connection);
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public override IDbConnection CreateConnection(string connectionString = "") {
            return new MySqlConnection(string.IsNullOrEmpty(connectionString) ? Settings.Default.DatabaseConnectionString : connectionString);
        }

        /// <summary>
        /// Creates the open connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public override IDbConnection CreateOpenConnection(string connectionString = "") {
            MySqlConnection connection = new MySqlConnection(string.IsNullOrEmpty(connectionString) ? Settings.Default.DatabaseConnectionString : connectionString);
            connection.Open();

            return connection;
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public override IDataParameter CreateParameter(string parameterName, object parameterValue) {
            return new MySqlParameter(parameterName, parameterValue);
        }

        /// <summary>
        /// Creates the stored proc command.
        /// </summary>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <returns></returns>
        public override IDbDataAdapter CreateDataAdapter() {
            return new MySqlDataAdapter();
        }

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public override IDbDataAdapter CreateDataAdapter(IDbCommand command) {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = (MySqlCommand)command;
            return adapter;
        }

        /// <summary>
        /// Fills the data set.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public override DataSet FillDataSet(DataSet dataset, IDbCommand command) {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = (MySqlCommand)command;
            adapter.Fill(dataset);
            return dataset;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Dispose() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens the connection.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void OpenConnection() {
            throw new NotImplementedException();
        }
    }
}
