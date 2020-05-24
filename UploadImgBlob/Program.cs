using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadImgBlob.Model;

namespace UploadImgBlob
{
    class Program
    {

        static void Main(string[] args)
        {

            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            //CloudBlobContainer container = blobClient.GetContainerReference("images");
            //container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
            //Console.ReadKey();

            TableManager<Student> obj = new TableManager<Student>("student");
            Student std = new Student()
            {
                Name = "shilpa",
                Email = "shilpa.garje@yash.com",
                Department = "Software engineer",
                IsActive = true
            };


            obj.InsertEntity(std, true);


        }


        //public static void UploadFile( )
        //{
        //    blobContainer.CreateIfNotExists();
        //    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(azureOperationHelper.blobName);
        //    blob.UploadFromFile(azureOperationHelper.srcPath);
        //}
    }
}
