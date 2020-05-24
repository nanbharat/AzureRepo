using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadImgBlob
{
   public class TableManager<TClass> where  TClass:TableEntity,new()
    { 
        // private property  
    private CloudTable table;

        // Constructor   
        public TableManager(string _CloudTableName)
        {
            if (string.IsNullOrEmpty(_CloudTableName))
            {
                throw new ArgumentNullException("Table", "Table Name can't be empty");
            }
            try
            {
                string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=sbgenaralstorage;AccountKey=3eXFGBBnVQNUQIXHQx152+vRaVuljzT1yAJwNPa58cRGq7ywqDVaKVCuIqD4kZZPIBkHeMDhAFrDcCJa+k2Vrg==;EndpointSuffix=core.windows.nets";
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                table = tableClient.GetTableReference(_CloudTableName);
                table.CreateIfNotExists();
            }
            catch (StorageException StorageExceptionObj)
            {
                throw StorageExceptionObj;
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }



        public void InsertEntity(TClass entity, bool forInsert = true)
        {
            try
            {
                if (forInsert)
                {
                    var insertOperation = TableOperation.Insert(entity);
                    table.Execute(insertOperation);
                }
                else
                {
                    var insertOrMergeOperation = TableOperation.InsertOrReplace(entity);
                    table.Execute(insertOrMergeOperation);
                }
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }



    }
}
