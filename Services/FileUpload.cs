using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace ex_upload_image_azure_storage.Services
{
    public class FileUpload
    {
        private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=ckdatademo;AccountKey=+agyXhFXsy6EMMD8fCoKCxk7eBVNgMUqo558CopgA5eJKkPbX1gvNQR48/w0YT4urn/Oil0tOstbILAKtT6NmQ==;EndpointSuffix=core.windows.net";
        private const string container = "ckdata-container";
        public static async Task<string> UploadBase64Image(string base64Image)
        { 
            // Gera um nome randomico para imagem
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            
            // Limpa o hash enviado
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, ""); 
            
            // Gera um array de Bytes
            byte[] imageBytes = Convert.FromBase64String(data);
            
            // Define o BLOB no qual a imagem será armazenada
            var blobClient = new BlobClient(connectionString, container, fileName);

            // Envia a imagem
            using(var stream = new MemoryStream(imageBytes)) {
                await blobClient.UploadAsync(stream);
            }
            // Retorna a URL da imagem
            return blobClient.Uri.AbsoluteUri;
        }
    }
}