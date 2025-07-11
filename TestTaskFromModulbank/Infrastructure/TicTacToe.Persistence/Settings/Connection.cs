namespace TicTacToe.Persistence.Settings
{
    public class Connection
    {
        public static string GetOptionConfiguration(string defaultConnection)
        {
            if (!string.IsNullOrEmpty(defaultConnection))
                return defaultConnection;

            string envString = Environment.GetEnvironmentVariable(DataBaseSet.Configuration);

            return envString;
        }
    }
}
