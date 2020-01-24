using System;
using System.Collections.Generic;

namespace DatabaseWrapper.Core.QueryGenerator {
    public class QueryBuilder {
        /// <summary>
        /// The table
        /// </summary>
        protected string table = "";

        /// <summary>
        /// The columns
        /// </summary>
        protected string columns = "";

        /// <summary>
        /// The joins
        /// </summary>
        protected string joins = "";

        /// <summary>
        /// The where clauses
        /// </summary>
        protected string whereClauses = "";

        /// <summary>
        /// The having clauses
        /// </summary>
        protected string havingClauses = "";

        /// <summary>
        /// The group by
        /// </summary>
        protected string groupBy = "";
        
        /// <summary>
        /// The order by
        /// </summary>
        protected string orderBy = "";

        /// <summary>
        /// The limit
        /// </summary>
        protected string limit = "";

        /// <summary>
        /// The where clause provided
        /// </summary>
        private bool whereClauseProvided = false;

        /// <summary>
        /// The having clause provided
        /// </summary>
        private bool havingClauseProvided = false;

        /// <summary>
        /// Gets or sets the query parameters.
        /// </summary>
        /// <value>
        /// The query parameters.
        /// </value>
        public Dictionary<string, object> Parameters { get; set; }

        #region Query construction methods

        /// <summary>
        /// Selects the specified columns.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <returns></returns>
        public QueryBuilder Select(List<string> columns = null) {
            if (columns != null) {
                this.columns = $" {string.Join(",", columns)} ";
            } else {
                this.columns = " * ";
            }
            return this;
        }

        /// <summary>
        /// Froms the specified table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public QueryBuilder From(string table) {
            this.table = $" FROM {table} ";
            return this;
        }

        /// <summary>
        /// Inners the join.
        /// </summary>
        /// <param name="table1">The table1.</param>
        /// <param name="column1">The column1.</param>
        /// <param name="table2">The table2.</param>
        /// <param name="column2">The column2.</param>
        /// <returns></returns>
        public QueryBuilder InnerJoin(string table1, string column1, string table2, string column2) {
            this.Join(JoinType.Inner, table1, column1, table2, column2);
            return this;
        }

        /// <summary>
        /// Lefts the join.
        /// </summary>
        /// <param name="table1">The table1.</param>
        /// <param name="column1">The column1.</param>
        /// <param name="table2">The table2.</param>
        /// <param name="column2">The column2.</param>
        /// <returns></returns>
        public QueryBuilder LeftJoin(string table1, string column1, string table2, string column2) {
            this.Join(JoinType.Left, table1, column1, table2, column2);
            return this;
        }

        /// <summary>
        /// Rights the join.
        /// </summary>
        /// <param name="table1">The table1.</param>
        /// <param name="column1">The column1.</param>
        /// <param name="table2">The table2.</param>
        /// <param name="column2">The column2.</param>
        /// <returns></returns>
        public QueryBuilder RightJoin(string table1, string column1, string table2, string column2) {
            this.Join(JoinType.Right, table1, column1, table2, column2);
            return this;
        }

        /// <summary>
        /// Wheres the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public QueryBuilder Where(string key, object value) {
            this.Clause(ClauseType.Where, key, value);
            return this;
        }

        /// <summary>
        /// Ands the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public QueryBuilder And(string key, object value) {
            this.Clause(ClauseType.And, key, value);
            return this;
        }

        /// <summary>
        /// Ors the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public QueryBuilder Or(string key, object value) {
            this.Clause(ClauseType.Or, key, value);
            return this;
        }

        /// <summary>
        /// Havings the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public QueryBuilder Having(string key, object value) {
            this.Clause(ClauseType.Having, key, value);
            return this;
        }

        /// <summary>
        /// Joins the specified join.
        /// </summary>
        /// <param name="join">The join.</param>
        /// <param name="table1">The table1.</param>
        /// <param name="table2">The table2.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid join</exception>
        public QueryBuilder Join(JoinType join, string table1, string column1, string table2, string column2) {
            string joinsTemp = string.Empty;

            switch (join) {
                case JoinType.Inner:
                    this.joins += $" INNER JOIN {table1}.{column1} = {table2}.{column2}";
                    break;
                case JoinType.Left:
                    this.joins += $" LEFT JOIN {table1}.{column1} = {table2}.{column2}";
                    break;
                case JoinType.Right:
                    this.joins += $" RIGHT JOIN {table1}.{column1} = {table2}.{column2}";
                    break;
                default:
                    throw new Exception("Invalid join");
            }

            return this;
        }

        /// <summary>
        /// Clauses the specified clause.
        /// </summary>
        /// <param name="clause">The clause.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// WHERE clause already provided once
        /// or
        /// WHERE or HAVING clause not provided
        /// or
        /// HAVING clause already provided once
        /// or
        /// Invalid clause
        /// </exception>
        public QueryBuilder Clause(ClauseType clause, string key, object value) {
            string clausesTemp = string.Empty;

            switch (clause) {
                case ClauseType.Where:
                    if (this.whereClauseProvided && !this.havingClauseProvided) {
                        throw new Exception("WHERE clause already provided once");
                    }
                    this.whereClauses += $" WHERE {key} = @{key} ";
                    this.whereClauseProvided = true;
                    break;
                case ClauseType.And:
                    if (!this.whereClauseProvided || !this.havingClauseProvided) {
                        throw new Exception("WHERE or HAVING clause not provided");
                    }
                    this.whereClauses += $" AND {key} = @{key} ";
                    break;
                case ClauseType.Or:
                    if (!this.whereClauseProvided || !this.havingClauseProvided) {
                        throw new Exception("WHERE or HAVING clause not provided");
                    }
                    this.whereClauses += $" OR {key} = @{key} ";
                    break;
                case ClauseType.Having:
                    if (!this.havingClauseProvided) {
                        throw new Exception("HAVING clause already provided once");
                    }
                    this.whereClauses += $" HAVING {key} = @{key} ";
                    break;
                default:
                    throw new Exception("Invalid clause");
            }

            if (this.Parameters == null) {
                this.Parameters = new Dictionary<string, object>();
            }
            this.Parameters.Add($"@{key}", value);

            return this;
        }

        /// <summary>
        /// Groups the by.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public QueryBuilder GroupBy(string columnName) {
            this.groupBy = $" GROUP BY {columnName} ";
            return this;
        }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid order type</exception>
        public QueryBuilder OrderBy(string columnName, OrderType order = OrderType.Asc) {
            switch (order) {
                case OrderType.Asc:
                    this.orderBy = $"{columnName} ASC ";
                    break;
                case OrderType.Desc:
                    this.orderBy = $"{columnName} DESC ";
                    break;
                default:
                    throw new Exception("Invalid order type");
            }
            return this;
        }

        /// <summary>
        /// Limits the specified limit.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public QueryBuilder Limit(int limit = 0, int offset = 0) {
            this.limit = $" LIMIT {limit} " + (offset != 0 ? $", {offset} " : " ");
            return this;
        }

        #endregion

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            return $"SELECT {this.columns} {this.table} {this.joins} {this.whereClauses} {this.groupBy} {this.havingClauses} {this.orderBy} {this.limit}";
        }
    }
}
