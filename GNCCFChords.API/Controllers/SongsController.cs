using GNCCFChords.API.BusinessLogic;
using GNCCFChords.API.Common;
using GNCCFChords.API.DataAccess;
using GNCCFChords.API.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GNCCFChords.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongLogic _songService;
        private readonly ApplicationDbContext _context;

        public SongsController(ISongLogic songService, ApplicationDbContext context)
        {
            _songService = songService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddSong([FromBody] SongDTO song)
        {
            try
            {
                var songId = await _songService.AddSong(song);
                return Ok(songId);
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSongs([FromQuery] string? search = "")
        {
            try
            {
                var result = await _songService.GetSongs(search);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
