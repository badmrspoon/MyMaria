using MySql.Data.MySqlClient;
using System.Management.Automation;

namespace MyMaria
{
    [Cmdlet(VerbsCommunications.Connect, "Database")]
    [OutputType(typeof(MySqlConnection))]
    public class ConnectDatabaseCommand : PSCmdlet
    {
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Server")]
        public string ComputerName { get; set; } = "localhost";

        [Parameter()]
        [ValidateNotNullOrEmpty()]
        public string Database { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty()]
        public PSCredential Credential { get; set; }

        [Parameter()]
        [ValidateRange(1, 65535)]
        public int Port { get; set; } = 3306;

        [Parameter()]
        [ValidateRange(0, int.MaxValue)]
        public int CommandTimeOut { get; set; } = 60;

        [Parameter()]
        [ValidateRange(0, int.MaxValue)]
        public int ConnectionTimeOut { get; set; } = 30;

        protected override void ProcessRecord()
        {
            string connectionString = $"server={ComputerName};port={Port};uid={Credential.UserName};pwd=\"{Credential.GetNetworkCredential().Password}\";";

            if (MyInvocation.BoundParameters.ContainsKey("Database"))
            {
                connectionString = string.Concat(connectionString, $"database={Database};");
            }

            connectionString = string.Concat(
                connectionString,
                $"default command timeout={CommandTimeOut}; Connection Timeout={ConnectionTimeOut};Allow User Variables=True");

            var connection = new MySqlConnection(connectionString);
            connection.Open();

            if (MyInvocation.BoundParameters.ContainsKey("Database"))
            {
                _ = new MySqlCommand($"USE {Database}", connection);
            }

            SessionState.PSVariable.Set(new PSVariable(SessionVariables.ConnectionName, connection, ScopedItemOptions.Private));
            WriteObject(connection);
        }
    }
}
