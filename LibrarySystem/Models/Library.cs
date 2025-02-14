namespace LibrarySystem.Models
{
    public class Library
    {
        public List<User> Users { get; set; }
        public List<Book> Books { get; set; }

        public Library()
        {
            Users = new List<User>();
            Books = new List<Book>();
        }

        public void AddBook(Book book)
            => Books.Add(book);

        public void RemoveBook(Book book)
            => Books.Remove(book);

        public void AddUser(User user)
            => Users.Add(user);

        public void RemoveUser(User user)
            => Users.Remove(user);

        public List<Book> SearchBooks(string criteria)
            => Books.Where(book => book.Title.Contains(criteria) || book.Author.Contains(criteria) || book.Category.Contains(criteria)).ToList();

        public List<User> SearchUsers(string criteria)
            => Users.Where(user => user.Name.Contains(criteria) || user.Id.Contains(criteria) ).ToList();

        public void GenerateReport()
        {
            Console.WriteLine("=== All Books ===");

            Books.ForEach(book => Console.WriteLine(book.Title));

            Console.WriteLine("=== All Users ===");

            Users.ForEach(user => Console.WriteLine(user.Name));

            Console.WriteLine("=== Borrowed Books ===");

            foreach (var user in Users)
            {
                foreach (var borrowedBook in user.BorrowedBooks)
                    Console.WriteLine($"{user.Name} borrowed {borrowedBook.Title}");
            }
                
        }
    }
}