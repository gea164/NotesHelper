using System.Data.SQLite;
using static NotesHelper.Database.SQLite.Types;

namespace NotesHelper.Database.SQLite
{
    class SQLiteHandler
    {
        public class WriteResult
        {
            public int affectedRows = 0;
            public long lastInsertedId = 0;
        };

        private readonly string connectionString;
        private static SQLiteHandler instance = null;

        //-------------------------------------------------------------------------------
        //  Singleton Instance
        //-------------------------------------------------------------------------------
        private SQLiteHandler(string database)
        {
            this.connectionString = $"Data Source={database}";
        }

        //-------------------------------------------------------------------------------
        // Initialization.
        // Just set the database name.
        //-------------------------------------------------------------------------------
        public static void Initialize(string database)
        {
            if(instance == null)
            {
                instance = new SQLiteHandler(database);
            }
        }
        
        //-------------------------------------------------------------------------------
        // Executes a write operation.
        // Returns the number of affected rows.
        //-------------------------------------------------------------------------------
        public static WriteResult Write(string query, QueryParams queryParams = null)
        {
            using (var connection = new SQLiteConnection(instance.connectionString))
            {
                var result = new WriteResult();
                connection.Open();

                var formattedQuery = QueryHelper.FormatQuery(query, queryParams);

                using (var command = new SQLiteCommand(formattedQuery, connection))
                {
                    result.affectedRows = command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand("SELECT last_insert_rowid()", connection))
                {
                    result.lastInsertedId = long.Parse(command.ExecuteScalar().ToString());
                }
                return result;
            }
        }

        //-------------------------------------------------------------------------------
        // Executes a read operation.
        // Returns the rows collection.
        //-------------------------------------------------------------------------------

        public static RecordsCollection Read(string query, QueryParams queryParams = null)
        {
            using (var connection = new SQLiteConnection(instance.connectionString))
            {
                connection.Open();
                var formattedQuery = QueryHelper.FormatQuery(query, queryParams);
                using (var cmd = new SQLiteCommand(formattedQuery, connection))
                {
                    using (var datareader = cmd.ExecuteReader())
                    {
                        var listOfRecords = new RecordsCollection();
                        while (datareader.Read())
                        {
                            var record = new Record();
                            for (int i = 0; i < datareader.FieldCount; ++i)
                            {
                                var colName = datareader.GetName(i);
                                var colValue = GetFieldValue(datareader, i);
                                record.Add(colName, colValue);
                            }
                            listOfRecords.Add(record);
                        }
                        return listOfRecords;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public static int GetRowsCount(string table, string condition = "")
        {
            var query = $"SELECT COUNT(*) AS rowCount FROM {table} {condition}";
            var result = SQLiteHandler.Read(query);
            if (result.Count == 1)
            {
                return result[0]["rowCount"].ToInt();
            }
            return -1;
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        private static Cell GetFieldValue(SQLiteDataReader datareader, int i)
        {
            var fieldType = datareader.GetFieldType(i).Name.ToUpper();
            var isNull = datareader.IsDBNull(i);
            switch (fieldType)
            {
                case "INT64":
                    return new Cell(isNull ? 0 : datareader.GetInt64(i));

                case "INT32":
                    return new Cell(isNull ? 0 : datareader.GetInt32(i));

                case "DOUBLE":
                    return new Cell(isNull ? 0.0 : datareader.GetDouble(i));

                case "STRING":
                    return new Cell(isNull ? "" : datareader.GetString(i));

                case "BOOLEAN":
                    return new Cell(isNull ? false : datareader.GetBoolean(i));

                //For calculated fields like SUM and printf the type is empty
                default:
                    if (isNull)
                    {
                        return new Cell("");
                    }
                    //Try to convert to string firts.
                    try
                    {
                        return new Cell(datareader.GetString(i));
                    }
                    catch (Exception)
                    {
                        //Try to convert to double
                        try
                        {
                            return new Cell(datareader.GetDouble(i));
                        }
                        catch (Exception)
                        {
                            //Finally, try to conver to integer.
                            return new Cell(datareader.GetInt64(i));
                        }
                    }
            }
        }
    }
}
