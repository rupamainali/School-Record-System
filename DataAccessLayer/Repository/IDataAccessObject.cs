namespace DataAccessLayer.Repository
{
    public interface IDataAccessObject
    {
        void OpenConnection();

        void CloseConnection();

        System.Data.DataSet ExecuteDataSet(string sql);

        System.Data.DataRow ExecuteDataRow(string sql);
        System.Data.DataTable ExecuteDataTable(string sql);
    }
}
