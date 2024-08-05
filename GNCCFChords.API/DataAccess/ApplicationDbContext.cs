using Microsoft.EntityFrameworkCore;

namespace GNCCFChords.API.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Model.Song> Songs { get; set; }
        public DbSet<Model.ChordPart> ChordParts { get; set; }
        public DbSet<Model.LyricPart> LyricParts { get; set; }
    }
}
