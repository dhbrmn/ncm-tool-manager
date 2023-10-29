namespace NcmToolManager.Library.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int LoginId { get; set; }
        public string DisplayName { get; set; }

        public UserModel() { }
        public UserModel(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }

    }
}
