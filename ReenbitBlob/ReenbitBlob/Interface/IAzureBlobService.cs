using ReenbitBlob.Models;

namespace ReenbitBlob.Interface
{
    public interface IAzureBlobService
    {
        Task UploadAsync(EmailPackageModel model);
    }
}
