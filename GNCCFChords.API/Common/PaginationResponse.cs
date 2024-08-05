namespace GNCCFChords.API.Common
{
    public class PaginationResponse<T>
    {
        public int CurrentPage { get; set; }
        public int ElementsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
        public IEnumerable<object> Results { get; set; }
    }
}
