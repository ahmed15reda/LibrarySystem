namespace LibrarySystem.Models
{
    public class FictionBook : Book
    {
        public string? Series { get; set; }
        public int Volume { get; set; }
        public FictionBook(string? title, string? author, string? category, string? iSBN,  string? series, int volume) 
            : base(title, author, category, iSBN)
        {
            Series = series;
            Volume = volume;
        }
    }
}
