using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repository
{
    public class DataAccessObject : IDataAccessObject
    {
        private readonly string _connectionString;
        SqlConnection _connection;
        public DataAccessObject(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
            _connection.Open();
        }
        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }

        public System.Data.DataSet ExecuteDataSet(string sql)
        {
            var ds = new System.Data.DataSet();
            var cmd = new SqlCommand(sql, _connection);
            cmd.CommandTimeout = 120;
            SqlDataAdapter da;
            try
            {
                OpenConnection();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                da = null;
                CloseConnection();
            }
            return ds;
        }

        public System.Data.DataRow ExecuteDataRow(string sql)
        {
            try
            {
                using (var ds = ExecuteDataSet(sql))
                {
                    if (ds == null || ds.Tables.Count == 0)
                        return null;

                    if (ds.Tables[0].Rows.Count == 0)
                        return null;

                    return ds.Tables[0].Rows[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable ExecuteDataTable(string sql)
        {
            try
            {
                using (var ds = ExecuteDataSet(sql))
                {
                    if (ds == null || ds.Tables.Count == 0)
                        return null;

                    if (ds.Tables[0].Rows.Count == 0)
                        return null;

                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
