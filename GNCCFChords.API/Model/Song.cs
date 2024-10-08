﻿using System.ComponentModel.DataAnnotations;

namespace GNCCFChords.API.Model
{
    public class Song
    {
        [Key]
        public Guid SongId { get; set; }
        public string SongName { get; set; }
        public string? Artist { get; set; }
        public bool ForMorningService { get; set; }
        public bool ForYouthService { get; set; }
        public DateTime Timestamp { get; set; }

        public ICollection<ChordPart> ChordParts { get; set; }
    }
}
