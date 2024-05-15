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
                                topic_id    INTEGER,
                                title       TEXT,
                                text        TEXT,
                                last_update TEXT
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
        public Note? GetNote(long id)
        {
            var query = $@"SELECT * FROM {TABLE_NAME} WHERE id={id}";

            var result = SQLiteHandler.Read(query);
            if (result.Count == 1)
            {
                return RecordToNote(result[0]);
            }
            return null;
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public List<Note> GetNotes(long topicId)
        {
            var query = $@"SELECT * FROM {TABLE_NAME} WHERE topic_id={topicId} ORDER BY title";

            var listOfNotes = new List<Note>();
            SQLiteHandler.Read(query)
                .ForEach((record) => {
                    listOfNotes.Add(RecordToNote(record));
                });
            return listOfNotes;
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public Note Insert(long topicId, string title, string text, string lastUpdate)
        {
            var query = $@"INSERT INTO {TABLE_NAME} (topic_id, title, text, last_update) 
                        VALUES ({topicId}, '{title}', '{text}', '{lastUpdate}')";

            var result = SQLiteHandler.Write(query);
            if (result.affectedRows == 1)
            {
                return new Note {
                    Id = result.lastInsertedId,
                    Text = text,
                    Title = title,
                    TopicId = topicId,
                    LastUpdate = lastUpdate,
                };
            }
            throw new System.Exception($"Error adding new topic. Affected rows: {result.affectedRows}");
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public void Update(long id, string title, string text, string lastUpdate)
        {
            var query = $@"UPDATE {TABLE_NAME} 
                            SET title='{title}', text='{text}', last_update='{lastUpdate}'
                            WHERE id={id}";

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
                TopicId = record["topic_id"].ToLong(),
                Title = record["title"].ToString(),
                Text = record["text"].ToString(),
                LastUpdate = record["last_update"].ToString(),
            };
        }
    }
}

