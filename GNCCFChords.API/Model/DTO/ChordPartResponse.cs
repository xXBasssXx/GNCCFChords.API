namespace GNCCFChords.API.Model.DTO
{
    public record ChordPartResponse(Guid? ChordPartId, string SongName, string ChordKey, string Chords);
}
