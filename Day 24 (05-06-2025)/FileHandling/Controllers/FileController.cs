using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

    public FileController()
    {
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
    }

    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var filePath = Path.Combine(folderPath, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok($"File '{file.FileName}' uploaded successfully.");
    }

    
    [HttpGet("download")]
    public IActionResult DownloadFile([FromQuery] string filename)
    {
        var filePath = Path.Combine(folderPath, filename);

        if (!System.IO.File.Exists(filePath))
            return NotFound("File not found.");

        var bytes = System.IO.File.ReadAllBytes(filePath);
        var contentType = "application/octet-stream"; 
        return File(bytes, contentType, filename);
    }
}
