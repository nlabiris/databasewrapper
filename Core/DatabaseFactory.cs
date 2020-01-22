using System;
using System.Reflection;

namespace DatabaseWrapper.Core {
    public sealed class DatabaseFactory {
        /// <summary>
        /// Creates the database.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error instantiating database: " + db + ". " + ex.Message</exception>
        public static IDatabase CreateDatabase(string provider, string connectionString) {
            string db = DatabaseConfiguration.GetDatabaseNamespace(provider);
            DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration(connectionString);
            try {
                // Find the class
                Type database = Type.GetType(db);
                // Get it's constructor
                ConstructorInfo constructor = database.GetConstructor(new Type[] { });
                // Invoke it's constructor, which returns an instance.
                Database createdObject = (Database)constructor.Invoke(null);
                // Initialize the connection string property for the database.
                createdObject.Configuration = databaseConfiguration;
                // Pass back the instance as a Database
                return createdObject;
            } catch (Exception ex) {
                throw new Exception("Error instantiating database: " + db + ". " + ex.Message);
            }
        }
    }
}
