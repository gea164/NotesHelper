using System.Diagnostics;

namespace NotesHelper.Common
{
    internal class TextHelper
    {
        const string TOPIC_START_SYMBOL = "[ ";
        const string TOPIC_END_SYMBOL = " ]";

        const string NOTE_START_SYMBOL = "{ ";
        const string NOTE_END_SYMBOL = " }";

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public static string FormatTopicText(string text)
        {
            return TOPIC_START_SYMBOL + text + TOPIC_END_SYMBOL;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public static string FormatNoteText(string text)
        {
            return NOTE_START_SYMBOL + text + NOTE_END_SYMBOL;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public static string UnformatTopicTex(string text)
        {
            if (text.StartsWith(TOPIC_START_SYMBOL) && text.EndsWith(TOPIC_END_SYMBOL))
            {
                return text.Substring(TOPIC_START_SYMBOL.Length,
                    text.Length - TOPIC_START_SYMBOL.Length - TOPIC_END_SYMBOL.Length);
            }
            return text;
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public static string UnformatNoteTex(string text)
        {
            if (text.StartsWith(NOTE_START_SYMBOL) && text.EndsWith(NOTE_END_SYMBOL))
            {
                return text.Substring(NOTE_START_SYMBOL.Length,
                    text.Length - NOTE_START_SYMBOL.Length - NOTE_END_SYMBOL.Length);
            }
            return text;
        }
    }
}
