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
        /// Gets or sets the query parameters.
        /// </summary>
        /// <value>
        /// The query parameters.
        /// </value>
        public Dictionary<string, object> queryParams { get; set; }

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder"/> class.
        /// </summary>
        /// <param name="queryParams">The query parameters.</param>
        public QueryBuilder(Dictionary<string, object> queryParams) {
            this.queryParams = queryParams;
        }

        #endregion

        #region Query construction methods

        /// <summary>
        /// Selects the specified columns.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <returns></returns>
        public QueryBuilder Select(List<string> columns) {
            this.columns = $" {string.Join(",", columns)} ";
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
        /// Joins the specified joins.
        /// </summary>
        /// <param name="joins">The joins.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid join</exception>
        public QueryBuilder Join(Dictionary<JoinType, string> joins) {
            string joinsTemp = string.Empty;

            foreach (KeyValuePair<JoinType, string> join in joins) {
                switch (join.Key) {
                    case JoinType.Inner:
                        joinsTemp += " INNER JOIN ";
                        break;
                    case JoinType.Left:
                        joinsTemp += " LEFT JOIN ";
                        break;
                    case JoinType.Right:
                        joinsTemp += " RIGHT JOIN ";
                        break;
                    default:
                        throw new Exception("Invalid join");
                }
                joinsTemp += join.Value;
            }
            this.joins = $" {joinsTemp} ";
            return this;
        }

        /// <summary>
        /// Clauses the specified clauses.
        /// </summary>
        /// <param name="clauses">The clauses.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// WHERE clause already provided once
        /// or
        /// WHERE clause not provided
        /// or
        /// WHERE clause not provided
        /// or
        /// Invalid clause
        /// </exception>
        public QueryBuilder Clause(Dictionary<ClauseType, string> clauses) {
            string clausesTemp = string.Empty;
            bool whereClauseProvided = false;

            foreach (KeyValuePair<ClauseType, string> clause in clauses) {
                switch (clause.Key) {
                    case ClauseType.Where:
                        if (whereClauseProvided) {
                            throw new Exception("WHERE clause already provided once");
                        }
                        clausesTemp += " WHERE ";
                        whereClauseProvided = true;
                        break;
                    case ClauseType.And:
                        if (!whereClauseProvided) {
                            throw new Exception("WHERE clause not provided");
                        }
                        clausesTemp += " AND ";
                        break;
                    case ClauseType.Or:
                        if (!whereClauseProvided) {
                            throw new Exception("WHERE clause not provided");
                        }
                        clausesTemp += " OR ";
                        break;
                    default:
                        throw new Exception("Invalid clause");
                }
                clausesTemp += clause.Value;
            }
            this.whereClauses = $" {clausesTemp} ";
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
        /// Havings the specified clauses.
        /// </summary>
        /// <param name="clauses">The clauses.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid clause</exception>
        public QueryBuilder Having(Dictionary<ClauseType, string> clauses) {
            string clausesTemp = string.Empty;

            foreach (KeyValuePair<ClauseType, string> clause in clauses) {
                switch (clause.Key) {
                    case ClauseType.And:
                        clausesTemp += " AND ";
                        break;
                    case ClauseType.Or:
                        clausesTemp += " OR ";
                        break;
                    default:
                        throw new Exception("Invalid clause");
                }
                clausesTemp += clause.Value;
            }
            this.havingClauses = $" {clausesTemp} ";
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
                    this.orderBy = "ASC";
                    break;
                case OrderType.Desc:
                    this.orderBy = "DESC";
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
