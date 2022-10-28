using MySql.Data.MySqlClient;
using System.Management.Automation;

namespace MyMaria
{
    [Cmdlet(VerbsLifecycle.Invoke, "NonQuery")]
    [OutputType(typeof(int))]
    public class InvokeNonQueryCommand : PSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty()]
        public string Query { get; set; }

        [Parameter()]
        public MySqlConnection Connection { get; set; } = null;

        protected override void ProcessRecord()
        {
            if (Connection == null)
                Connection = (MySqlConnection)SessionState.PSVariable.GetValue(SessionVariables.ConnectionName);

            using (var command = new MySqlCommand($"{Query}", Connection))
            {
                WriteObject(command.ExecuteNonQuery());
            }
        }
    }
}
