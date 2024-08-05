using GNCCFChords.API.Common;
using GNCCFChords.API.DataAccess;
using GNCCFChords.API.Model.DTO;

namespace GNCCFChords.API.BusinessLogic
{
    public interface IChordsLogic
    {
        public Task AddChordsToSong(ChordPartDTO chordPart);
        public Task<List<ChordPartDTO>> GetChordsBySong(Guid songId);
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

        public async Task<List<ChordPartDTO>> GetChordsBySong(Guid songId)
        {
            var chords = _context.ChordParts;

            if(!songId.Equals(Guid.Empty))
            {
                chords.Where(chords => chords.SongId == songId);
            }

            var mapped = chords
                .Select(chord => new ChordPartDTO(chord.ChordPartId, chord.SongId, chord.IntroChords, chord.PreChorusChords, chord.ChorusChords, chord.BridgeChords, chord.ChordKey))
                .ToList();

            return mapped;
        }
    }
}
