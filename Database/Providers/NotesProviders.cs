using NotesHelper.Database.Models;
using NotesHelper.Database.SQLite;

namespace NotesHelper.Database.Providers
{
    class NodesProviders
    {
        private static readonly string TABLE_NAME = "notes";

        //-------------------------------------------------------------------------------
        // Constructor.
        // Creates the table if was not created.
        //-------------------------------------------------------------------------------
        public NodesProviders()
        {
            SQLiteHandler.Initialize("database.db");
            string query = $@"CREATE TABLE IF NOT EXISTS {TABLE_NAME} (
                                id          INTEGER PRIMARY KEY,
                                parent_id   INTEGER,
                                title       TEXT,
                                text        TEXT
                              )";

            SQLiteHandler.Write(query);
        }
        //-------------------------------------------------------------------------------
        // Gets the number of rows on the table.
        //-------------------------------------------------------------------------------
        public int GetRowsCount(string condition = "")
        {
            return SQLiteHandler.GetRowsCount(TABLE_NAME, condition);
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public List<Note> GetNotes(long parentId)
        {
            var query = $@"SELECT * FROM {TABLE_NAME} WHERE parent_id={parentId} ORDER BY title";

            var listOfNotes = new List<Note>();
            SQLiteHandler.Read(query)
                .ForEach((record) => {
                    listOfNotes.Add(RecordToNote(record));
                });
            return listOfNotes;
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public Note Insert(long parentId, string title, string text)
        {
            var query = $@"INSERT INTO {TABLE_NAME} (parent_id, title, text) 
                        VALUES ({parentId}, '{title}', '{text}')";

            var result = SQLiteHandler.Write(query);
            if (result.affectedRows == 1)
            {
                return new Note {
                    Id = result.lastInsertedId,
                    Text = text,
                    Title = title,
                    ParentId = parentId
                };
            }
            throw new System.Exception($"Error adding new topic. Affected rows: {result.affectedRows}");
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public void Update(Topic topic)
        {
            var query = $@"UPDATE {TABLE_NAME} SET text='{topic.Text}' WHERE id={topic.Id}";

            var result = SQLiteHandler.Write(query);
            if (result.affectedRows != 1)
            {
                throw new System.Exception($"Error updating topic. Affected rows: {result.affectedRows}");
            }
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public bool Delete(long id)
        {
            var query = $"DELETE FROM {TABLE_NAME}  WHERE id={id}";
            var result = SQLiteHandler.Write(query);
            return result.affectedRows == 1;
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public int Delete(List<string> ids)
        {
            var query = $"DELETE FROM {TABLE_NAME}  WHERE id IN ({String.Join(", ", ids)})";
            var result = SQLiteHandler.Write(query);
            return result.affectedRows;
        }

        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        private static Note RecordToNote(Types.Record record)
        {
            return new Note
            {
                Id = record["id"].ToLong(),
                ParentId = record["parent_id"].ToLong(),
                Title = record["title"].ToString(),
                Text = record["text"].ToString()
            };
        }
    }
}

