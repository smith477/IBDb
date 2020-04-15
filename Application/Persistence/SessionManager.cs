using Cassandra;

namespace Application.Persistence
{
    public class SessionManager
    {
        public static ISession session;
        public static ISession GetSession()
        {
            if (session == null)
            {
                Cluster cluster = Cluster.Builder()
                     .WithPort(9042)
                     .AddContactPoints("127.0.0.1")
                     .Build();
                session = cluster.Connect("ibdb");
            }
            return session;
        }

    }
}