using Microsoft.AspNetCore.Mvc;
using ReenbitBlob.Interface;
using ReenbitBlob.Models;

namespace ReenbitBlob.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : Controller
    {
        private readonly IAzureBlobService _azureBlobService;
        public BlobController(IAzureBlobService azureBlobService)
        {
            _azureBlobService = azureBlobService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] EmailPackageModel model)
        {
            await _azureBlobService.UploadAsync(model);
            return Ok();
        }
    }
}
