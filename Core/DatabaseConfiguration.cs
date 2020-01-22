using DatabaseWrapper.Core.Providers;
using System;

namespace DatabaseWrapper.Core {
    public class DatabaseConfiguration {
        private string connectionString;

        public DatabaseConfiguration(string connectionString) {
            this.connectionString = connectionString;
        }

        public string ConnectionString {
            internal get {
                return connectionString;
            }
            set {
                connectionString = value;
            }
        }

        /// <summary>
        /// Retrieve the namespace from the database type provided in settings.
        /// </summary>
        /// <returns>Database namespace</returns>
        internal static string GetDatabaseNamespace(string provider) {
            switch (provider) {
                case "mysql":
                    return typeof(MySqlDatabase).Namespace + "." + typeof(MySqlDatabase).Name;
                case "mssql":
                    return typeof(MSSQLDatabase).Namespace + "." + typeof(MSSQLDatabase).Name;
                default:
                    throw new NotImplementedException("Interface not implemented yet or invalid provider given");
            }
        }

        /// <summary>
        /// Creates a new database access from the database type provided in settings.
        /// </summary>
        /// <returns>Database object</returns>
        internal static IDatabase GetDatabaseObject(string provider) {
            switch (provider) {
                case "mysql":
                    return new MySqlDatabase();
                case "mssql":
                    return new MSSQLDatabase();
                default:
                    throw new NotImplementedException("Interface not implemented yet or invalid provider given");
            }
        }
    }
}
