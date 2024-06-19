namespace Application.Models
{
    public class PaginatedResult<T>
    {
        public int CurrentPage { get; set; }
        public int RecordPerPage { get; set; }
        public int TotalPages { get; set; }
        public long TotalCount { get; set; }
        public List<T> Data { get; set; }
        public PaginatedResult(int start, int recordPerPage, int totalCount, List<T> data)
        {
            Data = data;
            CurrentPage = start/recordPerPage + 1;
            RecordPerPage = recordPerPage;
            TotalCount = totalCount;
            TotalPages = totalCount/recordPerPage + (totalCount%recordPerPage == 0 ? 0 : 1);
        }
    }
}
