namespace GNCCFChords.API.Model.DTO
{
    public record ChordPartDTO(Guid? ChordPartId, Guid SongId, string IntroChords, string? PreChorusChords, string ChorusChords, string? BridgeChords, char ChordKey);
}