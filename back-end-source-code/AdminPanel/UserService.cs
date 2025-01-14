namespace AdminPanel
{
    using System.Text.Json;

    public class FileUserService
    {
        private readonly string _filePath = "users.json";

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filePath)) return new List<User>();
            var jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(jsonData) ?? new List<User>();
        }

        public bool Register(User user)
        {
            var users = GetAllUsers();
            if (users.Any(u => u.UserName == user.UserName || u.Email == user.Email))
                return false;

            users.Add(user);
            SaveAllUsers(users);
            return true;
        }

        public User Login(string userName, string password)
        {
            var users = GetAllUsers();
            return users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }

        private void SaveAllUsers(List<User> users)
        {
            var jsonData = JsonSerializer.Serialize(users);
            File.WriteAllText(_filePath, jsonData);
        }
    }

}
