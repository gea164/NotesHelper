using System.Globalization;

namespace NotesHelper.Database.SQLite
{
    public class Types
    {
        public class QueryParams : Dictionary<string, object> { };
        public class Record : Dictionary<string, Cell> { };
        public class RecordsCollection : List<Record> { };

        public enum CellType
        {
            Integer,
            Long,
            Double,
            Boolean,
            String,
        }

        public class Cell
        {
            private long longValue;
            private int intValue;
            private double doubleValue;
            private bool boolValue;
            private string strValue = "";
            private CellType type = CellType.String;

            //---------------------------------------------------------------------------
            // Constructor
            //---------------------------------------------------------------------------
            public Cell(int value)
            {
                this.Set(value);
            }

            //---------------------------------------------------------------------------
            // Constructor
            //---------------------------------------------------------------------------
            public Cell(long value)
            {
                this.Set(value);
            }

            //---------------------------------------------------------------------------
            // Constructor
            //---------------------------------------------------------------------------
            public Cell(double value)
            {
                this.Set(value);
            }

            //---------------------------------------------------------------------------
            // Constructor
            //---------------------------------------------------------------------------
            public Cell(bool value)
            {
                this.Set(value);
            }

            //---------------------------------------------------------------------------
            // Constructor
            //---------------------------------------------------------------------------
            public Cell(string value)
            {
                this.Set(value);
            }

            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public CellType GetCellType()
            {
                return this.type;
            }

            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public int ToInt()
            {
                return intValue;
            }

            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public long ToLong()
            {
                return longValue;
            }

            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public double ToDouble()
            {
                return doubleValue;
            }

            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public bool ToBool()
            {
                return boolValue;
            }

            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public override string ToString()
            {
                return strValue;
            }
            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public void Set(string value)
            {
                this.type = CellType.String;
                this.strValue = value;
                this.boolValue = value.ToLower() == "true";
                long.TryParse(value, out longValue);
                int.TryParse(value, out intValue);
                double.TryParse(value, out doubleValue);
            }

            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public void Set(int value)
            {
                this.type = CellType.Integer;
                this.longValue = value;
                this.intValue = value;
                this.doubleValue = value;
                this.strValue = intValue.ToString();
            }
            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public void Set(long value)
            {
                this.type = CellType.Long;
                this.longValue = value;
                this.intValue = (int)value;
                this.doubleValue = value;
                this.strValue = intValue.ToString();
            }
            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public void Set(bool value)
            {
                this.type = CellType.Boolean;
                this.boolValue = value;
                this.strValue = value ? "true" : "false";
                this.longValue = this.intValue = value ? 1 : 0;
                this.doubleValue = value ? 1.0 : 0.0;
            }
            //---------------------------------------------------------------------------
            //---------------------------------------------------------------------------
            public void Set(double value)
            {
                this.type = CellType.Double;
                this.longValue = (long)value;
                this.intValue = (int)value;
                this.doubleValue = value;
                this.strValue = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", value);
            }
        }
    }
}
