using GNCCFChords.API.BusinessLogic;
using GNCCFChords.API.DataAccess;
using GNCCFChords.API.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GNCCFChords.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChordPartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IChordsLogic _chordsLogic;

        public ChordPartsController(ApplicationDbContext context, IChordsLogic chordsLogic)
        {
            _context = context;
            _chordsLogic = chordsLogic;
        }

        [HttpPost]
        public async Task<IActionResult> AddChordsToSong([FromBody] ChordPartDTO chordPart)
        {
            try
            {
                await _chordsLogic.AddChordsToSong(chordPart);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetChordsBySong([FromQuery] Guid songId)
        {
            try
            {
                var result = await _chordsLogic.GetChordsBySong(songId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
