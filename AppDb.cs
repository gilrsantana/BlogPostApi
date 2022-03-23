using MySqlConnector;

namespace BlogPostApi
{
    public class AppDb
    {
        public MySqlConnection Connection { get;}

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}