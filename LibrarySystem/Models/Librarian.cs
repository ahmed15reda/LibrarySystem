namespace LibrarySystem.Models
{
    public class Librarian : User
    {
        public Librarian(string id, string? name, string? employeeId) : base(id, name)
        {
            EmployeeId = employeeId;
        }

        public string? EmployeeId { get; set; }

    }
}
