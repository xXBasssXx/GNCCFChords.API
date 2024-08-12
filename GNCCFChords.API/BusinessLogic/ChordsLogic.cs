using GNCCFChords.API.Common;
using GNCCFChords.API.DataAccess;
using GNCCFChords.API.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace GNCCFChords.API.BusinessLogic
{
    public interface IChordsLogic
    {
        public Task AddChordsToSong(ChordPartDTO chordPart);
        public Task<ChordPartResponse> GetChordsBySong(Guid songId);
    }

    public class ChordsLogic: IChordsLogic
    {
        private readonly ApplicationDbContext _context;

        public ChordsLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddChordsToSong(ChordPartDTO chordPart)
        {
            var mapped = new Model.ChordPart
            {
                ChordPartId = Guid.NewGuid(),
                SongId = chordPart.SongId,
                IntroChords = chordPart.IntroChords,
                PreChorusChords = chordPart.PreChorusChords,
                ChorusChords = chordPart.ChorusChords,
                BridgeChords = chordPart.BridgeChords,
                ChordKey = chordPart.ChordKey
            };

            await _context.ChordParts.AddAsync(mapped);
            await _context.SaveChangesAsync();
        }

        public async Task<ChordPartResponse> GetChordsBySong(Guid songId)
        {
            var chordParts = await _context.ChordParts
                .Include(x => x.Song)
                .FirstOrDefaultAsync(x => x.SongId == songId);

            string chords = string.Empty;

            if(!string.IsNullOrEmpty(chordParts.IntroChords))
            {
                chords += $"INTRO: {Environment.NewLine}" +
                          $"{chordParts.IntroChords} {Environment.NewLine}{Environment.NewLine}";
                          
            }
            if (!string.IsNullOrEmpty(chordParts.PreChorusChords))
            {
                chords += $"PRE-CHORUS: {Environment.NewLine}" +
                          $"{chordParts.PreChorusChords} {Environment.NewLine}{Environment.NewLine}";
            }
            if(!string.IsNullOrEmpty(chordParts.ChorusChords))
            {
                chords += $"CHORUS: {Environment.NewLine}" +
                          $"{chordParts.ChorusChords}{Environment.NewLine}";
            }
            if(!string.IsNullOrEmpty(chordParts.BridgeChords))
            {
                chords += $"BRIDGE: {Environment.NewLine}" +
                          $"{chordParts.BridgeChords}{Environment.NewLine}";
            }

            var response = new ChordPartResponse(chordParts.ChordPartId, chordParts.Song.SongName, chordParts.ChordKey.ToString(), chords);

            return response;
        }
    }
}
