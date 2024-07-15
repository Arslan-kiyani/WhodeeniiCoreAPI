using Microsoft.AspNetCore.Mvc;
using WhoDeenii.Domain.Contracts.Interfaces;
using WhoDeenii.DTO.Response;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;   
    }

    [HttpGet]
    [Route("get-base64")]
    [Produces(typeof(ApiResponse<string>))]
    public async Task<IActionResult> GetImageAsBase64(string imagePath)
    {
        var response = await _imageService.GetImageAsBase64Async(imagePath);
        return Ok(response);
    }

}
