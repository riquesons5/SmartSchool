namespace SmartSchool.WebAPI.Helpers
{
    public class PaginationHeader
    {
        public int CorrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        
        public PaginationHeader(int correntPage, int itemsPerPage, int totalItems, int totalPages) 
        {
            this.CorrentPage = correntPage;
            this.ItemsPerPage = itemsPerPage;
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
        }        
    }
}