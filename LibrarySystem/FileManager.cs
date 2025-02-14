using LibrarySystem.Models;
using System.Text.Json;

namespace LibrarySystem
{
    public static class FileManager
    {
        public static void SaveData(string filePath, List<Book> books, List<User> users)
        {
            var data = new { Books = books, Users = users };

            string jsonData = JsonSerializer.Serialize(data);

            File.WriteAllText(filePath, jsonData);
        }

        public static void LoadData(string filePath,out List<Book> books,out List<User> users)
        {
            string jsonData = File.ReadAllText(filePath);

            var data = JsonSerializer.Deserialize<dynamic>(jsonData);

            books = data.Books.ToObject<List<Book>>();
            users = data.Users.ToObject<List<User>>();
        }
    }
}
