
using Azure.Data.Tables;
using StorageCRUD.Model;
using System.Collections.Concurrent;

namespace StorageCRUD.Repository
{
    public class TableStorage
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=demostoragenew1;AccountKey=ZPs+vGcWeKwN1yYuSWB5qbW/kK4KV7ew+T4d9MmxRRHd9QjSHBpDyXmdMlFO317+/G0iwbi0D+2I+ASt3eZMcw==;EndpointSuffix=core.windows.net";
        public static async Task AddTable(string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.CreateIfNotExistsAsync(); 
        }
        public static async Task<Details> UpdateTable(Details employee,string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.UpsertEntityAsync(employee);
            return null;
        }
        public static async Task<Details> GetTableData(string tableName,string partitionKey, string rowKey)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            var tableData = await client.GetEntityAsync<Details>(partitionKey, rowKey);
            return tableData;
        }
        //public static async Task<TableClient> GetTable(string tableName)
        //{
        //    var data = new TableServiceClient(connectionString);
        //    var client = data.GetTableClient(tableName);
        //    return client;
        //}
        public static async Task<TableClient> GetTable(string tableName) 
        { var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName); 
            return client;
        }
        public static async Task DeleteTableData(string tableName, string partitionKey, string rowKey)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.DeleteEntityAsync(partitionKey, rowKey);
            return ;
        }
        public static async Task DeleteTable(string tableName)
        {
            var data = new TableServiceClient(connectionString);
            await data.DeleteTableAsync(tableName);           
        }    
    }
}
