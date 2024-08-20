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
        public Task<string> GetMorningServiceLineUp();
        public Task<string> GetYouthServiceLineUp();
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
                Verse = chordPart.Verse,
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

            string chords = ConcatenateChordParts(chordParts.IntroChords, chordParts.Verse, chordParts.PreChorusChords, chordParts.ChorusChords, chordParts.BridgeChords);

            var response = new ChordPartResponse(chordParts.ChordPartId, chordParts.Song.SongName, chordParts.ChordKey.ToString(), chords);

            return response;
        }
        public async Task<string> GetMorningServiceLineUp()
        {
            var songs = await _context.Songs
                .Include(x => x.ChordParts)
                .Where(x => x.ForMorningService)
                .OrderBy(x => x.Timestamp)
                .ToListAsync();

            string lineUp = string.Empty;

            foreach (var song in songs)
            {
                var chordParts = song.ChordParts.FirstOrDefault();
                string chords = ConcatenateChordParts(chordParts.IntroChords, chordParts.Verse, chordParts.PreChorusChords, chordParts.ChorusChords, chordParts.BridgeChords);

                lineUp += $"{song.SongName} - {chordParts.ChordKey}{Environment.NewLine}{Environment.NewLine}{chords}";
            }

            return lineUp;
        }
        public async Task<string> GetYouthServiceLineUp()
        {
            var songs = await _context.Songs
                .Include(x => x.ChordParts)
                .Where(x => x.ForYouthService)
                .ToListAsync();

            string lineUp = string.Empty;

            foreach (var song in songs)
            {
                var chordParts = song.ChordParts.FirstOrDefault();
                string chords = ConcatenateChordParts(chordParts.IntroChords, chordParts.Verse, chordParts.PreChorusChords, chordParts.ChorusChords, chordParts.BridgeChords);

                lineUp += $"{song.SongName} - {chordParts.ChordKey}{Environment.NewLine}{chords}";
            }

            return lineUp;
        }
        private string ConcatenateChordParts(string intro, string verse, string preChorus, string chorus, string bridge)
        {
            string chords = string.Empty;

            if (!string.IsNullOrEmpty(intro))
            {
                chords += $"INTRO: {Environment.NewLine}" +
                          $"{intro} {Environment.NewLine}{Environment.NewLine}";

            }
            if (!string.IsNullOrEmpty(verse))
            {
                chords += $"VERSE: {Environment.NewLine}" +
                          $"{verse} {Environment.NewLine}{Environment.NewLine}";

            }
            if (!string.IsNullOrEmpty(preChorus))
            {
                chords += $"PRE-CHORUS: {Environment.NewLine}" +
                          $"{preChorus} {Environment.NewLine}{Environment.NewLine}";
            }
            if (!string.IsNullOrEmpty(chorus))
            {
                chords += $"CHORUS: {Environment.NewLine}" +
                          $"{chorus}{Environment.NewLine}{Environment.NewLine}";
            }
            if (!string.IsNullOrEmpty(bridge))
            {
                chords += $"BRIDGE: {Environment.NewLine}" +
                          $"{bridge}{Environment.NewLine}";
            }

            return chords;
        }
    }
}
