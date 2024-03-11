using Microsoft.AspNetCore.Mvc;
using ReenbitBlob.Interface;
using ReenbitBlob.Models;
using ReenbitBlob.Web.Controllers;

namespace ReenbitBlobTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Upload_New_FileAsync()
        {
            var mockAzureBlobService = new Mock<IAzureBlobService>();
            var model = new EmailPackageModel();
            var controller = new BlobController(mockAzureBlobService.Object);
            var result = await controller.Post(model);
            mockAzureBlobService.Verify(s => s.UploadAsync(It.IsAny<EmailPackageModel>()), Times.Once);
            Assert.IsType<OkResult>(result);
        }
    }
}