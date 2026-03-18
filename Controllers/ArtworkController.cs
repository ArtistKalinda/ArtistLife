using Microsoft.AspNetCore.Mvc;

namespace ArtistLife.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtworkController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetArtworks()
        {
            return Ok("All artworks");
        }

        [HttpPost]
        public IActionResult CreateArtwork()
        {
            return Ok("Artwork created");
        }

    }
}
