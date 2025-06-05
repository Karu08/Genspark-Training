using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using FileHandling.Contexts;
using FileHandling.Models;
using Microsoft.EntityFrameworkCore;
using FileHandling.Models.DTOs;


[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHubContext<NotifyHub> _hub;

    public DocumentController(AppDbContext context, IHubContext<NotifyHub> hub)
    {
        _context = context;
        _hub = hub;
    }

    [HttpPost("upload")]
    [Authorize(Roles = "HRAdmin")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] DocumentUploadDto dto)
    {
        var file = dto.File;
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var permittedExtensions = new[] { ".pdf", ".docx", ".xlsx", ".txt" };
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            return BadRequest("Invalid file type.");

        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedDocs");
        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        var filePath = Path.Combine(uploadFolder, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var doc = new Document
        {
            FileName = file.FileName,
        };

        _context.Documents.Add(doc);
        await _context.SaveChangesAsync();

        Console.WriteLine($"[NotifyHub] Sending NewDocument notification for {file.FileName}");

        await _hub.Clients.All.SendAsync("NewDocument", file.FileName);

        return Ok(new { file.FileName });
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetDocuments()
    {
        var docs = await _context.Documents.ToListAsync();
        return Ok(docs);
    }
    
    [HttpGet("test-notify")]
    public async Task<IActionResult> TestNotify()
    {
        await _hub.Clients.All.SendAsync("NewDocument", "TestFileFromAPI.txt");
        return Ok("Notification sent");
    }

}
