namespace NcmToolManager.Library.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int Role { get; set; }

        public UserModel() { }

        public UserModel(string name, int role)
        {
            Name = name;
            Role = role;
        }

        public UserModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public UserModel(string name, string lastName, int role)
        {
            Name = name;
            LastName = lastName;
            Role = role;
        }

        public UserModel(string name, string userName, string password, int role)
        {
            Name = name;
            UserName = userName;
            Password = password;
            Role = role;
        }
    }
}
