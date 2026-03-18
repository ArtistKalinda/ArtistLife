using Microsoft.AspNetCore.Mvc;
using WorldOfArt.Data;
using WorldOfArt.Models;

namespace WorldOfArt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtworkController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArtworkController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.Artworks
                .Select(a => new ArtworkDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    ImageUrl = a.ImageUrl,
                    LikesCount = _context.Likes.Count(l => l.ArtworkId == a.Id)
                })
                .ToList();

            return Ok(data);
        }

        [HttpPost("like")]
        public IActionResult Like(int artworkId, int userId)
        {
            var like = new Like
            {
                ArtworkId = artworkId,
                UserId = userId
            };

            _context.Likes.Add(like);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] string title, [FromForm] string description)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"https://localhost:7023/images/{fileName}";

            var artwork = new Artwork
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                UserId = 1
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            return Ok(artwork);
        }
        [HttpGet("my/{userId}")]
        public IActionResult GetMyArtworks(int userId)
        {
            var data = _context.Artworks
                .Where(a => a.UserId == userId)
                .ToList();

            return Ok(data);
        }
    }
}
