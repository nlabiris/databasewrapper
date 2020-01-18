using System.Data;

namespace DatabaseWrapper.Core {
    public static class DatabaseExtensions {
        /// <summary>
        /// Adds the with value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddWithValue<T>(this IDbCommand command, string name, T value) {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
