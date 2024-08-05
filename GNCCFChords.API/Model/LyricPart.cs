namespace GNCCFChords.API.Model
{
    public class LyricPart
    {
        public Guid LyricPartId { get; set; }
        public string LyricPartName { get; set; }
        public string LyricContent { get; set; }
        public Guid SongId { get; set; }

        public virtual Song Song { get; set; }
    }
}
