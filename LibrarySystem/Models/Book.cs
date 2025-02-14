namespace LibrarySystem.Models
{
    public class Book
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public string? ISBN { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Book(string? title, string? author, string? category, string? iSBN)
        {
            Title = title;
            Author = author;
            Category = category;
            ISBN = iSBN;
        }

        public void BorrowBook()
        {
            IsAvailable = false;
        }

        public void ReturnBook()
        {
            IsAvailable = true;
        }
    }
}
