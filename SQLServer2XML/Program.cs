// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;
using System.Data;

if (args.Length < 3)
{
    Console.WriteLine("At least 3 arguments required");
    Environment.Exit(1);
}

string ConnectionString = args[0];  //"Server = localhost; Database = BikeStores; Trusted_Connection = True;"
string Table = args[1];             //"sales.orders"
string Mode = args[2].ToLower();    //"xml2db"
string PathToFile = ".\\";
if (args.Length > 3) PathToFile = args[3];

try
{
    if (Mode == "xml2db")
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Path to XML file is required");
            Environment.Exit(1);
        }
        else
        {
            DataTable XMLTable = new DataTable(Table);
            XMLTable.ReadXml(PathToFile);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlBulkCopy BulkCopy = new SqlBulkCopy(connection);
                foreach (DataColumn c in XMLTable.Columns)
                    BulkCopy.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                BulkCopy.DestinationTableName = XMLTable.TableName;
                BulkCopy.WriteToServer(XMLTable);
            }
        }
    }
    else if (Mode == "db2xml")
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            string QueryStatement = "select * from " + Table + ";";
            connection.Open();
            SqlCommand Command = new SqlCommand(QueryStatement, connection);
            DataTable BikeStores = new DataTable(Table);
            SqlDataAdapter DataAdapter = new SqlDataAdapter(Command);
            DataAdapter.Fill(BikeStores);
            if (args.Length > 3)
            {
                BikeStores.WriteXml(PathToFile, XmlWriteMode.WriteSchema);
            }
            else
            {
                BikeStores.WriteXml(PathToFile + Table.Replace(".", "") + ".xml", XmlWriteMode.WriteSchema);
            }
        }
    }
    else Console.WriteLine("3 argument is incorrect");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}