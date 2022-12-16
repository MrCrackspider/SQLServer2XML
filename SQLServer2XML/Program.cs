// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using System.Data;

if (args.Length < 3)
{
    Console.WriteLine("At least 3 arguments required");
    Environment.Exit(1);
}
//"Server = localhost; Database = BikeStores; Trusted_Connection = True;"
//"Server = localhost; Database = BikeStoresEmpty; Trusted_Connection = True;"
string ConnectionString = args[0];
string Table = args[1]; //"sales.orders"
string Mode = args[2].ToLower(); //"xml2db"
string PathToFile = ".\\";
try
{
    if (Mode == "xml2db")
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Path to XML file required");
            Environment.Exit(1);
        }
        else
        {
            PathToFile = args[3];
            DataTable FromXML = new DataTable(Table);
            FromXML.ReadXml(PathToFile);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlBulkCopy BulkCopy = new SqlBulkCopy(connection);
            foreach (DataColumn c in FromXML.Columns)
                BulkCopy.ColumnMappings.Add(c.ColumnName, c.ColumnName);
            BulkCopy.DestinationTableName = FromXML.TableName;
            BulkCopy.WriteToServer(FromXML);
            connection.Close();
        }
    }
    else if (Mode == "db2xml")
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();
        string QueryStatement = "select top 20 * from " + Table + ";";
        SqlCommand Command = new SqlCommand(QueryStatement, connection);
        SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);
        DataTable BikeStores = new DataTable(Table);
        DataAdapter.Fill(BikeStores);
        connection.Close();
        BikeStores.WriteXml(PathToFile + Table.Replace(".", "") + ".xml", XmlWriteMode.WriteSchema);
    }
    else Console.WriteLine("3 argument is incorrect");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}