﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseWrapper.Core.Providers {
    public class MSSQLDatabase : Database {
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override IDbTransaction BeginTransaction(IDbConnection connection) {
            return connection.BeginTransaction();
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        /// <param name="connection"></param>
        public override void CloseConnection(IDbConnection connection) {
            if (connection.State == ConnectionState.Open) {
                connection.Close();
            }
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        public override void CommitTransaction(IDbTransaction transaction) {
            transaction.Commit();
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public override IDbCommand CreateCommand() {
            return new SqlCommand();
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        public override IDbCommand CreateCommand(string commandText) {
            return new SqlCommand(commandText);
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public override IDbCommand CreateCommand(string commandText, IDbConnection connection) {
            IDbCommand command = new SqlCommand(commandText, (SqlConnection)connection);
            this.PrepareParams(command);

            return command;
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override IDbCommand CreateCommand(string commandText, IDbConnection connection, Dictionary<string, object> parameters) {
            IDbCommand command = new SqlCommand(commandText, (SqlConnection)connection);
            this.PrepareParams(command, parameters);

            return command;
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public override IDbConnection CreateConnection() {
            return new SqlConnection(Configuration.ConnectionString);
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public override IDbConnection CreateConnection(string connectionString) {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Creates the open connection.
        /// </summary>
        /// <returns></returns>
        public override IDbConnection CreateOpenConnection() {
            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);
            connection.Open();

            return connection;
        }

        /// <summary>
        /// Creates the open connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public override IDbConnection CreateOpenConnection(string connectionString) {
            SqlConnection connection = new SqlConnection(connectionString);
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
            return new SqlParameter(parameterName, parameterValue);
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
            return new SqlDataAdapter();
        }

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public override IDbDataAdapter CreateDataAdapter(IDbCommand command) {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = (SqlCommand)command;
            return adapter;
        }

        /// <summary>
        /// Fills the data set.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public override DataSet FillDataSet(DataSet dataset, IDbCommand command) {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = (SqlCommand)command;
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
        /// <param name="connection">The connection.</param>
        public override void OpenConnection(IDbConnection connection) {
            if (connection.State == ConnectionState.Closed) {
                connection.Open();
            }
        }
    }
}
