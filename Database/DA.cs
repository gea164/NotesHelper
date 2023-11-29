
using NotesHelper.Database.Providers;

namespace NotesHelper.Database
{
    internal class DA
    {
        public static TopicsProvider Topics = new TopicsProvider();
        public static NodesProviders Notes = new NodesProviders();
    }
}
