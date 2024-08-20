using FluentValidation;
using GNCCFChords.API.Common;
using GNCCFChords.API.DataAccess;
using GNCCFChords.API.Model.DTO;
using GNCCFChords.API.Validator;
using Microsoft.EntityFrameworkCore;

namespace GNCCFChords.API.BusinessLogic
{
    public interface ISongLogic
    {
        public Task<Guid> AddSong(SongDTO song);
        public Task<List<SongDTO>> GetSongs(string? search);
        public Task UpdateMorningLineUp(Guid songId);
        public Task UpdateYouthLineUp(Guid songId);
    }
    public class SongLogic: ISongLogic
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<SongDTO> _validator;
        public SongLogic(ApplicationDbContext context, IValidator<SongDTO> songValidator)
        {
            _context = context;
            _validator = songValidator;
        }

        public async Task<Guid> AddSong(SongDTO song)
        {
            var validation = _validator.Validate(song);

            if (!validation.IsValid) 
            {
                throw new ValidationException(validation.Errors);
            }

            var mapped = new Model.Song
            {
                SongId = Guid.NewGuid(),
                SongName = song.SongName,
                Artist = song.Artist
            };

            await _context.Songs.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return mapped.SongId;
        }

        public async Task<List<SongDTO>> GetSongs(string? search)
        {
            var songs = _context.Songs
                .OrderBy(x => x.SongName)
                .Include(x => x.ChordParts)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                songs = songs.Where(x => x.SongName.Contains(search))
                    .AsQueryable();
            }   

            var mapped = songs.Select(x => new SongDTO(x.SongId, x.SongName, x.Artist, x.ChordParts.FirstOrDefault().ChordKey.ToString())).ToList();

            return mapped;
        }
        public async Task UpdateMorningLineUp(Guid songId)
        {
            var song = await _context.Songs
                .FindAsync(songId);

            if (song is null)
                return;

            song.ForMorningService = !song.ForMorningService;
            song.Timestamp = DateTime.Now;

            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateYouthLineUp(Guid songId)
        {
            var song = await _context.Songs
                .FindAsync(songId);

            if (song is null)
                return;

            song.ForYouthService = !song.ForYouthService;
            song.Timestamp = DateTime.Now;

            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
        }
    }
}
