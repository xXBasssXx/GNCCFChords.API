namespace GNCCFChords.API.Common
{
    public class PaginationRequest
    {
        public int CurrentPage { get; set; } = 1;
        public int ElementsPerPage { get; set; } = 10;
    }
}
