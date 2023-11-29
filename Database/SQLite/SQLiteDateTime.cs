using System;

namespace NotesHelper.Database.SQLite
{
    internal class SQLiteDateTime
    {
        private readonly int year;
        private readonly int month;
        private readonly int day;
        private readonly int hour;
        private readonly int minute; 
        private readonly int second;
                    
        public SQLiteDateTime(long datetime) {
            var datetimeStr = datetime.ToString();
            year = int.Parse(datetimeStr.Substring(0, 4));
            month = int.Parse(datetimeStr.Substring(4, 2));
            day = int.Parse(datetimeStr.Substring(6, 2));
            hour = int.Parse(datetimeStr.Substring(8, 2));
            minute = int.Parse(datetimeStr.Substring(10, 2));
            second = int.Parse(datetimeStr.Substring(12));
        }

        public string Date { get { return $"{year.ToString("0000")}-{month.ToString("00")}-{day.ToString("00")}"; } }
        public string Time { get { return $"{hour.ToString("00")}:{minute.ToString("00")}:{second.ToString("00")}"; } }
        public int Year { get { return year; } }
        public int Month { get { return month; } }
        public int Day { get { return day; } }
        public int Hour { get { return hour; } }
        public int Minute { get { return minute; } }
        public int Second { get { return second; } }

        public static long GetCurrentDateTime()
        {
            var now = DateTime.Now;

            var datetime = now.Year.ToString("0000")
                + now.Month.ToString("00")
                + now.Day.ToString("00")
                + now.Hour.ToString("00")
                + now.Minute.ToString("00")
                + now.Second.ToString("00");
            
            return long.Parse(datetime);
        }
    }
}
