using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseWrapper.Helper {
    public static class Utility {
        /// <summary>
		/// Checks result of a row.
		/// </summary>
		/// <returns><c>true</c>, if row result was checked, <c>false</c> otherwise.</returns>
		/// <param name="reader"><c>IDataReader</c> object.</param>
		/// <param name="columnName">Column name.</param>
		public static bool Check(IDataReader reader, string columnName) {
            if (!DBNull.Value.Equals(reader[columnName])) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Checks the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static T CheckValue<T>(this IDataReader reader, string columnName) where T : struct {
            if (DBNull.Value.Equals(reader[columnName])) {
                return default(T);
            }
            return (T)reader[columnName];
        }

        /// <summary>
        /// Checks the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static T CheckObject<T>(this IDataReader reader, string columnName) where T : class {
            if (DBNull.Value.Equals(reader[columnName])) {
                return default(T);
            }
            return reader[columnName] as T;
        }

        /// <summary>
        /// Tries the get as.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="value">The value.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public static bool TryGetAs<T>(this IDataReader reader, out T value, string columnName) where T : class {
            if (DBNull.Value.Equals(reader[columnName])) {
                value = default(T);
                return false;
            } else {
                value = (T)reader[columnName];
                return true;
            }
        }
    }
}
