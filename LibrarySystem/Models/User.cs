namespace LibrarySystem.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<Book>? BorrowedBooks { get; set; } = new List<Book>();
        public User(string? id, string? name)
        {
            Id = id;
            Name = name;
        }

        public void BorrowBook(Book book)
        {
            BorrowedBooks?.Add(book);
            book.BorrowBook();
        }

        public void ReturnBook(Book book)
        {
            BorrowedBooks?.Remove(book);
            book.ReturnBook();
        }

    }
}
