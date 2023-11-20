using MySql.Data.MySqlClient;
using System;
using System.Management.Automation;

namespace MyMaria
{
    [Cmdlet(VerbsCommunications.Disconnect, "Database")]
    [OutputType(typeof(MySqlConnection))]
    public class DisconnectDatabaseCommand : PSCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public MySqlConnection Connection { get; set; } = null;

        protected override void ProcessRecord()
        {
            if (Connection == null)
            {
                Connection = (MySqlConnection)SessionState.PSVariable.GetValue(SessionVariables.ConnectionName);
            }

            try
            {
                Connection.Close();
                WriteObject(Connection);
            }
            catch (Exception e)
            {
                WriteError(new ErrorRecord(e, e.Message, ErrorCategory.CloseError, null));
            }
            finally
            {
                SessionState.PSVariable.Remove(SessionVariables.ConnectionName);
            }
        }
    }
}
