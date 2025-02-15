using LibrarySystem.Models;
using System.Xml.Linq;

namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            string filePath = "library.json";

            if (File.Exists(filePath))
            {
                FileManager.LoadData(filePath, out List<Book> books, out List<User> users);
                library.Books = books;
                library.Users = users;
            }

            while (true)
            {
                DisplayMenu();

                Console.WriteLine("Enter Your Choice : ");

                string choice = Console.ReadLine();

                HandleUserChoice(choice, library, filePath);
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("==== Library Management System ====");
            Console.WriteLine("1- Manage Books");
            Console.WriteLine("2- Manage Users");
            Console.WriteLine("3- Borrow/Return Books");
            Console.WriteLine("4- Search");
            Console.WriteLine("5- Generate Reports");
            Console.WriteLine("6- Exit");
        }

        private static void HandleUserChoice(string choice, Library library, string filePath)
        {
            switch (choice)
            {
                case "1":
                    ManageBooks(library);
                    break;
                case "2":
                    ManageUsers(library);
                    break;
                case "3":
                    BorrowReturnBooks(library);
                    break;
                case "4":
                    Search(library);
                    break;
                case "5":
                    library.GenerateReport();
                    break;
                case "6":
                    FileManager.SaveData(filePath, library.Books, library.Users);
                    break;
                default:
                    Console.WriteLine("Invalid Choice. Please Try Again!");
                    break;
            }
        }

        private static void ManageBooks(Library library)
        {
            while (true)
            {
                Console.WriteLine("**** Manage Books ****");
                Console.WriteLine("1- Add Book");
                Console.WriteLine("2- Remove Book");
                Console.WriteLine("3- Update Book");
                Console.WriteLine("4- List Books");
                Console.WriteLine("5- Back");
                Console.WriteLine("Enter Your Choice :");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook(library);
                        break;
                    case "2":
                        RemoveBook(library);
                        break;
                    case "3":
                        UpdateBook(library);
                        break;
                    case "4":
                        ListBooks(library);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid Choice. Please Try Again!");
                        break;
                }
            }
        }

        private static void AddBook(Library library)
        {
            Console.WriteLine("Enter Book Type [1 For Fiction , 2 For Non-Fiction]");
            string bookType = Console.ReadLine();

            Console.WriteLine("Enter Book Title :");
            string title = Console.ReadLine()!;
            Console.WriteLine("Enter Book Author :");
            string author = Console.ReadLine();
            Console.WriteLine("Enter Book Category :");
            string category = Console.ReadLine();
            Console.WriteLine("Enter Book ISBN :");
            string isbn = Console.ReadLine();

            Book book = bookType == "1" ? AddFictionBook(title, author, category, isbn) : AddNonFictionBook(title, author, category, isbn);

            library.AddBook(book);
            Console.WriteLine("Book Added Successfully.");
            Console.Clear();
        }
        private static FictionBook AddFictionBook(string title, string author, string category, string isbn)
        {
            Console.WriteLine("Enter Book Series :");
            string series = Console.ReadLine();
            Console.WriteLine("Enter Book Volume :");
            int volume = int.Parse(Console.ReadLine());

            return new FictionBook(title, author, category, isbn, series, volume);
        }
        private static NonFictionBook AddNonFictionBook(string title, string author, string category, string isbn)
        {
            Console.WriteLine("Enter Book Subject :");
            string subject = Console.ReadLine();
            Console.WriteLine("Enter Book Edition :");
            int edition = int.Parse(Console.ReadLine());

            return new NonFictionBook(title, author, category, isbn, subject, edition);
        }

        private static void RemoveBook(Library library)
        {
            Console.WriteLine("Enter Book ISBN To Remove :");
            string isbn = Console.ReadLine();

            Book book = library.Books.FirstOrDefault(book => book.ISBN == isbn);

            if (book != null)
            {
                library.RemoveBook(book);
                Console.WriteLine("Book Removed Successfully.");
            }
            else
            {
                Console.WriteLine($"Book With ISBN {isbn} Not Found.");
            }
        }

        private static void UpdateBook(Library library)
        {
            Console.WriteLine("Enter Book ISBN To Update :");
            string isbn = Console.ReadLine();

            Book book = library.Books.FirstOrDefault(book => book.ISBN == isbn);

            if(book is null)
            {
                Console.WriteLine($"Book With ISBN {isbn} Not Found.");
                return;
            }

            if (book != null)
            {
                UpdateCommonBookDetails(book);

                switch (book)
                {
                    case FictionBook fictionBook:
                        UpdateFictionBookDetails(fictionBook);
                        break;
                    case NonFictionBook nonFictionBook:
                        UpdateNonFictionBookDetails(nonFictionBook);
                        break;
                }

                Console.WriteLine("Book Updated Successfully.");
            }
        }

        private static void UpdateCommonBookDetails(Book book)
        {
            Console.WriteLine("Enter New Title :");
            book.Title = Console.ReadLine()!;
            Console.WriteLine("Enter New Author :");
            book.Author = Console.ReadLine();
            Console.WriteLine("Enter New Category :");
            book.Category = Console.ReadLine();
        }
        private static void UpdateFictionBookDetails(FictionBook fictionBook)
        {
            Console.WriteLine("Enter New Series :");
            fictionBook.Series = Console.ReadLine();
            Console.WriteLine("Enter New Volume :");
            fictionBook.Volume = int.Parse(Console.ReadLine());
        }
        private static void UpdateNonFictionBookDetails(NonFictionBook nonFictionBook)
        {
            Console.WriteLine("Enter New Subject :");
            nonFictionBook.Subject = Console.ReadLine();
            Console.WriteLine("Enter New Edition :");
            nonFictionBook.Edition = int.Parse(Console.ReadLine());
        }
        private static void ListBooks(Library library)
        {
            Console.WriteLine("=== List Of Books ===");
            foreach (Book book in library.Books)
            {
                string details = book switch
                {
                    FictionBook fictionBook => $"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Series: {fictionBook.Series}, Volume: {fictionBook.Volume})",
                    NonFictionBook nonFictionBook => $"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Subject: {nonFictionBook.Subject}, Edition: {nonFictionBook.Edition})",
                    _ => $"{book.Title} by {book.Author} (ISBN: {book.ISBN})"
                };
            }
        }

        private static void ManageUsers(Library library)
        {
            while (true)
            {
                Console.WriteLine("**** Manage Users ****");
                Console.WriteLine("1- Add User");
                Console.WriteLine("2- Remove User");
                Console.WriteLine("3- Update User");
                Console.WriteLine("4- List Users");
                Console.WriteLine("5- Back");
                Console.WriteLine("Enter Your Choice :");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUser(library);
                        break;
                    case "2":
                        RemoveUser(library);
                        break;
                    case "3":
                        UpdateUser(library);
                        break;
                    case "4":
                        ListUsers(library);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid Choice. Please Try Again!");
                        break;
                }
            }
        }

        private static void AddUser(Library library)
        {
            Console.WriteLine("Enter User Type [1 For Member , 2 For Librarian]");
            string userType = Console.ReadLine();

            Console.WriteLine("Enter User Name :");
            string name = Console.ReadLine()!;
            Console.WriteLine("Enter User Id :");
            string id = Console.ReadLine();

            User user = userType == "1" ? AddMember(id, name) : AddLibrarian(id, name);

            library.AddUser(user);
            Console.WriteLine("User Added Successfully.");
        }

        private static Member AddMember(string id, string name)
        {
            Console.WriteLine("Enter User Membership Type :");
            MembershipType membershipType = Enum.Parse<MembershipType>(Console.ReadLine(), true);

            return new Member(id, name, membershipType);
        }
        private static Librarian AddLibrarian(string id, string name)
        {
            Console.WriteLine("Enter User EmployeeId :");
            string employeeId = Console.ReadLine();

            return new Librarian(id, name, employeeId);
        }
        private static void RemoveUser(Library library)
        {
            Console.WriteLine("Enter User Id To Remove :");
            string id = Console.ReadLine();

            User user = library.Users.FirstOrDefault(user => user.Id == id);

            if (user != null)
            {
                library.RemoveUser(user);
                Console.WriteLine("User Removed Successfully.");
            }
            else
            {
                Console.WriteLine($"User With Id {id} Not Found.");
            }
        }

        private static void UpdateUser(Library library)
        {
            Console.WriteLine("Enter User Id To Update :");
            string id = Console.ReadLine();

            User user = library.Users.FirstOrDefault(user => user.Id == id);

            if (user != null)
            {
                Console.WriteLine("Enter New Name :");
                user.Name = Console.ReadLine()!;

                if (user is Member member)
                {
                    Console.WriteLine("Enter New Membership Type :");
                    member.MembershipType = Enum.Parse<MembershipType>(Console.ReadLine(), true);
                }
                else if (user is Librarian librarian)
                {
                    Console.WriteLine("Enter New Employee Id :");
                    librarian.EmployeeId = Console.ReadLine();
                }

                Console.WriteLine("User Updated Successfully.");
            }
            else
            {
                Console.WriteLine($"User With Id {id} Not Found.");
            }
        }

        private static void ListUsers(Library library)
        {
            Console.WriteLine("=== List Of Users ===");
            foreach (User user in library.Users)
            {
                if (user is Member member)
                {
                    Console.WriteLine($"{user.Name} (Id: {user.Id}, Membership Type: {member.MembershipType})");
                }
                else if (user is Librarian librarian)
                {
                    Console.WriteLine($"{user.Name} (Id: {user.Id}, Employee Id: {librarian.EmployeeId})");
                }
                else
                {
                    Console.WriteLine($"{user.Name} (Id: {user.Id})");
                }
            }
        }

        private static void BorrowReturnBooks(Library library)
        {
            while (true)
            {
                Console.WriteLine("**** Borrow/Return Books ****");
                Console.WriteLine("1- Borrow Book");
                Console.WriteLine("2- Return Book");
                Console.WriteLine("3- List Borrowed Books");
                Console.WriteLine("4- Back");
                Console.WriteLine("Enter Your Choice :");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BorrowBook(library);
                        break;
                    case "2":
                        ReturnBook(library);
                        break;
                    case "3":
                        ListBorrowedBooks(library);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid Choice. Please Try Again!");
                        break;
                }
            }
        }

        private static void BorrowBook(Library library)
        {
            Console.WriteLine("Enter User Id :");
            string userId = Console.ReadLine();
            User? user = library.Users.FirstOrDefault(user => user.Id == userId);

            if(user != null)
            {
                Console.WriteLine("Enter Book ISBN to Borrow :");
                string isbn = Console.ReadLine();

                Book? book = library.Books.FirstOrDefault(book => book.ISBN == isbn && book.IsAvailable);

                if(book != null)
                {
                    user.BorrowBook(book);
                    Console.WriteLine("Book Borrowed Successfully.");
                }
                else
                {
                    Console.WriteLine("Book Not Available");
                }
            }
            else
            {
                Console.WriteLine("User Not Found.");
            }

        }

        private static void ReturnBook(Library library)
        {
            Console.WriteLine("Enter User Id :");
            string userId = Console.ReadLine();
            User? user = library.Users.FirstOrDefault(user => user.Id == userId);

            if (user != null)
            {
                Console.WriteLine("Enter Book ISBN to Return :");
                string isbn = Console.ReadLine();

                Book? book = library.Books.FirstOrDefault(book => book.ISBN == isbn);

                if (book != null)
                {
                    user.ReturnBook(book);
                    Console.WriteLine("Book Returned Successfully.");
                }
                else
                {
                    Console.WriteLine("Book Not Borrowed by This User");
                }
            }
            else
            {
                Console.WriteLine("User Not Found.");
            }

        }

        private static void ListBorrowedBooks(Library library)
        {
            Console.WriteLine("=== List Of Borrowed Books ===");
            foreach (var user in library.Users)
            {
                foreach (var book in user.BorrowedBooks)
                {
                    Console.WriteLine($"{user.Name} borrowed {book.Title}");
                }
            }

        }

        private static void Search(Library library)
        {
            while (true)
            {
                Console.WriteLine("**** Search ****");
                Console.WriteLine("1- Search Books");
                Console.WriteLine("2- Search Users");
                Console.WriteLine("3- Back");
                Console.WriteLine("Enter Your Choice :");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SearchBooks(library);
                        break;
                    case "2":
                        SearchUsers(library);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid Choice. Please Try Again!");
                        break;
                }
            }
        }

        private static void SearchBooks(Library library)
        {
            Console.WriteLine("Enter Search Criteria :");
            string criteria = Console.ReadLine();

            List<Book> books = library.SearchBooks(criteria);

            Console.WriteLine("=== Search Result ===");
            foreach (Book book in books)
            {
                if (book is FictionBook fictionBook)
                {
                    Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Series: {fictionBook.Series}, Volume: {fictionBook.Volume})");
                }
                else if (book is NonFictionBook nonFictionBook)
                {
                    Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Subject: {nonFictionBook.Subject}, Edition: {nonFictionBook.Edition})");
                }
                else
                {
                    Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN})");
                }
            }
        }

        private static void SearchUsers(Library library)
        {
            Console.WriteLine("Enter Search Criteria :");
            string criteria = Console.ReadLine();

            List<User> users = library.SearchUsers(criteria);

            Console.WriteLine("=== Search Result ===");

            foreach (User user in users)
            {
                if (user is Member member)
                {
                    Console.WriteLine($"{user.Name} (Id: {user.Id}, Membership Type: {member.MembershipType})");
                }
                else if (user is Librarian librarian)
                {
                    Console.WriteLine($"{user.Name} (Id: {user.Id}, Employee Id: {librarian.EmployeeId})");
                }
                else
                {
                    Console.WriteLine($"{user.Name} (Id: {user.Id})");
                }
            }
        }
    }
}
