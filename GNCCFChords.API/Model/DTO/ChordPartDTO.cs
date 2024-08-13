namespace GNCCFChords.API.Model.DTO
{
    public record ChordPartDTO(Guid? ChordPartId, Guid SongId, string IntroChords, string Verse, string? PreChorusChords, string ChorusChords, string? BridgeChords, char ChordKey);
}