using MySql.Data.MySqlClient;
using System.Management.Automation;

namespace MyMaria
{
    [Cmdlet(VerbsLifecycle.Invoke, "Query")]
    public class InvokeQueryCommand : PSCmdlet
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

            using (var dataAdapter = new MySqlDataAdapter(new MySqlCommand($"{Query}", Connection)))
            {
                using (var dataSet = new System.Data.DataSet())
                {
                    int rows = dataAdapter.Fill(dataSet);
                    WriteVerbose($"{rows} total rows affected");
                    foreach (System.Data.DataTable table in dataSet.Tables)
                    {
                        foreach (System.Data.DataRow row in table.Rows)
                            WriteObject(row);
                    }
                }
            }
        }
    }
}
