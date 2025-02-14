namespace LibrarySystem.Models
{
    public class NonFictionBook : Book
    {
        public string? Subject { get; set; }
        public int Edition { get; set; }
        public NonFictionBook(string? title, string? author, string? category, string? iSBN, string? subject, int edition)
            : base(title, author, category, iSBN)
        {
            Subject = subject;
            Edition = edition;
        }
    }
}
