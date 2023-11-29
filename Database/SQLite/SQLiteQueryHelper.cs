using System;
using static NotesHelper.Database.SQLite.Types;

namespace NotesHelper.Database.SQLite
{
    class QueryHelper
    {
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public static string FormatQuery(string query, QueryParams queryParams)
        {
            if (queryParams != null)
            {
                var formattedQuery = query;
                foreach (var pair in queryParams)
                {
                    formattedQuery = formattedQuery.Replace(pair.Key, GetSQLTypeAndValue(pair.Value));
                }
                return formattedQuery;
            }
            return query;
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        private static string GetSQLTypeAndValue(object value)
        {
            if (value.GetType() == typeof(string))
            {
                return value.ToString();
            }
            if (value.GetType() == typeof(int) || value.GetType() == typeof(Int64))
            {
                return value.ToString();
            }
            if (value.GetType() == typeof(bool))
            {
                return (bool)value ? "1" : "0";
            }
            if (value.GetType() == typeof(double) || value.GetType() == typeof(float))
            {
                return value.ToString().Replace(",", ".");
            }
            throw new Exception($"Error: Wrong type '{value.GetType().ToString()}'");
        }
    }
}
