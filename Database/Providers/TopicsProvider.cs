using NotesHelper.Database.Models;
using NotesHelper.Database.SQLite;

namespace NotesHelper.Database.Providers
{
    class TopicsProvider
    {
        private static readonly string TABLE_NAME = "topics";

        //-------------------------------------------------------------------------------
        // Constructor.
        // Creates the table if was not created.
        //-------------------------------------------------------------------------------
        public TopicsProvider()
        {
            SQLiteHandler.Initialize("database.db");
            string query = $@"CREATE TABLE IF NOT EXISTS {TABLE_NAME} (
                                id                      INTEGER PRIMARY KEY,
                                parent_id               INTEGER,
                                level                   INTEGER,
                                text                    TEXT
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
        public List<Topic> GetTopics(long parentId)
        {
            var query = $@"SELECT * FROM {TABLE_NAME} WHERE parent_id={parentId} ORDER BY text";

            var listOfTopics = new List<Topic>();
            SQLiteHandler.Read(query)
                .ForEach((record) => {
                    listOfTopics.Add(RecordToTopic(record));
                });
            return listOfTopics;
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public bool Exists(long parentId, string topic)
        {
            var query = $@"SELECT * FROM {TABLE_NAME} 
                            WHERE parent_id={parentId} 
                                AND text='{topic}' COLLATE NOCASE";

            return SQLiteHandler.Read(query).Count > 0;
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public Topic Insert(string topic)
        {
            return Insert(new Topic
            {
                ParentId = -1,
                Level = 0,
                Text = topic
            });
        }
        //-------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------
        public Topic Insert(Topic topic)
        {
            var query = $@"INSERT INTO {TABLE_NAME} (parent_id, level, text) 
                        VALUES ({topic.ParentId}, {topic.Level}, '{topic.Text}')";

            var result = SQLiteHandler.Write(query);
            if (result.affectedRows == 1)
            {
                return new Topic
                {
                    Id = result.lastInsertedId,
                    ParentId = topic.ParentId,
                    Level = topic.Level,
                    Text = topic.Text
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
        private static Topic RecordToTopic(Types.Record record)
        {
            return new Topic
            {
                Id = record["id"].ToLong(),
                ParentId = record["parent_id"].ToLong(),
                Level = record["level"].ToLong(),
                Text = record["text"].ToString()
            };
        }
    }
}

