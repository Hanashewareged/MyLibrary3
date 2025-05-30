using System;
using System.Data;
using System.Data.SqlClient; // Changed from System.Data.SQLite
using System.Windows.Forms; // For MessageBox
using System.Configuration; // For ConfigurationManager

public static class DatabaseHelper
{
    // It's best practice to store connection strings in App.config
    // Add this to your App.config:
    // <configuration>
    //   <connectionStrings>
    //     <add name="MyLibraryDB" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyLibraryDB.mdf;Integrated Security=True;Connect Timeout=30" providerName="System.Data.SqlClient" />
    //   </connectionStrings>
    // </configuration>
    private static string connectionString = ConfigurationManager.ConnectionStrings["MyLibraryDB"].ConnectionString;

    /// <summary>
    /// Gets a new SQL Server connection.
    /// </summary>
    /// <returns>A new SqlConnection object.</returns>
    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    /// <summary>
    /// Executes a non-query SQL command (INSERT, UPDATE, DELETE).
    /// </summary>
    /// <param name="query">The SQL query to execute.</param>
    /// <param name="parameters">Optional array of SqlParameter objects.</param>
    /// <returns>The number of rows affected, or -1 if an error occurred.</returns>
    public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch (SqlException ex) // Catch specific SQL exceptions
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("SqlException in ExecuteNonQuery: " + ex.Message); // Log for debugging
                    return -1; // Indicate failure
                }
            }
        }
    }

    /// <summary>
    /// Executes a SQL query and returns the results in a DataTable.
    /// </summary>
    /// <param name="query">The SQL query to execute.</param>
    /// <param name="parameters">Optional array of SqlParameter objects.</param>
    /// <returns>A DataTable containing the query results.</returns>
    public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader()) // Use SqlDataReader for SQL Server
                    {
                        dt.Load(reader); // Load data directly into DataTable
                    }
                }
                catch (SqlException ex) // Catch specific SQL exceptions
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("SqlException in ExecuteQuery: " + ex.Message); // Log for debugging
                }
            }
        }
        return dt;
    }

    /// <summary>
    /// Executes a SQL query that returns a single scalar value (e.g., COUNT, MAX, MIN).
    /// </summary>
    /// <param name="query">The SQL query to execute.</param>
    /// <param name="parameters">Optional array of SqlParameter objects.</param>
    /// <returns>The scalar value returned by the query, or null if an error occurred or no result.</returns>
    public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
    {
        using (SqlConnection conn = GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    // Handle DBNull.Value if the query returns no rows or a NULL value
                    return result == DBNull.Value ? null : result;
                }
                catch (SqlException ex) // Catch specific SQL exceptions
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("SqlException in ExecuteScalar: " + ex.Message); // Log for debugging
                    return null;
                }
            }
        }
    }
}
