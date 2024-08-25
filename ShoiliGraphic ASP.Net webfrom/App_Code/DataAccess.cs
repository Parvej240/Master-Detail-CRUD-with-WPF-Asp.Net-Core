using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public static class DataAccess
{

    static string _ConnectionString = ConfigurationManager.ConnectionStrings["schoolConnection"].ConnectionString;

    static SqlConnection _Connection = null;
    public static SqlConnection Connection
    {
        get
        {
            if (_Connection == null)
            {
                _Connection = new SqlConnection(_ConnectionString);
                _Connection.Open();

                return _Connection;
            }
            else if (_Connection.State != System.Data.ConnectionState.Open)
            {
                _Connection.Open();

                return _Connection;
            }
            else
            {
                return _Connection;
            }
        }
    }

    public static DataSet GetDataSet(string sql)
    {
        using (SqlCommand cmd = new SqlCommand(sql, Connection))
        {
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds;
        }


    }
      
    public static DataTable GetDataTable(string sql)
    {
        DataSet ds = GetDataSet(sql);

        if (ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }

    public static int ExecuteSQL(string sql)
    {
        using (var cmd = new SqlCommand(sql, Connection))
        {
            return cmd.ExecuteNonQuery();

        }


    }
}