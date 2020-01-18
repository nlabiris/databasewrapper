using System;

namespace DatabaseWrapper.Core {
    public class DataWorker {
        /// <summary>
        /// The database
        /// </summary>
        private IDatabase _database = null;

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public IDatabase database {
            get { return _database; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataWorker"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="connectionString">The connection string.</param>
        public DataWorker() {
            try {
                _database = DatabaseFactory.CreateDatabase();
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
