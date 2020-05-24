using Azure.Storage.Blobs;
using System;
using System.IO;

namespace UploadBlob
{
    class Program
    {
        static  void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("Azure Blob storage v12 - .NET quickstart sample\n");

            // Retrieve the connection string for use with the application. The storage
            // connection string is stored in an environment variable on the machine
            // running the application called AZURE_STORAGE_CONNECTION_STRING. If the
            // environment variable is created after the application is launched in a
            // console or with Visual Studio, the shell or application needs to be closed
            // and reloaded to take the environment variable into account.
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=sbn;AccountKey=uSSjg1A17C+U1/BIMTusfnQORZQ6Lxkh4C/UwYoOdRa57sVF+w6phIa+ZYbpCcAhifKMABPqi7LcdKb0UW8Q9Q==;EndpointSuffix=core.windows.net";

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = new BlobContainerClient(connectionString, "sbncontainer");


           //string localPath = @"C:\Users\bharat.nannavare\Desktop\AngularAssgnment\11.jpg";

            

           // // Get a reference to a blob
           // BlobClient blobClient = containerClient.GetBlobClient(localPath);

           // Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

           // // Open the file and upload its data
           // using (FileStream uploadFileStream = File.OpenRead(localPath))
           // {
           //     blobClient.UploadAsync(uploadFileStream, true);
           //     uploadFileStream.Close();
           // }


            // Create a local file in the ./data/ directory for uploading and downloading
            string localPath = @"D:\DEmo\data";
            string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);

            // Write text to the file
             File.WriteAllTextAsync(localFilePath, "Hello, World!");

            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            // Open the file and upload its data
            using (FileStream uploadFileStream = File.OpenRead(localFilePath))
            {
                 blobClient.UploadAsync(uploadFileStream, true);
                uploadFileStream.Close();
            }


        }
    }
}
