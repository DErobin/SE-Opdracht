using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

using System.Data;

/// <summary>
/// Summary description for DBManager
/// </summary>
public class DBManager
{
    static private string DBConString = "User Id=" + "dbi304418" + ";Password=" + "KeLdtn9JNW" + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";
    static private OracleConnection Con;

    static public OracleConnection Connection
    {
        get
        {
            if(Con == null)
            {
                Con = new OracleConnection(DBConString);
                Con.Open();
            }
            return Con;
        }
    }
    public DBManager()
	{

	}

    static public OracleDataReader ExecuteQuery(string query)
    {
        OracleCommand cmd = new OracleCommand(query, Connection);
        OracleDataReader ODReader = cmd.ExecuteReader();
        return ODReader;
    }

    static public OracleCommand ExecuteProcedure(string storedprocedure)
    {
        OracleCommand command = new OracleCommand(storedprocedure, Connection);

        command.CommandType = CommandType.StoredProcedure;
        return command;
    }
}