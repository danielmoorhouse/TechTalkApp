 using Microsoft.WindowsAzure.Storage.Blob;

 namespace TechTalk.Data
 {
     public interface IUpload
     {
          CloudBlobContainer GetBlobContainer(string connectionString, string containerName);
     }
 }