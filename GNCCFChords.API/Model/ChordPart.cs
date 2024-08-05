namespace GNCCFChords.API.Model
{
    public class ChordPart
    {
        public Guid ChordPartId { get; set; }
        public string IntroChords { get; set; }
        public string? PreChorusChords { get; set; }
        public string ChorusChords { get; set; }
        public string? BridgeChords { get; set; }
        public char ChordKey { get; set; }
        public Guid SongId { get; set; }

        public virtual Song Song { get; set; }
    }
}
